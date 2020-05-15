
var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");

Information($"Running target {target} in configuration {configuration}");

var distDirectory = Directory("./build");
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
        DotNetCoreBuild(".",
            new DotNetCoreBuildSettings()
            {
                Configuration = configuration,
                ArgumentCustomization = args => args.Append("--no-restore"),
            });
    });

Task("Test")
    .Does(() =>
    {
        var projects = GetFiles("./tests/**/*.csproj");
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

Task("BuildAndTest")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test");


Task("Default")
    .IsDependentOn("BuildAndTest");
  
RunTarget(target);