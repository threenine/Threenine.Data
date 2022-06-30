#tool "dotnet:?package=GitVersion.Tool&version=5.10.3"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
string version = String.Empty;

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() => {
    DotNetClean("Threenine.Data.sln");
});

Task("Version")
  .IsDependentOn("Clean")
    .Does(() =>
{
   var result = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = true
    });
    
    version = result.NuGetVersionV2;
    Information($"Nuget Version: { version.ToString() }");
    Information($"Semantic Version: { result.FullSemVer.ToString() }");
});

Task("Restore")
    .Description("Restoring the solution dependencies")
    .Does(() => {
    
    var projects = GetFiles("**/**/*.csproj");

    foreach(var project in projects )
    {
      Information($"Restoring { project.ToString()}");
      DotNetRestore(project.ToString());
    }

});

Task("Build")
    .IsDependentOn("Version")
    .Does(() => {
     var buildSettings = new DotNetBuildSettings {
                        Configuration = configuration,
                       };
     var projects = GetFiles("**/**/*.csproj");
     foreach(var project in projects )
     {
         Information($"Building {project.ToString()}");
         DotNetBuild(project.ToString(),buildSettings);
     }
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {

       var testSettings = new DotNetTestSettings  {
          Configuration = configuration,
          NoBuild = true,
      };
      
     var projects = GetFiles("./tests/Unit/*.csproj");
     foreach(var project in projects )
     {
       Information($"Running Tests : { project.ToString()}");
       DotNetTest(project.ToString(), testSettings );
     }
});

Task("Pack")
 .IsDependentOn("Test")
 .Does(() => {
 
   var settings = new DotNetPackSettings
    {
        Configuration = configuration,
        OutputDirectory = "./.artifacts",
        MSBuildSettings = new DotNetMSBuildSettings()
                        .WithProperty("PackageVersion", version)
                        .WithProperty("Copyright", $"© Copyright Threenine.co.uk {DateTime.Now.Year}")
                        .WithProperty("Version", version)
    };
    
    DotNetPack("Threenine.Data.sln", settings);
 
 });
 
 Task("Publish")
  .IsDependentOn("Pack")
  .Does(context => {
  
  
    foreach(var file in GetFiles("./.artifacts/*.nupkg"))
     {
       Information("Publishing {0}...", file.GetFilename().FullPath);
       DotNetNuGetPush(file, new DotNetNuGetPushSettings {
             ApiKey = context.EnvironmentVariable("NUGET_API_KEY"),
             Source = "https://api.nuget.org/v3/index.json"
       });
      
   }
});




//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

Task("Default")
       .IsDependentOn("Clean")
       .IsDependentOn("Restore")
       .IsDependentOn("Version")
       .IsDependentOn("Build")
       .IsDependentOn("Test")
       .IsDependentOn("Pack")
       .IsDependentOn("Publish");

RunTarget(target);
