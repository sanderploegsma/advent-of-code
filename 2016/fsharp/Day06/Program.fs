module Day06

open System
open System.IO

// Source: https://www.howtobuildsoftware.com/index.php/how-do/bgnh/f-f-30-how-to-transpose-a-matrix
let transpose (matrix:_ [][]) =
    if matrix.Length = 0 then failwith "Invalid matrix"
    Array.init matrix.[0].Length (fun i ->
        Array.init matrix.Length (fun j ->
            matrix.[j].[i]))

let partOne input =
    transpose input
    |> Array.map (Array.countBy id >> Array.maxBy (fun (_, n) -> n))
    |> Array.map (fun (c, _) -> c)
    |> String

let partTwo input =
    transpose input
    |> Array.map (Array.countBy id >> Array.minBy (fun (_, n) -> n))
    |> Array.map (fun (c, _) -> c)
    |> String

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.toArray
        |> Array.map Seq.toArray

    partOne input |> printfn "Part one: %s"
    partTwo input |> printfn "Part two: %s"
    0
