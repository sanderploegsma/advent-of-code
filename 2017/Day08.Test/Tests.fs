module Tests

open Day08
open FsUnit.Xunit
open Xunit

[<Fact>]
let ``Input parsing`` () =
    parseInstruction "a inc 10 if b < 2" |> should equal (Increment ("a", 10), LessThan ("b", 2))
    parseInstruction "a inc -10 if b != -2" |> should equal (Increment ("a", -10), NotEqualTo ("b", -2))

[<Fact>]
let ``Example input``() =
    let input = [
        (Increment ("b", 5), GreaterThan ("a", 1))
        (Increment ("a", 1), LessThan ("b", 5))
        (Decrement ("c", -10), GreaterThanOrEqualTo ("a", 1))
        (Increment ("c", -20), EqualTo ("c", 10))
    ]

    let (memory, limit) = eval input Map.empty 0
    maxRegisterValue memory |> should equal 1
    limit |> should equal 10