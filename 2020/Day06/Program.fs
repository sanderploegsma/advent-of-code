module Day06

open System.IO

let countAllAnswers (group: string list) = 
    group
    |> List.toSeq
    |> Seq.collect id
    |> Seq.distinct
    |> Seq.length

let countCommonAnswers (group: string list) =
    group
    |> List.toSeq
    |> Seq.collect id
    |> Seq.countBy id
    |> Seq.filter (fun (_, count) -> count = List.length group)
    |> Seq.length

let partOne =
    List.sumBy countAllAnswers

let partTwo =
    List.sumBy countCommonAnswers

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadAllText("Input.txt").Split("\r\n\r\n")
        |> Seq.map (fun group -> group.Split("\r\n") |> Array.toList)
        |> Seq.toList
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
