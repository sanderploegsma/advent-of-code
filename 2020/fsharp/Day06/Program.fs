module Day06

open System.IO

let countAnswers combine =
    List.map Set.ofSeq
    >> List.toSeq
    >> combine
    >> Set.count

let partOne =
    List.sumBy (countAnswers Set.unionMany)

let partTwo =
    List.sumBy (countAnswers Set.intersectMany)

[<EntryPoint>]
let main argv =
    let input =
        File.ReadAllText("Input.txt").Split("\r\n\r\n")
        |> Seq.map (fun group -> group.Split("\r\n") |> Array.toList)
        |> Seq.toList

    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
