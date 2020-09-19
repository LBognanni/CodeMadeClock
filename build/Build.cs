using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Microsoft.Build.Tasks;
using System.IO;

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

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;

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


    Target Release => _ => _
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

}
