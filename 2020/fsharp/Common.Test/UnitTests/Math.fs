namespace UnitTests

module Math =

    open Common.Math
    open FsUnit.Xunit
    open Xunit

    [<Theory>]
    [<InlineData(1, 1, 1)>]
    [<InlineData(1, 2, 1)>]
    [<InlineData(2, 4, 2)>]
    [<InlineData(6, 9, 3)>]
    [<InlineData(21, 2, 1)>]
    let ``Greatest Common Divisor`` a b expected =
        gcd a b |> should equal expected

    [<Theory>]
    [<InlineData(3, 2, 6)>]
    [<InlineData(4, 5, 20)>]
    [<InlineData(1, 1, 1)>]
    [<InlineData(2, 4, 4)>]
    let ``Least Common Multiple`` a b expected =
        lcm a b |> should equal expected
