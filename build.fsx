// include Fake lib
#r @"packages/FAKE.4.28.0/tools/FakeLib.dll"
open Fake

let projectName = "set"
let srcTarget = sprintf "./%s/**/*.fsproj" projectName
let buildDir = "./build"

Target "Clean" (fun _ ->
    CleanDirs [buildDir;]
)

Target "Build" (fun _ ->
  !! srcTarget
     |> MSBuildDebug buildDir "Build"
     |> Log "AppBuild-Output: "
)

Target "Test" (fun _ ->
    !! (sprintf "%s/%s.dll" buildDir projectName)
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true;
             OutputFile = buildDir + "/TestResults.xml" })
)

Target "Default" (fun _ ->
    trace "Building project"
)

"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
