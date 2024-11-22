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
    | _ -> failwithf "Invalid instruction %s" line

let toMask mapping =
    Seq.map mapping
    >> Seq.fold (sprintf "%s%c") ""
    >> fun s -> Convert.ToInt64(s, 2)

let partOne input =
    let apply (memory, m0, m1) instruction =
        let assign address value = memory |> Map.add address ((value &&& m0) ||| m1)

        match instruction with
        | Mask m ->
            let mask0 = toMask (fun c -> if c = 'X' then '0' else '1') m
            let mask1 = toMask (fun c -> if c = '1' then '1' else '0') m
            memory, mask0, mask1

        | Assignment (address, value) -> (assign address value), m0, m1

    let initial = Map.empty, 0L, 0L
    let (memory, _, _) = input |> Seq.fold apply initial

    Map.toSeq memory |> Seq.sumBy snd

let partTwo input =
    let apply (memory, m0, m1s) instruction =
        let assign (address: int64) value =
            m1s
            |> Seq.map (fun m1 -> (address &&& m0) ||| m1)
            |> Seq.fold (fun mem addr -> Map.add addr value mem) memory

        match instruction with
        | Mask m ->
            let mask0 = toMask (fun c -> if c = 'X' then '0' else '1') m
            let mask1 = toMask (fun c -> if c = '1' then '1' else '0') m

            let floating =
                Seq.rev m
                |> Seq.indexed
                |> Seq.filter (fun (_, c) -> c = 'X')
                |> Seq.map fst
                |> Seq.map (fun i -> 1L <<< i)

            let mask1s =
                (Seq.toList >> sublists) floating
                |> List.map (List.fold (|||) 0L)
                |> List.map ((|||) mask1)

            memory, mask0, mask1s

        | Assignment (address, value) -> (assign address value), m0, m1s

    let initial = Map.empty, 0L, [0L]
    let (memory, _, _) = input |> Seq.fold apply initial

    Map.toSeq memory |> Seq.sumBy snd

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.map parse
        |> Seq.toList

    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
