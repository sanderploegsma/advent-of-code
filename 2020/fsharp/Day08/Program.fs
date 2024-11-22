module Day08

open System.IO

type Instruction =
    | Acc of int
    | Jump of int
    | Nop of int

type State = { Acc: int; Pos: int; Path: int list }

let parse (line: string): Instruction =
    let parts = line.Split(" ")
    match parts.[0], int parts.[1] with
    | "acc", x -> Acc x
    | "jmp", x -> Jump x
    | "nop", x -> Nop x
    | x, _ -> failwithf "Invalid instruction %s" x

let rec boot (state: State) (instructions: Instruction array): State =
    let nextInstruction pos =
        if state.Pos = Array.length instructions then
            None
        else
            Some instructions.[pos]

    match nextInstruction state.Pos with
    | None -> state
    | Some (Jump x) when List.contains (state.Pos + x) state.Path -> state
    | Some (Nop _) ->
        let newState = { state with Pos = state.Pos + 1; Path = state.Pos :: state.Path }
        boot newState instructions
    | Some (Acc x) ->
        let newState = { state with Acc = state.Acc + x; Pos = state.Pos + 1; Path = state.Pos :: state.Path }
        boot newState instructions
    | Some (Jump x) ->
        let newState = { state with Pos = state.Pos + x; Path = state.Pos :: state.Path }
        boot newState instructions


let partOne input =
    let initialState = { Acc = 0; Pos = 0; Path = [] }
    let finalState = boot initialState input
    finalState.Acc

let partTwo input =
    let switch i =
        let mutable copy = Array.copy input
        match copy.[i] with
        | Jump x -> copy.[i] <- Nop x
        | Nop x -> copy.[i] <- Jump x
        | x -> copy.[i] <- x
        copy

    let initialState = { Acc = 0; Pos = 0; Path = [] }
    seq { 0 .. Array.length input - 1 }
    |> Seq.map switch
    |> Seq.map (boot initialState)
    |> Seq.filter (fun state -> state.Pos = Array.length input)
    |> Seq.map (fun state -> state.Acc)
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
