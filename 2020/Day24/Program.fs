module Day24

open System.IO

type Direction = East | SouthEast | SouthWest | West | NorthWest | NorthEast

let step (x, y) direction =
    match direction with
    | East -> x + 1, y + 1
    | West -> x - 1, y - 1
    | SouthEast -> x + 1, y
    | NorthEast -> x, y + 1
    | SouthWest -> x, y - 1
    | NorthWest -> x - 1, y

let rec navigate start directions =
    match directions with 
    | [] -> start
    | d :: tail -> navigate (step start d) tail

let rec parse chars =
    match chars with
    | [] -> []
    | 'e' :: tail -> East :: parse tail
    | 'w' :: tail -> West :: parse tail
    | 's' :: 'e' :: tail -> SouthEast :: parse tail
    | 'n' :: 'e' :: tail -> NorthEast :: parse tail
    | 's' :: 'w' :: tail -> SouthWest :: parse tail
    | 'n' :: 'w' :: tail -> NorthWest :: parse tail

let partOne tiles =
    tiles
    |> List.map (navigate (0, 0))
    |> List.countBy id
    |> List.filter (fun (_, count) -> count % 2 = 1)
    |> List.length

let neighbours coordinate = 
    [East; SouthEast; SouthWest; West; NorthWest; NorthEast] 
    |> List.map (step coordinate)

let run (input: ('T * int) list) (findNeighbours: 'T -> 'T list) =
    let mutable state = Map.ofList input

    let getState point = Map.tryFind point state |> Option.defaultValue 0

    for i in 0 .. 99 do
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
                | 1, n when n = 0 || n > 2 -> 0
                | 0, 2 -> 1
                | v, _ -> v
            
            newState <- newState.Add(point, newValue)
           )

        state <- newState

    state |> Map.toList

let partTwo tiles =
    let initial = 
        tiles
        |> List.map (navigate (0, 0))
        |> List.countBy id
        |> List.map (fun (tile, count) -> tile, count % 2)

    let final = run initial neighbours
    final |> List.map snd |> List.sum

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.map (Seq.toList >> parse)
        |> Seq.toList
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
