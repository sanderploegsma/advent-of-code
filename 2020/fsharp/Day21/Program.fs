module Day21

open System.IO

let parse (line: string) =
    line.Split(" (contains ")
    |> fun s -> s.[1].TrimEnd(')').Split(", "), s.[0].Split(' ')

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.map parse
        |> Seq.toList

    let mutable candidates = Map.empty
    for allergens, ingredients in input do
        for allergen in allergens do
            let existing = Map.tryFind allergen candidates |> Option.defaultValue (Set.ofArray ingredients)
            candidates <- Map.add allergen (Set.intersect existing (Set.ofArray ingredients)) candidates

    let allCandidates = Map.toList candidates |> List.collect (snd >> Set.toList)
    let allIngredients = List.collect (snd >> Array.toList) input
    let partOne = List.sumBy (fun i -> if List.contains i allCandidates then 0 else 1) allIngredients
    printfn "Part one: %d" partOne

    let mutable allergies = Map.empty
    while not (Map.isEmpty candidates) do
        let taken = Map.toList allergies |> List.map snd |> Set.ofList
        let nextAvailable allergen candidates =
            let available = Set.difference candidates taken |> Set.toList
            List.tryExactlyOne available |> Option.map (fun candidate -> allergen, candidate)

        let allergen, candidate = Map.pick nextAvailable candidates
        allergies <- Map.add allergen candidate allergies
        candidates <- Map.remove allergen candidates

    let partTwo = Map.toList allergies |> List.sortBy fst |> List.map snd |> String.concat ","
    printfn "Part two: %s" partTwo

    0
