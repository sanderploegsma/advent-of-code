module Day06

open System.IO
open System.Text.RegularExpressions

type Square = (int * int) * (int * int)
type Instruction =
    | TurnOn of Square
    | TurnOff of Square
    | Toggle of Square

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)
    if m.Success
    then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

let parse line =
    match line with
    | Regex @"turn on (\d+),(\d+) through (\d+),(\d+)" [x1; y1; x2; y2] ->
        TurnOn ((int x1, int y1), (int x2, int y2))
    | Regex @"turn off (\d+),(\d+) through (\d+),(\d+)" [x1; y1; x2; y2] ->
        TurnOff ((int x1, int y1), (int x2, int y2))
    | Regex @"toggle (\d+),(\d+) through (\d+),(\d+)" [x1; y1; x2; y2] ->
        Toggle ((int x1, int y1), (int x2, int y2))

let solve input turnOn turnOff toggle =
    let mutable grid = Array.init 1000 (fun _ -> Array.init 1000 (fun _ -> 0))
    let instructions = List.toArray input

    let update ((x1, y1), (x2, y2)) op =
        for x in x1 .. x2 do
            for y in y1 .. y2 do
                grid.[x].[y] <- op grid.[x].[y]

    for i in 0 .. Array.length instructions - 1 do
        match instructions.[i] with
        | TurnOn sq -> update sq turnOn
        | TurnOff sq -> update sq turnOff
        | Toggle sq -> update sq toggle

    Array.sumBy Array.sum grid

let partOne input =
    let turnOn = (fun _ -> 1)
    let turnOff = (fun _ -> 0)
    let toggle = (fun state -> 1 - state)

    solve input turnOn turnOff toggle

let partTwo input =
    let turnOn = (fun state -> state + 1)
    let turnOff = (fun state -> max 0 (state - 1))
    let toggle = (fun state -> state + 2)

    solve input turnOn turnOff toggle

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.map parse
        |> Seq.toList

    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
