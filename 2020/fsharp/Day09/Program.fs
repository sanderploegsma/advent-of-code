module Day09

open Common.Collections
open System.IO

let rec partOne (preamble: int64 list) (input: int64 list) =
    let preamblePairs = combinations 2 preamble
    let isMultipleOfAny x =
        preamblePairs
        |> List.map List.sum
        |> List.exists (fun sum -> sum = x)

    let newPreamble = List.skip 1 preamble
    match input with
    | head :: tail when isMultipleOfAny head -> partOne (newPreamble @ [head]) tail
    | head :: _ -> head

let rec partTwo number input =
    let mutable seen = []
    let mutable position = 0

    while List.sum seen < number && position < List.length input do
        seen <- input.[position] :: seen
        position <- position + 1

    if List.sum seen = number then
        List.min seen + List.max seen
    else
        partTwo number (List.skip 1 input)

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.map int64
        |> Seq.toList

    let found = partOne (List.take 25 input) (List.skip 25 input)
    printfn "Part one: %d" found
    partTwo found input |> printfn "Part two: %d"
    0
