module Day03

open System.IO

type Direction = North | East | South | West

let parse c =
    match c with
    | '^' -> North
    | '>' -> East
    | 'v' -> South
    | '<' -> West

let nextPosition direction (x, y) =
    match direction with
    | North -> (x, y + 1)
    | East -> (x + 1, y)
    | South -> (x, y - 1)
    | West -> (x - 1, y)

let rec traverse position visited directions =
    let visited' = position :: visited
    match directions with
    | [] -> visited'
    | direction :: tail -> traverse (nextPosition direction position) visited' tail

let rec traverse2 santaPos robotPos visited directions =
    let visited' = santaPos :: robotPos :: visited
    match directions with
    | [] -> visited'
    | santaDir :: robotDir :: tail -> traverse2 (nextPosition santaDir santaPos) (nextPosition robotDir robotPos) visited' tail

let partOne directions =
    let path = traverse (0, 0) [] directions
    List.distinct path |> List.length

let partTwo directions =
    let path = traverse2 (0, 0) (0, 0) [] directions
    List.distinct path |> List.length

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText("Input.txt") |> Seq.map parse |> Seq.toList

    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
