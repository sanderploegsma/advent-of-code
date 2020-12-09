module Day02

open System.IO

let parse (line: string) =
    let lwh = line.Split("x")
    (int lwh.[0], int lwh.[1], int lwh.[2])

let calcSurfaceArea (l, w, h) =
    let sides = [l*w; w*h; h*l]
    List.sumBy (fun side -> side * 2) sides + List.min sides

let calcRibbonLength (l, w, h) = 
    let sides = [l; w; h]
    let ribbon = List.sort sides |> List.take 2 |> List.sumBy (fun side -> side * 2)
    let bow = List.fold (*) 1 sides
    ribbon + bow

let partOne = List.sumBy calcSurfaceArea
let partTwo = List.sumBy calcRibbonLength

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt")
        |> Seq.map parse
        |> Seq.toList
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
