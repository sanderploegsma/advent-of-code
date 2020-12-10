namespace UnitTests

module RegularExpressions =
    open Common.RegularExpressions
    open FsUnit.Xunit
    open Xunit

    type Type = 
        | String of string 
        | Number of int 
        | Boolean of bool

    [<Fact>]
    let ``Matching regular expressions`` () =
        let getMatch exp =
            match exp with
            | Regex @"^(\d+)$" [x] -> Number (int x)
            | Regex @"^true$" [] -> Boolean true
            | Regex @"^false" [] -> Boolean false
            | x -> String x

        getMatch "123456" |> should equal (Number 123456)
        getMatch "true" |> should equal (Boolean true)
        getMatch "false" |> should equal (Boolean false)
        getMatch "test" |> should equal (String "test")