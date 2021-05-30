using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations(TimeoutInMilliseconds = 1_500)]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    AbsolutePath TestsDirectory = RootDirectory / "BESL.Application.Tests";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            RootDirectory.GlobDirectories("BESL.**/bin", "BESL.**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Clean, Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target RunUnitTests => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var testProjects = GlobFiles(TestsDirectory, "**/*.csproj");

            DotNetTest(s => s
                .EnableNoBuild()
                .EnableCollectCoverage()
                .SetCoverletOutputFormat(CoverletOutputFormat.opencover)
                .SetTestAdapterPath(".")
                .CombineWith(csw => testProjects
                    .Select(p =>
                    {
                        string assemblyName = Path.GetFileNameWithoutExtension(p);
                        Logger.Normal($"Adding test congiruations for assembly: '{assemblyName}'");
                        string projectRoutePrefix = ArtifactsDirectory / assemblyName;

                        return csw
                            .SetProjectFile(p)
                            .SetLogger($"xunit;LogFilePath={projectRoutePrefix}_testresults.xml")
                            .SetCoverletOutput($"{projectRoutePrefix}_coverage.xml");
                    })), Environment.ProcessorCount, true);
        });
}