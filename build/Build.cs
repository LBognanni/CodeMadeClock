using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Tools.GitVersion;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Microsoft.Build.Tasks;
using System.IO;
using System;
using System.Linq;
using Octokit;
using System.Threading.Tasks;

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
    
    [GitRepository] 
    readonly GitRepository GitRepository;

    [GitVersion(Framework = "netcoreapp3.1", UpdateAssemblyInfo = true, UpdateBuildNumber = true)]
    readonly GitVersion GitVersion;
    private readonly string GIT_OWNER = "lbognanni";
    private readonly string GIT_REPO = "CodeMade.Clock";

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


    Target Setup => _ => _
        .DependsOn(Compile)
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
            var process = System.Diagnostics.Process.Start(isccPath, "setup.iss");
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
            if (!GitRepository.IsOnMasterBranch())
            {
                throw new Exception("Releases can only be generated from the master branch");
            }
            var githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            var tokenAuth = new Credentials(githubToken);
            var client = new GitHubClient(new ProductHeaderValue("build"));
            client.Credentials = tokenAuth;

            var tag = await CreateTag(client);
            var release = await CreateRelease(client, tag.Tag);
            await UploadRelease(client, release, tag.Tag);
        });

    private async Task<GitTag> CreateTag(GitHubClient client)
    {
        var tag = GitVersion.AssemblySemVer;
        var sha = GitVersion.Sha;
        var newTag = new NewTag
        {
            Message = "",
            Object = sha,
            Tag = tag,
            Type = TaggedType.Commit,
            Tagger = new Committer("Loris Bognanni", "loris@codemade.co.uk", DateTimeOffset.Now)
        };
        return await client.Git.Tag.Create(GIT_OWNER, GIT_REPO, newTag);
    }

    private async Task<Release> CreateRelease(GitHubClient client, string version)
    {
        var newRelease = new NewRelease($"v{version}");
        newRelease.Name = $"Version {version}";
        newRelease.Body = "Please see [the official page](https://www.codemade.co.uk/clock) for release notes.";
        newRelease.Draft = true;
        newRelease.Prerelease = false;

        return await client.Repository.Release.Create(GIT_OWNER, GIT_REPO, newRelease);
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
            var asset = await client.Repository.Release.UploadAsset(release, assetUpload);
        }
    }
}
