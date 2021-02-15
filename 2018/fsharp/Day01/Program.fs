open System.IO

/// Calculate the final frequency by applying each delta, starting at 0.
let partOne deltas = List.sum deltas

/// While applying each delta starting at 0, find the first frequency reached twice.
let partTwo initial =
    let rec sumUntilDuplicate freqs deltas cur =
        match deltas with
        // If we found the frequency before, we can stop
        | d :: _ when List.contains (cur + d) freqs -> cur + d
        // Else, keep looking
        | d :: tail -> sumUntilDuplicate ((cur + d) :: freqs) tail (cur + d)
        // If we reach the end of the input, loop around
        | [] -> sumUntilDuplicate freqs initial cur
    sumUntilDuplicate [] initial 0

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.map int
        |> Seq.toList

    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
