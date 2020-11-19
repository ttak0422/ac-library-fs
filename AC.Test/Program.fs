module AC.Test

open System
open Expecto
open Expecto
open AC 

[<Tests>]
let libraryTests = 
    testList "AC.Library" [
        test "add" {
            Expect.equal (Library.add 1 2) 3 "1+2"
        }
        testProperty "交換法則" <| fun a b -> 
            Library.add a b = Library.add b a
    ]

[<EntryPoint>]
let main argv =
    runTestsInAssembly defaultConfig argv