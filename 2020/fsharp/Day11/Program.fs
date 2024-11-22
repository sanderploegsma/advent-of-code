module Day11

open Common.Navigation
open System.IO

type Tile = Floor | FreeSeat | OccupiedSeat

let parse (row, line) =
    let parseChar c =
        match c with
        | '.' -> Floor
        | 'L' -> FreeSeat

    line
    |> Seq.indexed
    |> Seq.map (fun (col, c) -> ((row, col), parseChar c))

let rec evolve tiles =
    let getNewTile coordinate tile =
        let surroundingOccupied =
            neighbours8 coordinate
            |> List.choose (fun c -> Map.tryFind c tiles)
            |> List.sumBy (fun t -> if t = OccupiedSeat then 1 else 0)

        match tile with
        | FreeSeat when surroundingOccupied = 0 -> OccupiedSeat
        | OccupiedSeat when surroundingOccupied >= 4 -> FreeSeat
        | _ -> tile

    let newTiles = tiles |> Map.map getNewTile
    if newTiles = tiles then
        newTiles
    else
        evolve newTiles

let rec evolve2 tiles =
    let getNewTile coordinate tile =
        let firstSeat line =
            line
            |> Seq.skipWhile (fun c -> Map.containsKey c tiles && tiles.[c] = Floor)
            |> Seq.map (fun c -> Map.tryFind c tiles)
            |> Seq.head

        let surroundingOccupied =
            lineOfSight coordinate
            |> List.choose firstSeat
            |> List.sumBy (fun t -> if t = OccupiedSeat then 1 else 0)

        match tile with
        | FreeSeat when surroundingOccupied = 0 -> OccupiedSeat
        | OccupiedSeat when surroundingOccupied >= 5 -> FreeSeat
        | _ -> tile

    let newTiles = tiles |> Map.map getNewTile
    if newTiles = tiles then
        newTiles
    else
        evolve2 newTiles

let partOne input =
    evolve input
    |> Map.filter (fun _ tile -> tile = OccupiedSeat)
    |> Map.count

let partTwo input =
    evolve2 input
    |> Map.filter (fun _ tile -> tile = OccupiedSeat)
    |> Map.count

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.indexed
        |> Seq.collect parse
        |> Map.ofSeq

    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
