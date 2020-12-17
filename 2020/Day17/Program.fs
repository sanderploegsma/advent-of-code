module Day17

open Common.Navigation
open System.IO

let parse (y, line) =
    Seq.mapi (fun x c -> (x, y), if c = '#' then 1 else 0) line

let run (input: ('T * int) list) (findNeighbours: 'T -> 'T list) =
    let mutable state = Map.ofList input

    let getState point = Map.tryFind point state |> Option.defaultValue 0

    for i in 0 .. 5 do
        let mutable newState =
            Map.toList state
            |> List.map fst
            |> List.collect findNeighbours
            |> List.distinct
            |> List.map (fun p -> p, getState p)
            |> Map.ofList

        Map.toList newState
        |> List.iter (fun (point, value) ->
            let activeNeighbours = findNeighbours point |> List.sumBy getState           
            let newValue =
                match value, activeNeighbours with
                | 1, n when n = 2 || n = 3 -> 1
                | 0, 3 -> 1
                | _, _ -> 0
            
            newState <- newState.Add(point, newValue)
           )

        state <- newState

    state |> Map.toList |> List.map snd |> List.sum

let partOne input = 
    let coordinates = List.map (fun ((x, y), state) -> (x, y, 0), state) input
    run coordinates neighbours3D

let partTwo input =
    let coordinates = List.map (fun ((x, y), state) -> (x, y, 0, 0), state) input
    run coordinates neighbours4D

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt")
        |> Seq.indexed
        |> Seq.collect parse
        |> Seq.toList
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0