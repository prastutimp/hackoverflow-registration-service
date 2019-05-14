///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "BuildAndPublish");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// VARIABLES
///////////////////////////////////////////////////////////////////////////////

const string SolutionName = "./HackOverflow.sln";

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Restore")
   .Does(() =>
{
   DotNetCoreRestore(SolutionName);
});

Task("Build")
   .Does(() =>
{
   DotNetCoreBuild(
      SolutionName,
      new DotNetCoreBuildSettings
      {
         Configuration = configuration
      });
});

Task("Publish")
   .Does(() =>
{
   DotNetCorePublish(
      "./src/Api.Http/Api.Http.csproj",
      new DotNetCorePublishSettings
      {
         Configuration = configuration,
         OutputDirectory = "./.publish"
      });
});

Task("BuildAndPublish")
   .IsDependentOn("Restore")
   .IsDependentOn("Build")
   .IsDependentOn("Publish");

RunTarget(target);