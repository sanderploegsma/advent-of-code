module Day05

open System.IO

let traverse updateOffset input =
    let mutable memory = List.toArray input
    let mutable position = 0
    let mutable jumps = 0
    
    while position < Array.length memory do
        let offset = memory.[position]
        memory.[position] <- updateOffset offset
        position <- position + offset
        jumps <- jumps + 1
    jumps

let partOne = traverse (fun offset -> offset + 1)
let partTwo = traverse (fun offset -> if offset >= 3 then offset - 1 else offset + 1)

[<EntryPoint>]
let main argv =
    let input = File.ReadLines("input.txt") |> Seq.map int |> Seq.toList
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
