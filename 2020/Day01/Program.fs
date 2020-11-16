module Day01

open System.IO

let partOne = sprintf "Hello, %s!"

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText("input.txt")
    partOne input |> printfn "Part one: %s"
    0 // return an integer exit code
