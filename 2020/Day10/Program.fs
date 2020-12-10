module Day10

open System.IO

let partOne input = 
    let range = (0 :: input) @ [List.max input + 3] |> List.sort

    let differences = 
        range
        |> List.pairwise
        |> List.map (fun (a, b) -> b - a)
        |> List.countBy id
        |> Map.ofList

    differences.[1] * (differences.[3])

let partTwo input =
    let mutable cache = Map.empty
    let adapters = (0 :: input) |> List.sortDescending
    cache <- cache.Add(List.head adapters, 1L)

    for i in 1 .. List.length adapters - 1 do
        let jolts = adapters.[i]
        let count =
            seq { 1 .. 3}
            |> Seq.map (fun x -> jolts + x)
            |> Seq.map (fun c -> cache.TryFind(c))
            |> Seq.map (Option.defaultValue 0L)
            |> Seq.sum

        cache <- cache.Add(jolts, count)

    cache.[0]

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt")
        |> Seq.map int
        |> Seq.toList

    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
