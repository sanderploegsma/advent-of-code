module Tests

open Day03
open Xunit
open FsUnit.Xunit

[<Theory>]
[<InlineData(1, 0)>]
[<InlineData(12, 3)>]
[<InlineData(23, 2)>]
[<InlineData(1024, 31)>]
let ``PartOne example`` n expected =
    partOne n |> should equal expected
