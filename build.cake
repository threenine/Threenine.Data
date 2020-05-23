
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
    
       var buildSettings =  new DotNetCoreBuildSettings { Configuration = configuration,
                                                          ArgumentCustomization = args => args.Append("--no-restore"),
                                                         };
                                       
        Information("Building Test Database Project");                               
        DotNetCoreBuild("./tests/Database/TestDatabase.csproj", buildSettings);
        Information("Building Unit Test Project Project");  
        DotNetCoreBuild("./tests/Threenine.Data.Tests/Threenine.Data.Tests.csproj", buildSettings);
        Information("Building Threenine.Data Project");  
        DotNetCoreBuild("./src/Threenine.Data.csproj", buildSettings);
           
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
                    Configuration = configuration
                });
        }
    });
    
Task("Default")
       .IsDependentOn("Clean")
       .IsDependentOn("Restore")
       .IsDependentOn("Build")
       .IsDependentOn("Test");
  
RunTarget(target);