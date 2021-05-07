module Day09

open System.IO

let decompress (sequence: string) =
    let mutable result = ""
    let mutable position = 0
    while position < sequence.Length do
        if sequence.[position] <> '(' then
            result <- result + sequence.Substring(position, 1)
            position <- position + 1
        else
            let markerEnd = sequence.IndexOf(')', position)
            let marker = sequence.Substring(position, markerEnd - position + 1).TrimStart('(').TrimEnd(')').Split('x')
            let (length, repeat) = (int marker.[0], int marker.[1])

            for _ in 1 .. repeat do
                result <- result + sequence.Substring(markerEnd + 1, length)
            position <- markerEnd + length + 1
    result

let rec decompressV2 (sequence: string) =
    let mutable position = 0
    let mutable size = 0L
    while position < sequence.Length do
        if sequence.[position] <> '(' then
            position <- position + 1
            size <- size + 1L
        else
            let markerEnd = sequence.IndexOf(')', position)
            let marker = sequence.Substring(position, markerEnd - position + 1).TrimStart('(').TrimEnd(')').Split('x')
            let (length, repeat) = (int marker.[0], int marker.[1])

            let subseq = decompressV2 (sequence.Substring(markerEnd + 1, length))
            for _ in 1 .. repeat do
                size <- size + subseq
            position <- markerEnd + length + 1
    size

let partOne input = (decompress input).Length
let partTwo input = decompressV2 input

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText("Input.txt")
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
