module Day04

open System
open System.IO

let noDuplicateWords (phrase: string list) =
    let distinct = List.distinct phrase
    List.length phrase = List.length distinct

let noAnagrams (phrase: string list) =
    let sortChars = Seq.sort >> Seq.toArray >> String
    phrase |> List.map sortChars |> noDuplicateWords

let validate validation (input: string list) =
    input
    |> List.map (fun phrase -> phrase.Split(" ") |> Array.toList)
    |> List.filter validation
    |> List.length

let partOne = validate noDuplicateWords
let partTwo = validate noAnagrams

[<EntryPoint>]
let main argv =
    let input = File.ReadLines("input.txt") |> Seq.toList
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
