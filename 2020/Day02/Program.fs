module Day02

open System.IO
open System.Text.RegularExpressions

type Policy = int * int * char

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)
    if m.Success
    then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

let parse line =
    match line with
    | Regex @"([0-9]+)-([0-9]+) ([a-z]{1}): ([a-z]+)" [ a; b; character; password ] ->
        ((int a, int b, char character), password)
    | _ -> failwithf "Invalid line %s" line

let validateMinMaxCharacterCount ((min, max, char), password: string) =
    let characterCount =
        password
        |> Seq.filter (fun c -> c = char)
        |> Seq.length

    characterCount >= min && characterCount <= max

let validateCharacterPosition ((posA, posB, char), password: string) =
    let charA = password.[posA - 1]
    let charB = password.[posB - 1]
    charA <> charB && (charA = char || charB = char)

let validate validator input =
    input |> List.filter validator |> List.length

let partOne = validate validateMinMaxCharacterCount
let partTwo = validate validateCharacterPosition

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("input.txt")
        |> Seq.map parse
        |> Seq.toList

    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
