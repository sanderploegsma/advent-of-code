module Day07

open System
open System.IO

type ContainedBags = { Count: int; Color: string; }
type Rule = { Color: string; Children: ContainedBags list }

let parseRule (rule: string) =
    let parseParentSegment (segment: string) =
        let parts = segment.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        parts.[0] + parts.[1]
    let parseChildSegment (segment: string) =
        let parts = segment.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        { Count = int parts.[0]; Color = parts.[1] + parts.[2] }
    
    let parentAndChildren = rule.Split(" contain ", StringSplitOptions.RemoveEmptyEntries)
    let children = 
        match parentAndChildren.[1] with
        | "no other bags." -> []
        | x -> x.Split(", ") |> Array.map parseChildSegment |> Array.toList

    { Color = parseParentSegment parentAndChildren.[0]; Children = children }

let rec canContain color rules seen =
    let hasChildWithColor rule = List.exists (fun (child: ContainedBags) -> child.Color = color) rule.Children
    let parents = rules |> List.filter hasChildWithColor
    if List.isEmpty parents then
        seen
    else
        let newSeen = seen @ (List.map (fun rule -> rule.Color) parents)
        parents
        |> List.collect (fun rule -> canContain rule.Color rules newSeen)

let rec containsCount color rules =
    let rule = rules |> List.find (fun rule -> rule.Color = color)
    if List.isEmpty rule.Children then 0
    else
        let childContainsCount child =
            child.Count + (child.Count * containsCount child.Color rules)
        rule.Children |> List.sumBy childContainsCount

let partOne rules = 
    canContain "shinygold" rules []
    |> List.distinct
    |> List.length

let partTwo rules = containsCount "shinygold" rules

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt")
        |> Seq.map parseRule
        |> Seq.toList
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
