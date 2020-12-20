module Day20

open System.IO

let parse (tile: string array) =
    let id = tile.[0].Substring(5, 4) |> int
    let grid =
        tile
        |> Array.skip 1
        |> Array.map (Seq.map (fun c -> if c = '#' then 1 else 0))
        |> Array.map Seq.toArray

    (id, grid)

let edges tile =
    [
        Array.head tile
        Array.last tile
        Array.map (Array.head) tile
        Array.map (Array.last) tile
    ]

let edgeId edge =
    let id = Array.indexed >> Array.sumBy (fun (i, n) -> n <<< i)
    min (id edge) (Array.rev edge |> id)

let partOne tiles =
    let mutable edgeToTiles = Map.empty
    let mutable tileToEdges = Map.empty

    for (tileId, tile) in tiles do
        let mutable edgeIds = []
        for edge in edges tile do
            let eId = edgeId edge
            let tiles' = Map.tryFind eId edgeToTiles |> Option.defaultValue []
            edgeIds <- eId :: edgeIds
            edgeToTiles <- edgeToTiles.Add(eId, tileId :: tiles')
        tileToEdges <- tileToEdges.Add(tileId, edgeIds)

    let edgeHasMatch e = List.length edgeToTiles.[e] = 2
    let isCorner edges = List.filter edgeHasMatch edges |> List.length = 2
    
    let corners = 
        tileToEdges 
        |> Map.filter (fun _ edges -> isCorner edges) 
        |> Map.toList 
        |> List.map fst

    corners 
    |> List.map int64 
    |> List.fold (*) 1L

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.filter (fun l -> l <> "")
        |> Seq.chunkBySize 11
        |> Seq.map parse
        |> Seq.toList
        
    partOne input |> printfn "Part one: %d"
    0
