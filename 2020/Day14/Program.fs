module Day14

open Common.Collections
open Common.RegularExpressions
open System
open System.IO

type Instruction = 
    | Mask of string
    | Assignment of int64 * int64

let parse line =
    match line with
    | Regex @"mask = ([X01]{36})" [mask] -> Mask mask
    | Regex @"mem\[(\d+)\] = (\d+)" [address; value] -> Assignment (int64 address, int64 value)

let partOne input =
    let mutable memory = Map.empty
    let mutable mask = ""

    let calculateValue (value: int64) =
        Convert.ToString(value, 2).PadLeft(36, '0')
        |> Seq.zip mask
        |> Seq.map (fun (m, v) -> if m = 'X' then v else m)
        |> Seq.fold (sprintf "%s%c") ""
        |> fun s -> Convert.ToInt64(s, 2)

    for i in 0 .. List.length input - 1 do
        match input.[i] with
        | Mask m -> mask <- m
        | Assignment (address, value) -> memory <- memory.Add(address, calculateValue value)

    Map.toSeq memory
    |> Seq.sumBy snd

let partTwo input =
    let mutable memory = Map.empty
    let mutable mask = ""

    let calculateAddresses (address: int64) =
        let address' = 
            Convert.ToString(address, 2).PadLeft(36, '0')
            |> Seq.zip mask
            |> Seq.map (fun (m, v) -> if m = '0' then v else m)
            |> Seq.fold (sprintf "%s%c") ""

        let floating = address' |> Seq.filter (fun c -> c = 'X') |> Seq.length
        seq {
            for i in 0 .. (pown 2 floating) - 1 do
                let bits = Convert.ToString(i, 2).PadLeft(floating, '0')
                address'
                |> Seq.mapFold (fun (b: string) c -> if c = 'X' then (Seq.head b, b.Substring(1)) else (c, b)) bits
                |> fst
                |> Seq.fold (sprintf "%s%c") ""
                |> fun s -> Convert.ToInt64(s, 2)
        }
    
    for i in 0 .. List.length input - 1 do
        match input.[i] with
        | Mask m -> mask <- m
        | Assignment (address, value) ->
            calculateAddresses address
            |> Seq.iter (fun address' -> memory <- memory.Add(address', value))

    Map.toSeq memory
    |> Seq.sumBy snd

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.map parse
        |> Seq.toList
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
