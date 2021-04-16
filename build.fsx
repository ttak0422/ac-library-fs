#r "paket:
nuget FSharp.Core ~> 4.7.0
nuget Fake.Core.Target
nuget Fake.Core.Process
nuget Fake.DotNet.Cli
nuget Fake.Core.ReleaseNotes
nuget Fake.DotNet.AssemblyInfoFile
nuget Fake.DotNet.Paket
nuget Fake.Tools.Git
nuget Fake.Core.Environment
nuget Fake.Core.UserInput
nuget Fake.IO.FileSystem
nuget Fake.DotNet.MsBuild
nuget Fantomas.Extras
nuget Fake.Api.GitHub //"
#load "./.fake/build.fsx/intellisense.fsx"
#if !FAKE
#r "netstandard"
#endif

open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators
open Fantomas
open Fantomas.Extras 
open Fantomas.FormatConfig

Target.initEnvironment ()

Target.create "Clean" (fun _ -> !! "./**/bin" ++ "./**/obj" |> Shell.cleanDirs)

Target.create "Restore"
<| fun _ -> !! "./**/*.fsproj" |> Seq.iter (DotNet.restore id)

Target.create
    "Build"
    (fun _ ->
        DotNet.test
            (fun p ->
                { p with
                      Configuration = DotNet.BuildConfiguration.Debug })
            "./AC/AC.fsproj")

Target.create
    "Test"
    (fun _ ->
        DotNet.test
            (fun p ->
                { p with
                      Configuration = DotNet.BuildConfiguration.Debug })
            "./AC.Test/AC.Test.fsproj")

Target.create "CodeFormat" (fun _ ->
  !!"./**/*.fs"
  |> FakeHelpers.formatCode
  |> Async.RunSynchronously
  |> printfn "Formatted files: %A"
)

Target.create "All" ignore

"Clean" ==> "Restore" ==> "CodeFormat" ==> "Test" ==> "All"

Target.runOrDefault "All"
