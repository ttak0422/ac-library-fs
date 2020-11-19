module AC.EnumerationTest

open System
open Expecto
open AC 

[<Tests>]
let permutationTests = 
    testList "順列" [
        test "要素が空のとき" {
            let list = []
            let act = Enumeration.permutations list 
            let exp = [[]]
            Expect.equal act exp "[[]]"
        }
        test "[1;2;3]" {
            let list = [1..3]
            let result = Enumeration.permutations list 
            let act1 =
                result
                |> List.distinct
                |> List.length
            let exp1 = 6
            let act2 = 
                result
                |> List.forall (List.forall(fun x -> x = 1 || x = 2 || x = 3))
            let exp2 = true
            Expect.equal act1 exp1 "6通り"
            Expect.equal act2 exp2 "要素は1,2,3のいずれかで構成される"
        }
        test "[2..6]" {
            let list = [2..6]
            let result = Enumeration.permutations list 
            let act1 =
                result
                |> List.distinct
                |> List.length
            let exp1 = 120
            let act2 = 
                let nums = Set.ofList [2..6]
                result
                |> List.forall (List.forall(fun x -> Set.contains x nums))
            let exp2 = true
            Expect.equal act1 exp1 "120通り"
            Expect.equal act2 exp2 "要素は[2..6]のいずれかで構成される"
        }
        test "[\"a\";\"b\";\"c\"]" {
            let list = ["a";"b";"c"]
            let result = Enumeration.permutations list 
            let act1 =
                result
                |> List.distinct
                |> List.length
            let exp1 = 6
            let act2 = 
                result
                |> List.forall (List.forall(fun x -> x = "a" || x = "b" || x = "c"))
            let exp2 = true
            Expect.equal act1 exp1 "6通り"
            Expect.equal act2 exp2 "要素はa,b,cのいずれかの文字列で構成される"
        }
    ]

[<Tests>]
let combinationTests = 
    testList "組み合わせ" [
        test "[1..5]からn個選ぶ" {
            let list = [1..5]

            let act = 
                Enumeration.combination 1 list 
                |> List.map (List.sort)
                |> List.distinct
                |> List.length 
            let exp = 5
            Expect.equal act exp "n=5, r=1"

            let act = 
                Enumeration.combination 2 list 
                |> List.map (List.sort)
                |> List.distinct
                |> List.length 
            let exp = 10
            Expect.equal act exp "n=5, r=2"

            let act = 
                Enumeration.combination 3 list 
                |> List.map (List.sort)
                |> List.distinct
                |> List.length 
            let exp = 
                Enumeration.combination 2 list 
                |> List.map (List.sort)
                |> List.distinct
                |> List.length 
            Expect.equal act exp "(n=5, r=2) = (n=5, r=3)"
        }
        test "[1..n]から0個選ぶ" {
            let list = [1]
            let act = 
                Enumeration.combination 0 list 
                |> List.map (List.sort)
                |> List.distinct
                |> List.length 
            Expect.equal act 1 "from [1]"
             
            let list = [1..3324]
            let act = 
                Enumeration.combination 0 list 
                |> List.map (List.sort)
                |> List.distinct
                |> List.length 
            Expect.equal act 1 "from [1..3324]"
        }
        test "[1..n]からn個選ぶ" {
            let list = [1]
            let act = 
                Enumeration.combination 1 list 
                |> List.map (List.sort)
                |> List.distinct
                |> List.length 
            Expect.equal act 1 "from [1]"
             
            let list = [1..3324]
            let act = 
                Enumeration.combination 3324 list 
                |> List.map (List.sort)
                |> List.distinct
                |> List.length 
            Expect.equal act 1 "from [1..3324]"
        }
        test "['a';'b';'c';'d']からn個選ぶ" {
            let list = ['a';'b';'c';'d']
            let act = 
                Enumeration.combination 2 list 
                |> List.map (List.sort)
                |> List.distinct
                |> List.length 
            Expect.equal act 6 "n=4, r=2"
        }
        test "r, n-r" {
            let list = [1..12]
            let len = list.Length
            seq { 1.. 12 }
            |> Seq.iter (fun x ->
                let act = 
                    Enumeration.combination x list 
                    |> List.map (List.sort)
                    |> List.distinct
                    |> List.length 
                let exp = 
                    Enumeration.combination (list.Length - x) list 
                    |> List.map (List.sort)
                    |> List.distinct
                    |> List.length 
                Expect.equal act exp (sprintf "r=%i, n-r=%i" x (len - x))
            )
        }
    ]