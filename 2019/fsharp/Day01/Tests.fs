module AdventOfCode2019.Day01.Tests

open AdventOfCode2019.Day01.Solution
open System.IO
open Xunit

[<Theory>]
[<InlineData(12, 2)>]
[<InlineData(14, 2)>]
[<InlineData(1969, 654)>]
[<InlineData(100756, 33583)>]
let ``Part One - Examples`` mass expected =
    Assert.Equal(expected, calculateFuelRequirements mass)

[<Fact>]
let ``Part One``() =
    let input = File.ReadLines("Day01\\Input.txt") |> Seq.map int
    Assert.Equal(3361976, sumFuelRequirements input)
    
[<Theory>]
[<InlineData(14, 2)>]
[<InlineData(1969, 966)>]
[<InlineData(100756, 50346)>]
let ``Part Two - Examples`` mass expected =
    Assert.Equal(expected, calculateFuelRequirementsRec mass)

[<Fact>]
let ``Part Two``() =
    let input = File.ReadLines("Day01\\Input.txt") |> Seq.map int
    Assert.Equal(5040085, sumFuelRequirementsRec input)