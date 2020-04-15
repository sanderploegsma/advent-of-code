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
    let removeCharAt i (s: string) = s.Remove(i) + s.Substring(i + 1)

    let findDuplicate = List.countBy id >> List.tryFind (fun (_, c) -> c = 2)

    let rec checkPosition i =
        let duplicate =
            ids
            |> List.map (removeCharAt i)
            |> findDuplicate

        match duplicate with
        | Some(x, _) -> Some(x)
        | None when ids.IsEmpty -> None
        | None when ids.Head.Length = i -> None
        | None -> checkPosition (i + 1)

    checkPosition 0

let input = File.ReadLines("input.txt") |> Seq.toList

printfn "Part one: %d" <| partOne input
printfn "Part two: %A" <| partTwo input
