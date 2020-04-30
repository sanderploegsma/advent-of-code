open System.IO

let input =
    File.ReadAllText("input.txt")
    |> Seq.map (System.Char.GetNumericValue >> int)

/// <summary>
/// Sum elements of the given sequence if it matches the element `n` positions ahead.
/// Consider the sequence circular, so the element after the last element is the first element.
/// </summary>
let sumAdjacent n seq =
    Seq.append seq seq
    |> Seq.windowed (n + 1)
    |> Seq.take (Seq.length seq)
    |> Seq.filter (fun x -> Seq.head x = Seq.last x)
    |> Seq.sumBy Seq.head

input |> sumAdjacent 1 |> printfn "Part one: %d"

input
|> sumAdjacent (Seq.length input / 2)
|> printfn "Part two: %d"
