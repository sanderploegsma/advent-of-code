open System.IO

/// <summary>
/// Finds the checksum of the given box IDs by calculating
/// the number of IDs that have 2 of the same letter
/// times the number of IDs that have 3 of the same letter.
/// </summary>
let partOne ids =
    let countIf pred = List.filter pred >> List.length

    let has n (x: string) =
        x.ToCharArray()
        |> Array.countBy id
        |> Array.exists (fun (_, c) -> c = n)

    (ids |> countIf (has 2)) * (ids |> countIf (has 3))


/// <summary>
/// Finds the two box IDs that vary by only one character at the same position.
/// </summary>
/// <returns>The characters both box IDs have in common</returns>
let partTwo ids =
    let removeAt i = List.map (fun (x: string) -> x.Remove(i) + x.Substring(i + 1))

    let rec findDuplicate i =
        let duplicate =
            ids
            |> removeAt i
            |> List.countBy id
            |> List.tryFind (fun (_, c) -> c = 2)

        match duplicate with
        | Some(x, _) -> x
        | None -> findDuplicate (i + 1)

    findDuplicate 0

let input = File.ReadLines("input.txt") |> Seq.toList

printfn "Part one: %d" <| partOne input
printfn "Part two: %s" <| partTwo input
