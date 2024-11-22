module Day03

open System.IO
open System

type Triangle = int * int * int

let parseLine (line: string) =
    line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
    |> Array.map (fun s -> s.Trim())
    |> Array.map int

let readH (data: int[][]) =
    let readRow row =
        (data.[row].[0], data.[row].[1], data.[row].[2])
    seq { 0 .. Array.length data - 1}
    |> Seq.map readRow
    |> Seq.toList

let readV (data: int[][]) =
    let numRows = Array.length data
    let readCol row col =
        (data.[row].[col], data.[row+1].[col], data.[row+2].[col])
    seq { 0 .. 3 .. numRows * 3 - 1 }
    |> Seq.map (fun row -> readCol (row % numRows) (row / numRows))
    |> Seq.toList

let isValid (a, b, c) =
    a + b > c &&
    a + c > b &&
    b + c > a

let countValid = List.filter isValid >> List.length

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.map parseLine
        |> Seq.toArray

    input |> readH |> countValid |> printfn "Part one: %d"
    input |> readV |> countValid |> printfn "Part one: %d"
    0
