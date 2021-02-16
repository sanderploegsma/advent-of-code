open System
open System.IO
open System.Text.RegularExpressions

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)

    if m.Success
    then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

let parse line =
    match line with
    | Regex @"Step ([A-Z]) must be finished before step ([A-Z]) can begin." [ a; b ] -> a, b
    | _ -> failwithf "Unable to parse line: %s" line

let rec solve (dependencies: Map<string, string list>) readySteps finishedSteps =
    match Set.toList readySteps |> List.sort with
    | [] -> finishedSteps
    | x :: tail ->
        let finishedSteps' = finishedSteps @ [ x ]

        let unlocked =
            dependencies
            |> Map.filter (fun _ -> List.except finishedSteps' >> List.isEmpty)
            |> Map.toSeq
            |> Seq.map fst
            |> Set.ofSeq
            
        let newReady = Set.difference (Set.ofList tail |> Set.union unlocked) (Set.ofList finishedSteps')

        solve dependencies newReady finishedSteps'

[<EntryPoint>]
let main argv =
    let dependencies =
        File.ReadLines("Input.txt") |> Seq.map parse

    let dependencyMap =
        dependencies
        |> Seq.groupBy snd
        |> Map.ofSeq
        |> Map.map (fun _ values -> Seq.map fst values |> Seq.toList)

    let ready =
        dependencies
        |> Seq.map fst
        |> Seq.filter (fun x -> not (Map.containsKey x dependencyMap))
        |> Set.ofSeq

    solve dependencyMap ready []
    |> String.Concat
    |> printfn "Part one: %s"

    0 // return an integer exit code
