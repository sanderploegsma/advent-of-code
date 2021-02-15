open System
open System.IO

let reduce polymer =
    let react polymer' unit =
        match polymer' with
        | x :: tail when abs (unit - x) = 32 -> tail
        | _ -> unit :: polymer'
        
    Seq.fold react [] polymer
    
let partOne = Seq.map int >> reduce >> Seq.length

let partTwo polymer =
    let polymerExcludingUnit unit =
        polymer
        |> String.filter (fun c -> Char.ToLower(c) <> unit)
    
    polymer
    |> Seq.distinctBy Char.ToLower
    |> Seq.map polymerExcludingUnit
    |> Seq.map partOne
    |> Seq.min

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText("Input.txt")
    
    printfn "Part one: %d" <| partOne input
    printfn "Part two: %d" <| partTwo input
    
    0 // return an integer exit code
