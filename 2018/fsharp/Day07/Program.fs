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
    | Regex @"Step ([A-Z]) must be finished before step ([A-Z]) can begin." [ a; b ] -> a.[0], b.[0]
    | _ -> failwithf "Unable to parse line: %s" line

type Task = { Task: char; TimeLeft: int }

type Worker =
    | Idle
    | Working of Task

let solve timePerTask numWorkers dependencies =
    let allTasks =
        dependencies
        |> Seq.collect (fun (a, b) -> [ a; b ])
        |> Set.ofSeq

    let dependencyMap =
        dependencies
        |> Seq.groupBy snd
        |> Map.ofSeq
        |> Map.map (fun _ values -> Seq.map fst values |> Seq.toList)

    let mutable finished = List.empty
    let mutable remaining = Set.toList allTasks

    let isTaskReady task =
        Map.tryFind task dependencyMap
        |> Option.map (List.except finished >> List.isEmpty)
        |> Option.defaultValue true

    let pullTask () =
        let readyTasks =
            remaining |> List.filter isTaskReady |> List.sort

        match readyTasks with
        | [] -> Idle
        | task :: _ ->
            remaining <- List.except [ task ] remaining
            let time = timePerTask + (int task - int 'A' + 1)
            Working { Task = task; TimeLeft = time }

    let mutable t = 0

    let maxT =
        List.length remaining * (timePerTask + 26)

    let schedule = Array2D.create maxT numWorkers Idle

    while not (Set.forall (fun task -> List.contains task finished) allTasks) do
        for w in 0 .. numWorkers - 1 do
            if t = 0 then
                schedule.[t, w] <- pullTask ()
            else
                match schedule.[t - 1, w] with
                | Idle -> schedule.[t, w] <- pullTask ()
                | Working task when task.TimeLeft = 1 ->
                    finished <- finished @ [ task.Task ]
                    schedule.[t, w] <- pullTask ()
                | Working task ->
                    schedule.[t, w] <- Working
                                           { task with
                                                 TimeLeft = task.TimeLeft - 1 }

        t <- t + 1

    finished, t - 1

[<EntryPoint>]
let main argv =
    let dependencies =
        File.ReadLines("Input.txt") |> Seq.map parse

    let order, _ = solve 0 1 dependencies
    printfn "Part one: %s" (String.Concat order)

    let _, time = solve 60 5 dependencies
    printfn "Part two: %d" time

    0 // return an integer exit code
