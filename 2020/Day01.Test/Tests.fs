module Day01.Test

open Day01
open Xunit

let example = [1721; 979; 366; 299; 675; 1456]

[<Fact>]
let ``Part One`` () =
    Assert.Equal(514579, (partOne example))
    
[<Fact>]
let ``Part Two`` () =
    Assert.Equal(241861950, (partTwo example))