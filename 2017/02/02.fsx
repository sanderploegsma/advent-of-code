open System.IO

let input =
    File.ReadAllLines("input.txt")
    |> Seq.map ((fun row -> row.Split("\t")) >> Array.map int)


let partOne row = Array.max row - Array.min row

let divides =
    function
    | (a, b) when a % b = 0 -> Some(a / b)
    | (a, b) when b % a = 0 -> Some(b / a)
    | _ -> None

let partTwo row =
    Array.allPairs row row
    |> Array.filter (fun (a, b) -> a <> b)
    |> Array.pick divides

input
|> Seq.sumBy partOne
|> printfn "Part one: %d"

input
|> Seq.sumBy partTwo
|> printfn "Part two: %d"
