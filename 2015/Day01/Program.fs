module Day01

open System.IO

let parse c = if c = '(' then 1 else -1

let partOne =
    Seq.map parse
    >> Seq.sum

let partTwo input =
    let (pos, _) = 
        input
        |> Seq.map parse
        |> Seq.indexed
        |> Seq.fold (fun (pos, floor) (newPos, diff) -> if floor < 0 then (pos, floor) else (newPos, floor + diff)) (0, 0)
    pos + 1

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText("Input.txt")
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
