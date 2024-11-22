module Day01

open System.IO
open Common.Collections

let multiply = List.fold (*) 1

let find n input =
    combinations n input
    |> List.filter (fun l -> List.sum l = 2020)
    |> List.map multiply
    |> List.head

let partOne = find 2
let partTwo = find 3

[<EntryPoint>]
let main argv =
    let input = File.ReadAllLines("input.txt") |> Seq.map int |> Seq.toList
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0 // return an integer exit code
