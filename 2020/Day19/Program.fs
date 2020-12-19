module Day19

open System.Text.RegularExpressions
open System.IO

let createPattern rule (rules: Map<int, string>) =
    let rec build r n =
        match n, rules.[r] with
        | n, _ when n > 25 -> "" // No rule goes this deep
        | _, "\"a\"" -> "a"
        | _, "\"b\"" -> "b"
        | n, composite -> 
            composite.Split(" ") 
            |> Array.map (fun s -> if s = "|" then "|" else build (int s) (n + 1))
            |> Array.fold (+) ""
            |> sprintf "(%s)"

    sprintf "^%s$" (build rule 0)

let partOne (rules: Map<int, string>) messages =
    let pattern = createPattern 0 rules
    
    messages
    |> List.filter (fun m -> Regex.IsMatch(m, pattern))
    |> List.length

let partTwo (rules: Map<int, string>) messages =
    let newRules = 
        rules
        |> Map.add 8 "42 | 42 8"
        |> Map.add 11 "42 31 | 42 11 31"

    partOne newRules messages

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt")
        |> Seq.toList

    let rules = 
        input 
        |> List.takeWhile (fun s -> s <> "")
        |> List.map (fun s -> s.Split(": "))
        |> List.map (fun s -> int s.[0], s.[1])
        |> Map.ofList

    let messages = input |> List.skip (Map.count rules + 1)
        
    partOne rules messages |> printfn "Part one: %d"
    partTwo rules messages |> printfn "Part two: %d"
    0
