#tool "nuget:?package=JetBrains.dotCover.CommandLineTools"
var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");

Information($"Running target {target} in configuration {configuration}");

var distDirectory = Directory("./build");
var packageDirectory = Directory("./package");
var temporaryFolder = Directory("./temp");
TaskSetup(setupContext =>
{
   if(TeamCity.IsRunningOnTeamCity)
   {
      TeamCity.WriteStartBuildBlock(setupContext.Task.Description ?? setupContext.Task.Name);
   }
});

TaskTeardown(teardownContext =>
{
   if(TeamCity.IsRunningOnTeamCity)
   {
      TeamCity.WriteEndProgress(teardownContext.Task.Description ?? teardownContext.Task.Name);
   }
});

Task("Clean")
    .Description("Cleaning the solution directory")
    .Does(() =>
    {
        CleanDirectory(distDirectory);
    });

Task("Restore")
    .Description("Restoring the solution dependencies")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

 Task("Build")
    .Does(() =>
    {
    
       var buildSettings =  new DotNetCoreBuildSettings { 
                                                          Configuration = configuration,
                                                          ArgumentCustomization = args => args.Append("--no-restore"),
                                                         };
        var projects = GetFiles("./**/*.csproj");
        
        foreach(var project in projects)
        {
           Information("Building Project: " + project);
           DotNetCoreBuild(project.ToString(), buildSettings);
        }
      });

Task("Test")
    .Does(() =>
    {
        var projects = GetFiles("./tests/**/*.Tests.csproj");
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
 Task("TestCoverage")
 .Does(() => {
  var coverageResultFile = System.IO.Path.Combine(temporaryFolder, "coverageResult.dcvr");
 
    var testDllsPattern = string.Format("./**/bin/{0}/*.*Tests.dll", configuration);
 
    var testDlls = GetFiles(testDllsPattern);
 
    var testResultsFile = System.IO.Path.Combine(temporaryFolder, "testResults.trx");
 
    DotCoverCover(tool => {
          tool.MSTest(testDlls, new MSTestSettings() {
             ResultsFile = testResultsFile
          });
       },
       new FilePath(coverageResultFile),
       new DotCoverCoverSettings()
          .WithFilter("+:Application")
          .WithFilter("-:Application.*Tests")
       );
 
    if(TeamCity.IsRunningOnTeamCity)
    {
       TeamCity.ImportData("mstest", testResultsFile);
 
       TeamCity.ImportDotCoverCoverage(coverageResultFile);
    }
 });  
Task("Default")
       .IsDependentOn("Clean")
       .IsDependentOn("Restore")
       .IsDependentOn("Build")
       .IsDependentOn("Test");
  
RunTarget(target);

