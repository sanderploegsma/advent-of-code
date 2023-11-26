module Day09

open System.IO

type State = {
    Level: int
    Score: int
    Garbage: int
    IsGarbage: bool
    IsCanceled: bool
}

let initialState = {
    Level = 0
    Score = 0
    Garbage = 0
    IsGarbage = false
    IsCanceled = false
}

let rec traverse (input: char list) state =
    match input with
    | [] -> state
    | _ :: tail when state.IsGarbage && state.IsCanceled -> traverse tail { state with IsCanceled = false }
    | '!' :: tail when state.IsGarbage -> traverse tail { state with IsCanceled = true }
    | '>' :: tail when state.IsGarbage -> traverse tail { state with IsGarbage = false }
    | _ :: tail when state.IsGarbage -> traverse tail { state with Garbage = state.Garbage + 1 }
    | '<' :: tail -> traverse tail { state with IsGarbage = true }
    | '{' :: tail -> traverse tail { state with Level = state.Level + 1 }
    | '}' :: tail -> traverse tail { state with Score = state.Score + state.Level; Level = state.Level - 1 }
    | _ :: tail -> traverse tail state

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText("input.txt") |> Seq.toList
    let state = traverse input initialState
    printfn "Part one: %d" state.Score
    printfn "Part two: %d" state.Garbage
    0
