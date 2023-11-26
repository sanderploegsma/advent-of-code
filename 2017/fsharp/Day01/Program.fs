module Day01

open System.IO

/// Sum elements of the given sequence if it matches the element `n` positions ahead.
/// Consider the sequence circular, so the element after the last element is the first element.
let sumAdjacent n seq =
    Seq.append seq seq
    |> Seq.windowed (n + 1)
    |> Seq.take (Seq.length seq)
    |> Seq.filter (fun x -> Seq.head x = Seq.last x)
    |> Seq.sumBy Seq.head

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText("input.txt") |> Seq.map (System.Char.GetNumericValue >> int)
    let middle = Seq.length input / 2

    input |> sumAdjacent 1 |> printfn "Part one: %d"
    input |> sumAdjacent middle |> printfn "Part two: %d"
    
    0 // return an integer exit code

