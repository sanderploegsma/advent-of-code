open System.IO

let has n (x: string) =
    x.ToCharArray()
    |> Array.countBy id
    |> Array.exists (fun (_, c) -> c = n)

let countIf pred = List.filter pred >> List.length

/// <summary>
/// Finds the checksum of the given box IDs by calculating
/// the number of IDs that have 2 of the same letter
/// times the number of IDs that have 3 of the same letter.
/// </summary>
let partOne ids =
    let has2 = has 2
    let has3 = has 3

    (ids |> countIf has2) * (ids |> countIf has3)

let input = File.ReadLines("input.txt") |> Seq.toList

printfn "Part one: %d" <| partOne input
