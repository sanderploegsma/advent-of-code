module Day08

open System.IO

type Instruction =
    | Acc of int
    | Jump of int
    | Nop of int

type State = { Accumulator: int; Position: int; Path: int list }

let parse (line: string) =
    let parts = line.Split(" ")
    match parts.[0], int parts.[1] with
    | "acc", x -> Acc x
    | "jmp", x -> Jump x
    | "nop", x -> Nop x
    | x, _ -> failwithf "Invalid instruction %s" x

let rec boot state (instructions: Instruction array) =
    if state.Position = Array.length instructions then
        state
    else
        match instructions.[state.Position] with
        | Nop _ -> 
            let newState = { state with Position = state.Position + 1; Path = state.Position :: state.Path }
            boot newState instructions
        | Acc x -> 
            let newState = { state with Accumulator = state.Accumulator + x; Position = state.Position + 1; Path = state.Position :: state.Path }
            boot newState instructions
        | Jump x when List.contains (state.Position + x) state.Path -> state
        | Jump x -> 
            let newState = { state with Position = state.Position + x; Path = state.Position :: state.Path }
            boot newState instructions
                

let partOne input = 
    let initialState = { Accumulator = 0; Position = 0; Path = [] }
    let finalState = boot initialState input
    finalState.Accumulator

let partTwo input =
    let switch i =
        let mutable copy = Array.copy input
        match copy.[i] with
        | Jump x -> copy.[i] <- Nop x
        | Nop x -> copy.[i] <- Jump x
        | x -> copy.[i] <- x
        copy

    let initialState = { Accumulator = 0; Position = 0; Path = [] }
    seq { 0 .. Array.length input - 1 } 
    |> Seq.map switch 
    |> Seq.map (boot initialState)
    |> Seq.filter (fun state -> state.Position = Array.length input)
    |> Seq.map (fun state -> state.Accumulator)
    |> Seq.head

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.map parse
        |> Seq.toArray
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
