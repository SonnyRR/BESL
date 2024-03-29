using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using Polly;
using System;
using System.IO;
using System.Linq;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tooling.ProcessTasks;
using static Nuke.Common.Tools.Docker.DockerTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.GitHub.GitHubTasks;

[CheckBuildProjectConfigurations(TimeoutInMilliseconds = 1_500)]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    public Build()
    {
        // Redirect output from STERR to STDOUT.
        DockerLogger = (_, message) => Logger.Normal(message);
    }

    public static int Main() => Execute<Build>(x => x.Compile);

    readonly string GitHubImageRegistry = "docker.pkg.github.com";
    readonly string ImageName = "besl.web";
    static bool IsHostGitHubAction => Host is GitHubActions;

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("The PAT used in order to push the Docker image to the container registry as an owner of the repository")]
    readonly string GitHubPersonalAccessToken;

    [Parameter("The GitHub user account that will be used to push the Docker image to the container registry")]
    readonly string GitHubUsername;

    [Parameter("The CodeCov token used for authenticating when uploading coverage reports.")]
    readonly string CodeCovToken;

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
                            .SetLoggers($"xunit;LogFilePath={projectRoutePrefix}_testresults.xml")
                            .SetCoverletOutput($"{projectRoutePrefix}_coverage.xml");
                    })), Environment.ProcessorCount, true);
        });

    Target BuildDockerImage => _ => _
        .Executes(() =>
        {
            DockerBuild(s => s
                .SetProcessEnvironmentVariable("DOCKER_BUILDKIT", "1")
                .SetProcessWorkingDirectory(RootDirectory)
                .SetFile(RootDirectory / "Dockerfile")
                .SetPath(".")
                .SetTag(ImageName));
        });

    Target PublishDockerImage => _ => _
        .DependsOn(BuildDockerImage)
        .OnlyWhenDynamic(() => GitRepository.IsOnMasterBranch())
        .Requires(
            () => GitHubUsername,
            () => GitHubPersonalAccessToken)
        .Executes(() =>
        {
            var (repositoryOwner, repositoryName) = GetGitHubRepositoryInfo(GitRepository);

            DockerLogin(s => s
                .SetServer(GitHubImageRegistry)
                .SetUsername(GitHubUsername)
                .SetPassword(GitHubPersonalAccessToken)
                .EnableProcessLogOutput());

            PushDockerImageWithTag("latest", repositoryOwner, repositoryName);
        });

    Target UploadCodeCoverageArtifact => _ => _
        .Requires(() => CodeCovToken)
        .After(RunUnitTests)
        .OnlyWhenStatic(() => GitRepository.IsOnMasterBranch())
        .Executes(() =>
        {
            Policy
                .HandleResult<int>(ec => ec == 1)
                .WaitAndRetry(5,
                    ra => TimeSpan.FromSeconds(Math.Pow(2, ra)),
                    (ec, timeSpan, retryCount, context) =>
                        {
                            Logger.Normal($"Process exited with code: '{ec}'");
                            Logger.Normal($"Attempting to fetch the 'CodeCov' uploader. Try: {retryCount}");
                        })
                .Execute(() => FetchCodeCovUploader(RootDirectory));

            var scriptPath = Directory
                .GetFiles(RootDirectory, "codecov*")
                .SingleOrDefault();

            ControlFlow.Assert(!string.IsNullOrWhiteSpace(scriptPath), "'CodeCov' uploader is not present.");

            if (string.IsNullOrWhiteSpace(Path.GetExtension(scriptPath)))
            {
                Logger.Normal("Adding executable permissions to the CodeCov shell script.");
                using var changePermissionsProcess = StartShell($"chmod +x {scriptPath}");
            }

            using var coverageReportUploadProcess = StartShell($@"{scriptPath} -t {CodeCovToken}", RootDirectory, logOutput: true);
            coverageReportUploadProcess.WaitForExit();
        });

    int FetchCodeCovUploader(AbsolutePath downloadPath)
    {
        if (string.IsNullOrWhiteSpace(downloadPath))
        {
            throw new ArgumentNullException(nameof(downloadPath));
        }

        var process = Environment.OSVersion.Platform switch
        {
            PlatformID.Win32NT => StartProcess("powershell", "Invoke-WebRequest -Uri https://uploader.codecov.io/latest/windows/codecov.exe -Outfile codecov.exe", RootDirectory, logOutput: true),
            PlatformID.Unix => StartShell("curl -Os https://uploader.codecov.io/latest/linux/codecov", RootDirectory, logOutput: true),
            PlatformID.MacOSX => StartShell("curl -Os https://uploader.codecov.io/latest/macos/codecov", RootDirectory, logOutput: true),
            _ => throw new PlatformNotSupportedException()
        };

        process.WaitForExit();
        return process.ExitCode;
    }
    void PushDockerImageWithTag(string tag, string repositoryOwner, string repositoryName)
    {
        tag = tag?.ToLower();
        repositoryOwner = repositoryOwner?.ToLower();
        repositoryName = repositoryName?.ToLower();

        var targetImageName = $"{GitHubImageRegistry}/{repositoryOwner}/{repositoryName}/{ImageName}:{tag}";

        DockerTag(s => s
            .SetSourceImage(ImageName)
            .SetTargetImage(targetImageName));

        Logger.Normal($"Pushing Docker image with tag: {tag}");
        DockerPush(s => s.SetName(targetImageName));
    }
}