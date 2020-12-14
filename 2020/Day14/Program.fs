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

let mask1 (m, v) = if m = 'X' then v else m
let mask2 (m, v) = if m = '0' then v else m

let applyMask fn mask value =
    value
    |> Seq.zip mask
    |> Seq.map fn
    |> Seq.fold (sprintf "%s%c") ""

/// Generate all possible values with the given floating bit positions.
/// Example: floating bits 2, 5 and 7 would result in
/// 00000000, 00000001, 00000100, 000000101, 00100000, 00100001, etc
let generateValues floatingBits =
    let length = Seq.length floatingBits

    let fill values =
        Seq.replicate 36 '0'
        |> Seq.indexed
        |> Seq.mapFold (fun b (i, v) -> if Seq.contains i floatingBits then (Seq.head b, Seq.skip 1 b) else (v, b)) values
        |> fst

    seq { 0 .. (pown 2 length) - 1 }
    |> Seq.map (fun i -> Convert.ToString(i, 2).PadLeft(length, '0'))
    |> Seq.map fill

type State = Map<int64, int64> * string

let apply1 (memory, mask) instruction =
    let maskFn (value: int64) = 
        Convert.ToString(value, 2).PadLeft(36, '0')
        |> applyMask mask1 mask
        |> fun s -> Convert.ToInt64(s, 2)

    let assign address value =
        memory |> Map.add address (maskFn value)

    match instruction with
    | Mask m -> memory, m
    | Assignment (address, value) -> (assign address value), mask

let apply2 (memory, mask) instruction =
    let maskFn (address: int64) =
        let addressMask = 
            Convert.ToString(address, 2).PadLeft(36, '0') 
            |> applyMask mask2 mask
        
        let floatingBits = 
            addressMask 
            |> Seq.indexed 
            |> Seq.filter (fun (_, c) -> c = 'X') 
            |> Seq.map fst 
            |> Seq.toList

        generateValues floatingBits
        |> Seq.map (applyMask mask1 addressMask)
        |> Seq.map (fun s -> Convert.ToInt64(s, 2))

    let assign address value =
        maskFn address
        |> Seq.fold (fun mem addr -> Map.add addr value mem) memory

    match instruction with
    | Mask m -> memory, m
    | Assignment (address, value) -> (assign address value), mask

let run (apply: State -> Instruction -> State) input =
    input
    |> Seq.fold apply (Map.empty, "")
    |> fst
    |> Map.toSeq
    |> Seq.sumBy snd

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.map parse
        |> Seq.toList
        
    input |> run apply1 |> printfn "Part one: %d"
    input |> run apply2 |> printfn "Part two: %d"
    0
