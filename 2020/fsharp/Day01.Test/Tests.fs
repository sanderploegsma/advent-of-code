module Day01.Test

open Day01
open FsUnit.Xunit
open Xunit

let example = [1721; 979; 366; 299; 675; 1456]

[<Fact>]
let ``Part One`` () =
    partOne example |> should equal 514579

[<Fact>]
let ``Part Two`` () =
    partTwo example |> should equal 241861950
