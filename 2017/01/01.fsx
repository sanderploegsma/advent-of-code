open System.IO

let input =
    File.ReadAllText("input.txt")
    |> Seq.map (System.Char.GetNumericValue >> int)
    |> Seq.toList

/// <summary>
/// Finds the sum of all digits that match the next digit in the list.
/// The list is circular, so the digit after the last digit is the first digit in the list.
/// </summary>
let partOne lst =
    let first = List.head lst
    let last = List.last lst
    let initial = if first = last then first else 0

    let rec sumAdjacent sum pairs =
        match pairs with
        | [] -> sum
        | (a, b) :: tail when a = b -> sumAdjacent (sum + a) tail
        | (_, _) :: tail -> sumAdjacent sum tail

    List.pairwise lst |> sumAdjacent initial

partOne input |> printfn "Part one: %d"
