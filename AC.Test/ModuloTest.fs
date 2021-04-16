module AC.ModuloTest

open System
open Expecto
open AC

let mod' = 71
let mod'' = int64 1e9 + 7L


[<Tests>]
let modPownTests =
    testList
        "modPown"
        [ test "tiny case 1" {
              let act = Modulo.modPown mod' 123 4
              let exp = (pown 123 4) % mod'
              Expect.equal act exp "(123 ^ 4) % 71"
          }
          test "tiny case 2" {
              let act = Modulo.modPown mod' 543 2
              let exp = (pown 543 2) % mod'
              Expect.equal act exp "(543 ^ 2) % 71"
          }
          test "tiny case 3" {
              let act = Modulo.modPown mod' 0 123
              let exp = (pown 0 123) % mod'
              Expect.equal act exp "(0 ^ 123) % 71"
          }
          test "large case 1" {
              let act = Modulo.modPown mod'' 123L 45 |> bigint

              let exp =
                  Numerics.BigInteger.ModPow(bigint 123, bigint 45, bigint mod'')

              Expect.equal act exp "(123 ^ 45) % (1e9 + 7)"
          }
          test "large case 2" {
              let act = Modulo.modPown mod'' 543L 21 |> bigint

              let exp =
                  Numerics.BigInteger.ModPow(bigint 543, bigint 21, bigint mod'')

              Expect.equal act exp "(543 ^ 21) % (1e9 + 7)"
          } ]
