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
using System.Text;

[CheckBuildProjectConfigurations]
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
    readonly Configuration Configuration = Configuration.Release;

    [Solution] 
    readonly Solution Solution;

    [GitVersion(Framework = "netcoreapp3.1", UpdateAssemblyInfo = true, UpdateBuildNumber = true)]
    readonly GitVersion GitVersion;
    private readonly string GIT_OWNER = "LBognanni";
    private readonly string GIT_REPO = "CodeMadeClock";

    AbsolutePath OutputDirectory => RootDirectory / "output";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            EnsureCleanDirectory(OutputDirectory);
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
            MSBuildTasks.MSBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetVerbosity(MSBuildVerbosity.Minimal)
                .DisableRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetConfiguration(Configuration)
                .SetNoBuild(true)
                .SetVerbosity(DotNetVerbosity.Minimal)
                .SetProjectFile("src/CodeMade.Clock.sln")
            );
        });


    Target Setup => _ => _
        //.DependsOn(Test)
        .Executes(() => {
            UpdateVersionInSetup();
            RunSetup();
        });

    private void RunSetup()
    {
        var innoLocation = Environment.ExpandEnvironmentVariables(@"%userprofile%\.nuget\packages\tools.innosetup");
        if (!Directory.Exists(innoLocation))
        {
            throw new Exception("InnoSetup location not found");
        }
        var folders = Directory.GetDirectories(innoLocation);
        if (!folders.Any())
        {
            throw new Exception("No version of InnoSetup found");
        }
        var latestVersion = folders.OrderByDescending(f => f).First();
        var isccPath = Path.Combine(latestVersion, "tools", "ISCC.exe");
        if (!File.Exists(isccPath))
        {
            throw new Exception("InnoSetup executable not found");
        }

        var process = System.Diagnostics.Process.Start(isccPath, $"setup.iss");
        process.WaitForExit();
        if (process.ExitCode != 0)
        {
            throw new Exception("Setup build did not run successfully :(");
        }
    }

    private void UpdateVersionInSetup()
    {
        var setupFile = File.ReadAllLines("setup.iss");
        var updatedLines = new List<string>();
        foreach (var line in setupFile)
        {
            if(line.StartsWith("#define AppVersion"))
            {
                updatedLines.Add($"#define AppVersion \"{GitVersion.AssemblySemVer}\"");
            }
            else
            {
                updatedLines.Add(line);
            }
        }
        File.WriteAllLines("setup.iss", updatedLines);
    }

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

    private async Task<GitTag> CreateTag(GitHubClient client)
    {
        var tag = $"v{GitVersion.AssemblySemVer}";
        var sha = GitVersion.Sha;
        var newTag = new NewTag
        {
            Tag = tag,
            Tagger = new Committer("Loris Bognanni", "loris@codemade.co.uk", DateTimeOffset.Now)
        };
        Logger.Normal($"Creating tag {tag} for commit {sha}");
        return await client.Git.Tag.Create(GIT_OWNER, GIT_REPO, newTag);
    }

    private async Task<Release> CreateRelease(GitHubClient client, string version)
    {
        var newRelease = new NewRelease(version)
        {
            Name = $"Version {version}",
            Body = await GetCommitMessagesSinceLastRelease(client),
            Draft = true,
            Prerelease = false
        };

        Logger.Normal($"Creating release {version}");
        return await client.Repository.Release.Create(GIT_OWNER, GIT_REPO, newRelease);
    }

    private async Task<string> GetCommitMessagesSinceLastRelease(GitHubClient client)
    {
        var lastRelease = await client.Repository.Release.GetLatest(GIT_OWNER, GIT_REPO);
        if (lastRelease == null)
        {
            return "Initial release";
        }

        var commits = await client.Repository.Commit.GetAll(GIT_OWNER, GIT_REPO, new CommitRequest
        {
            Since = lastRelease.PublishedAt,
        });
        return String.Join("\r\n", commits.Select(c => $" - {c.Commit.Message}"));
    }

    private async Task UploadRelease(GitHubClient client, Release release, string version)
    {
        using (var archiveContents = File.OpenRead(@"output\clock-setup.exe"))
        { 
            var assetUpload = new ReleaseAssetUpload()
            {
                FileName = $"clock-setup-{version}",
                ContentType = "application/zip",
                RawData = archiveContents
            };
            Logger.Normal($"Uploading release");
            var asset = await client.Repository.Release.UploadAsset(release, assetUpload);
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