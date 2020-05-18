
var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");

Information($"Running target {target} in configuration {configuration}");

var distDirectory = Directory("./build");
var packageDirectory = Directory("./package");

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(distDirectory);
    });

Task("Restore")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

 Task("Build")
    .Does(() =>
    {
        DotNetCoreBuild("./src/Threenine.Data.csproj",
            new DotNetCoreBuildSettings()
            {
                Configuration = configuration,
                ArgumentCustomization = args => args.Append("--no-restore"),
            });
    });

Task("Test")
    .Does(() =>
    {
        var projects = GetFiles("./tests/Threenine.Data.Tests/Threenine.Data.Tests.csproj");
        foreach(var project in projects)
        {
            Information("Testing project " + project);
            DotNetCoreTest(
                project.ToString(),
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true,
                    ArgumentCustomization = args => args.Append("--no-restore"),
                });
        }
    });
    
  Task("Package")
    .Does(() => 
    {
        var settings = new DotNetCorePackSettings
             {
                 Configuration = configuration,
                 OutputDirectory = packageDirectory
             };
    
          DotNetCorePack("./src/Threenine.Data.csproj", settings);
    });

Task("BuildTestDeploy")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Package");


Task("Default")
    .IsDependentOn("BuildTestDeploy");
  
RunTarget(target);