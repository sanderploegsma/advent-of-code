module Day01.Test

open Day01
open Xunit

[<Fact>]
let ``Part One`` () =
    Assert.Equal("Hello, World!", (partOne "World"))