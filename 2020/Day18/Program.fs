module Day18

open System.IO
open System.Text.RegularExpressions
open System.Collections.Generic

let solveEq (precedence: Map<string, int>) (eq: string) =
    // Source: https://en.wikipedia.org/wiki/Operator-precedence_parser
    let rec solveRec value (queue: Queue<string>) min =
        let mutable next = ""
        let mutable lhs = value
        while queue.TryPeek(&next) && precedence.[next] >= min do
            let op = queue.Dequeue()
            let mutable rhs = queue.Dequeue() |> int64
            while queue.TryPeek(&next) && precedence.[next] > precedence.[op] do
                rhs <- solveRec rhs queue precedence.[next]
            lhs <- match op with
                   | "+" -> lhs + rhs
                   | "*" -> lhs * rhs
        lhs

    let queue = new Queue<string>()
    eq.Split(" ") |> Array.iter (fun n -> queue.Enqueue(n))
    solveRec (queue.Dequeue() |> int64) queue 0

let solve solver (input: string)  =
    let mutable eq = input
    let pattern = "\([\s\+\*\d]+\)"

    let solveGroup (group: string) =
        let n = solver (group.TrimStart('(').TrimEnd(')'))
        sprintf "%d" n

    while Regex.IsMatch(eq, pattern) do
        eq <- Regex.Replace(eq, pattern, fun m -> solveGroup m.Value)

    solver eq

let partOne =
    let precedence = Map.ofList [("+", 1); ("*", 1)]
    let solver = solveEq precedence
    List.sumBy (solve solver)

let partTwo =
    let precedence = Map.ofList [("+", 2); ("*", 1)]
    let solver = solveEq precedence
    List.sumBy (solve solver)

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.toList
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
