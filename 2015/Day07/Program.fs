module Day07

open System.IO
open System.Text.RegularExpressions

type Identifier = string

type Gate =
    | Signal of Identifier
    | SignalValue of int16
    | And of Identifier * Identifier
    | AndValue of Identifier * int16
    | Or of Identifier * Identifier
    | LeftShift of Identifier * int
    | RightShift of Identifier * int
    | Not of Identifier

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)
    if m.Success
    then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

let parse (line: string) =
    let parseGate gate =
        match gate with 
        | Regex @"^([a-z]+)$" [a] -> Signal a
        | Regex @"^(\d+)$" [a] -> SignalValue (int16 a)
        | Regex @"^([a-z]+) OR ([a-z]+)$" [a; b] -> Or (a, b)
        | Regex @"^([a-z]+) AND ([a-z]+)$" [a; b] -> And (a, b)
        | Regex @"^1 AND ([a-z]+)$" [a] -> AndValue (a, 1s)
        | Regex @"^([a-z]+) LSHIFT (\d+)$" [a; b] -> LeftShift (a, int b)
        | Regex @"^([a-z]+) RSHIFT (\d+)$" [a; b] -> RightShift (a, int b)
        | Regex @"^NOT ([a-z]+)$" [a] -> Not a

    let gateAndId = line.Split(" -> ")
    (gateAndId.[1], parseGate gateAndId.[0])

let rec resolve identifier gates (cache: byref<Map<string, int16>>) =
    if cache.ContainsKey(identifier) then
        cache.[identifier]
    else
        let (_, gate) = List.find (fun (x, _) -> x = identifier) gates
        let value =
            match gate with
            | SignalValue value -> value
            | Signal a -> resolve a gates &cache
            | Or (a, b) -> (resolve a gates &cache) ||| (resolve b gates &cache)
            | And (a, b) -> (resolve a gates &cache) &&& (resolve b gates &cache)
            | AndValue (a, b) -> (resolve a gates &cache) &&& b
            | LeftShift (a, shift) -> (resolve a gates &cache) <<< shift
            | RightShift (a, shift) -> (resolve a gates &cache) >>> shift
            | Not a -> ~~~ (resolve a gates &cache)
        
        cache <- cache.Add(identifier, value) 
        value

let solve input = 
    let mutable cache = Map.empty
    resolve "a" input &cache

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt")
        |> Seq.map parse
        |> Seq.toList
        
    let signalA = solve input
    signalA |> printfn "Part one: %d"

    let newInput = ("b", SignalValue signalA) :: List.filter (fun (x, _) -> x <> "b") input
    solve newInput |> printfn "Part two: %d"
    0
