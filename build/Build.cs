using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Tools.GitVersion;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using System.IO;
using System;
using System.Linq;
using Octokit;
using System.Threading.Tasks;
using Nuke.Common.Tools.NUnit;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
using System.Text;
using System.Security.Cryptography.Xml;

[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Release'")]
    readonly string Configuration = "Release";

    [Solution] 
    readonly Solution Solution;

    [GitVersion(Framework = "net6.0", UpdateAssemblyInfo = true, UpdateBuildNumber = true)]
    readonly GitVersion GitVersion;
    private readonly string GIT_OWNER = "LBognanni";
    private readonly string GIT_REPO = "CodeMadeClock";

    AbsolutePath OutputDirectory => RootDirectory / "output";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            OutputDirectory.CreateOrCleanDirectory();
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetVerbosity(DotNetVerbosity.Minimal)
                .SetNoRestore(true)
            );

            DotNetPublish(s => s
                .SetProject(Solution.GetProject("CodeMade.Clock"))
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetVerbosity(DotNetVerbosity.Minimal)
                .SetNoRestore(true)
                .SetOutput(OutputDirectory)
            );
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            Console.WriteLine($"Version {GitVersion.AssemblySemVer}");
            DotNetTest(s => s
                .SetConfiguration(Configuration)
                .SetNoBuild(true)
                .SetVerbosity(DotNetVerbosity.Minimal)
                .SetProjectFile("src/CodeMade.Clock.sln")
            );
        });

    Target Docs => _ => _
        .DependsOn(Compile)
        .Executes(GenerateDocs);


    Target Setup => _ => _
        .DependsOn(Test)
        .Executes(() => {
            var innoLocation = Environment.ExpandEnvironmentVariables(@"%userprofile%\.nuget\packages\tools.innosetup");
            if(!Directory.Exists(innoLocation))
            {
                throw new Exception("InnoSetup location not found");
            }
            var folders = Directory.GetDirectories(innoLocation);
            if(!folders.Any())
            {
                throw new Exception("No version of InnoSetup found");
            }
            var latestVersion = folders.OrderByDescending(f => f).First();
            var isccPath = Path.Combine(latestVersion, "tools", "ISCC.exe");
            if (!File.Exists(isccPath))
            {
                throw new Exception("InnoSetup executable not found");
            }
            var process = System.Diagnostics.Process.Start(isccPath, $"setup.iss /DAppVersion={GitVersion.AssemblySemVer}");
            process.WaitForExit();
            if(process.ExitCode!=0)
            {
                throw new Exception("Setup build did not run successfully :(");
            }
        });


    Target Release => _ => _
        .DependsOn(Setup)
        .Executes(async () =>
        {
            var githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            var tokenAuth = new Credentials(githubToken);
            var client = new GitHubClient(new ProductHeaderValue("build"));
            client.Credentials = tokenAuth;

            var tag = $"v{GitVersion.AssemblySemVer}";
            var release = await CreateRelease(client, tag);
            await UploadRelease(client, release, tag);
        });

    Target ReleaseBeta => _ => _
        .DependsOn(Setup)
        .Executes(async () =>
        {
            var githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            var tokenAuth = new Credentials(githubToken);
            var client = new GitHubClient(new ProductHeaderValue("build"));
            client.Credentials = tokenAuth;

            var tag = $"v{GitVersion.AssemblySemVer}-beta";
            var release = await CreateRelease(client, tag, true);
            await UploadRelease(client, release, tag);
        });


    private async Task<GitTag> CreateTag(GitHubClient client)
    {
        var tag = $"v{GitVersion.AssemblySemVer}";
        var sha = GitVersion.Sha;
        var newTag = new NewTag
        {
            Tag = tag,
            Tagger = new Committer("Loris Bognanni", "loris@codemade.co.uk", DateTimeOffset.Now)
        };
        Serilog.Log.Information($"Creating tag {tag} for commit {sha}");
        return await client.Git.Tag.Create(GIT_OWNER, GIT_REPO, newTag);
    }

    private async Task<Release> CreateRelease(GitHubClient client, string version, bool isBeta = false)
    {
        var newRelease = new NewRelease(version)
        {
            Name = $"Version {version}",
            Body = "Please see [the official page](https://www.codemade.co.uk/clock) for release notes.",
            Draft = true,
            Prerelease = isBeta
        };

        Serilog.Log.Information($"Creating release {version}");
        return await client.Repository.Release.Create(GIT_OWNER, GIT_REPO, newRelease);
    }

    private async Task UploadRelease(GitHubClient client, Release release, string version)
    {
        using (var archiveContents = File.OpenRead(@"output\clock-setup.exe"))
        { 
            var assetUpload = new ReleaseAssetUpload()
            {
                FileName = $"clock-setup.exe",
                ContentType = "application/vnd.microsoft.portable-executable",
                RawData = archiveContents
            };
            Serilog.Log.Information($"Uploading release");
            var asset = await client.Repository.Release.UploadAsset(release, assetUpload);
        }
    }

    private void GenerateDocs()
    {
        var members = LoadDocsFile(@"src\CodeMade.ScriptedGraphics\CodeMade.ScriptedGraphics.xml")
            .Union(LoadDocsFile(@"src\CodeMade.Clock\CodeMade.Clock.xml")
                .Where(x => !x.Name.Contains("CodeMade.Clock.Properties"))
                .Where(x => !x.Name.Contains("Dispose"))
            )
            .ToArray();

        if (!Directory.Exists("docs"))
        {
            Directory.CreateDirectory("docs");
        }

        Console.WriteLine(Path.GetFullPath(".\\"));
        Console.WriteLine($"{members.Length} annotations found.");

        var classes = members.Where(x => x.Type == "T").ToList();
        var allProps = members.Where(x => x.Type == "P").ToList();

        var ignoredClasses = new[] { "T:CodeMade.Clock.TimedLayer", "T:CodeMade.Clock.TimedText" };

        foreach (var cls in classes)
        {
            if (ignoredClasses.Contains(cls.Name))
            {
                continue;
            }

            Console.WriteLine(cls.Name);

            var md = new StringBuilder();
            md.AppendLine($"# {cls.ClassName}");
            md.AppendLine();
            md.AppendLine(cls.SanitizedSummary);

            if (cls.See != null)
            {
                var references = cls.See.Select(see =>
                {
                    var reference = members.First(x => x.Name == see.To);
                    return $"[{reference.ClassName}]({reference.ClassName}.md)";
                });

                md.AppendLine($"See {string.Join(", ", references)}");
            }

            WriteProperties(members, allProps, cls, md);

            if (!string.IsNullOrWhiteSpace(cls.Example))
            {
                md.AppendLine();
                md.AppendLine("---");
                md.AppendLine();
                md.AppendLine("## Example");
                md.AppendLine();
                md.AppendLine("```json");
                md.Append(cls.SanitizedExample);
                md.AppendLine("```");
            }

            var fileName = $"docs\\{cls.ClassName}.md";
            Console.WriteLine($"Writing: {fileName}");
            File.WriteAllText(fileName, md.ToString());
        }

    }

    private static List<Member> LoadDocsFile(string xmlFile)
    {
        using var fs = File.OpenRead(xmlFile);
        var serializer = new XmlSerializer(typeof(XmlDoc));
        var doc = (XmlDoc)serializer.Deserialize(fs);
        return doc.Members;
    }

    private static void WriteProperties(IReadOnlyCollection<Member> members, IReadOnlyCollection<Member> allProps, Member cls, StringBuilder md, bool header = true)
    {
        var props = allProps.Where(p => p.ClassName == cls.ClassName).ToArray();
        if (props.Any())
        {
            if (header)
            {
                md.AppendLine();
                md.AppendLine("## Properties");
            }

            foreach (var prop in props)
            {
                md.AppendLine($"### {prop.PropertyName}");
                md.AppendLine();
                md.AppendLine(prop.SanitizedSummary);
                md.AppendLine();

                if (prop.See != null)
                {
                    var references = prop.See.Select(see =>
                    {
                        var reference = members.First(x => x.Name == see.To);
                        return $"[{reference.ClassName}]({reference.ClassName}.md)";
                    });

                    md.AppendLine($"See { string.Join(", ", references) }");
                }
            }
        }

        if(cls.Inherits != null)
        {
            var parent = members.First(x => x.Name == cls.Inherits.To);
            WriteProperties(members, allProps, parent, md, false);
        }
    }
}

internal class FileNameComparer : IEqualityComparer<AbsolutePath>
{
    public bool Equals([AllowNull] AbsolutePath x, [AllowNull] AbsolutePath y)
    {
        return Path.GetFileName(x.ToString()).Equals(Path.GetFileName(y.ToString()));
    }

    public int GetHashCode([DisallowNull] AbsolutePath obj)
    {
        return obj.GetHashCode();
    }
}

public class Reference
{
    [XmlAttribute("cref")]
    public string To { get; set; }
}

public class Member
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlElement("summary")]
    public string Summary { get; set; }

    [XmlElement("example")]
    public string Example { get; set; }

    [XmlElement("see")]
    public Reference[] See { get; set; }

    [XmlElement("inheritdoc")]
    public Reference Inherits { get; set; }

    public string Type => Name.Split(':')[0];

    public string ClassName
    {
        get
        {
            var typeClass = Name.Split(':');
            var classSplit = typeClass[1].Split('.');
            return typeClass[0] switch
            {
                "T" => classSplit[^1],
                "P" => classSplit[^2],
                _ => throw new NotImplementedException()
            };
        }
    }

    public string SanitizedSummary => BaseLineString(Summary);

    public string SanitizedExample => BaseLineString(Example);

    private static string BaseLineString(string text, int minSpaces = 0)
    {
        var prepend = new String(' ', minSpaces);
        var lines = text.Split("\r\n".ToCharArray()).Skip(1).ToArray();
        if (lines.Length == 1)
        {
            return prepend + text.TrimStart();
        }

        StringBuilder sbLines = new StringBuilder();
        var spaces = lines[0].TakeWhile(Char.IsWhiteSpace).Count();
        foreach (var line in lines)
        {
            sbLines.AppendLine(prepend + line.Substring(spaces));
        }

        return sbLines.ToString();
    }

    public string PropertyName
    {
        get
        {
            if(Type == "P")
            {
                return Name.Split('.').Last();
            }

            return "";
        }
    }
}

[XmlRoot("doc")]
public class XmlDoc
{
    [XmlArray("members")]
    [XmlArrayItem("member")]
    public List<Member> Members { get; set; }
}