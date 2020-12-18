module Day18

open System.IO
open System.Text.RegularExpressions

let solveEq (eq: string) =
    let items = eq.Split(" ")
    let mutable value = int64 items.[0]
    let mutable op = (*)

    for i in 1 .. items.Length - 1 do
        match items.[i] with
        | "+" -> op <- (+)
        | "*" -> op <- (*)
        | n -> value <- op value (int64 n)

    value

let solve (input: string) =
    let mutable eq = input
    let pattern = "\([\s\+\*\d]+\)"

    let solveGroup (group: string) =
        let n = solveEq (group.TrimStart('(').TrimEnd(')'))
        sprintf "%d" n

    while Regex.IsMatch(eq, pattern) do
        eq <- Regex.Replace(eq, pattern, fun m -> solveGroup m.Value)

    solveEq eq

let partOne (input: string list) =
    let solved = List.map (fun eq -> eq, solve eq) input

    solved |> List.iter (fun (eq, ans) -> printfn "%s = %d" eq ans)
    solved |> List.sumBy snd

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.toList

    let examples = [
        "2 * 3 + (4 * 5)"
        "5 + (8 * 3 + 9 + 3 * 4 * 3)"
        "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))"
        "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"
    ]
        
    partOne input |> printfn "Part one: %d"
    // partTwo input |> printfn "Part two: %d"
    0
