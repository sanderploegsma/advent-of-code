module Day20

open Common.Navigation
open System
open System.IO

type Side = Top | Right | Bottom | Left

type Tile =
    { Id: int; Grid: char[][] }

    member this.edges = [Top; Right; Bottom; Left] |> List.map this.edge

    member this.edge side =
        match side with
        | Top -> Array.head this.Grid
        | Bottom -> Array.last this.Grid
        | Left -> Array.map (Array.head) this.Grid
        | Right -> Array.map (Array.last) this.Grid

    member this.orientations () =
        seq {
            yield this
            yield this.rotate()
            yield this.rotate().rotate()
            yield this.rotate().rotate().rotate()
            yield this.flip()
            yield this.flip().rotate()
            yield this.flip().rotate().rotate()
            yield this.flip().rotate().rotate().rotate()
        }

    /// Create a new tile where each outer edge is removed
    member this.withoutBorder () =
        let size = Array.length this.Grid - 2
        let grid =
            this.Grid
            |> Array.skip 1
            |> Array.take size
            |> Array.map (Array.skip 1)
            |> Array.map (Array.take size)

        { this with Grid = grid }
    
    /// Rotate the tile 90deg clockwise
    member this.rotate () =
        let grid = Array.mapi (fun i row -> Array.mapi (fun j _ -> this.Grid.[j].[i]) row |> Array.rev) this.Grid
        { this with Grid = grid }

    /// Flip the tile over its vertical axis
    member this.flip () =
        { this with Grid = Array.map (Array.rev) this.Grid }

    /// Connect this tile in any of its orientations to the given edge on the given side
    member this.connectTo (tile: Tile) theirSide mySide =
        this.orientations()
        |> Seq.tryFind (fun t -> t.edge mySide = tile.edge theirSide)

    /// Check whether this tile connects to any other tile on the given side
    member this.hasNeighbourOnSide tiles side =
        tiles
        |> List.filter (fun tile -> tile.Id <> this.Id)
        |> List.collect (fun tile -> tile.edges @ (List.map Array.rev tile.edges))
        |> List.exists (fun edge -> this.edge side = edge)

let parse (tile: string array) =
    let id = tile.[0].Substring(5, 4) |> int
    let grid =
        tile
        |> Array.skip 1
        |> Array.map Seq.toArray

    { Id = id; Grid = grid }

let findCorners (tiles: Tile list) =
    let isCorner (tile: Tile) =
        let matchingSides = [Top; Right; Left; Bottom] |> List.filter (tile.hasNeighbourOnSide tiles)
        List.length matchingSides = 2

    tiles
    |> List.filter isCorner

let solve (tiles: Tile list) =
    let topLeft = 
        findCorners tiles 
        |> List.take 1
        |> List.toSeq
        |> Seq.collect (fun tile -> tile.orientations())
        |> Seq.find (fun tile -> tile.hasNeighbourOnSide tiles Right && tile.hasNeighbourOnSide tiles Bottom)

    let size = List.length tiles |> float |> sqrt |> int

    seq {
        let mutable tiles' = tiles
        let mutable previousTile = topLeft
        let mutable previousTileOnLeftEdge = topLeft
        for row in 0 .. size - 1 do
            for col in 0 .. size - 1 do
                let tile =
                    match row, col with
                    | 0, 0 -> previousTile
                    | _, 0 ->
                        let newTile = List.pick (fun (tile: Tile) -> tile.connectTo previousTileOnLeftEdge Bottom Top) tiles'
                        previousTile <- newTile
                        previousTileOnLeftEdge <- newTile
                        newTile
                    | _, _ -> 
                        let newTile = List.pick (fun (tile: Tile) -> tile.connectTo previousTile Right Left) tiles'
                        previousTile <- newTile
                        newTile
                tiles' <- List.filter (fun t -> t.Id <> tile.Id) tiles'
                tile
    }
    |> Seq.chunkBySize size
    |> Seq.toArray

let stitch (puzzle: Tile[][]): Tile =
    let stitchRow (row: Tile[]): char[][] =
        let size = Array.length row.[0].Grid
        seq { 
            for r in 0 .. size - 1 do
                row |> Array.map (fun t -> t.Grid.[r]) |> Array.concat
        }
        |> Seq.toArray

    let grid =
        puzzle
        |> Array.map (Array.map (fun tile -> tile.withoutBorder()))
        |> Array.collect stitchRow

    { Id = 0; Grid = grid }

let overlay (mask: Coordinate list) (tile: Tile): Tile option =
    let mutable grid = tile.Grid

    let gridSize = Array.length grid
    let maskWidth = List.maxBy fst mask |> fst
    let maskHeight = List.maxBy snd mask |> snd

    let mutable found = false
    for row in 0 .. gridSize - maskWidth do
        for col in 0 .. gridSize - maskHeight do
            let mask' = List.map (fun c -> addCoordinates c (row, col)) mask
            if List.forall (fun (row', col') -> grid.[row'].[col'] = '#') mask' then
                found <- true
                for (row', col') in mask' do
                    grid.[row'].[col'] <- '.'

    if found then Some { tile with Grid = grid } else None

let partOne input =
    findCorners input
    |> List.map (fun tile -> int64 tile.Id)
    |> List.fold (*) 1L

let partTwo input =
    let puzzle = solve input
    let image = stitch puzzle

    //                   # 
    // #    ##    ##    ###
    //  #  #  #  #  #  #   
    let mask = [
        (0, 1); (1, 0)
        (4, 0); (5, 1); (6, 1); (7, 0)
        (10, 0); (11, 1); (12, 1); (13, 0)
        (16, 0); (17, 1); (18, 1); (18, 2); (19, 1)
    ]

    let corrected = image.orientations() |> Seq.pick (overlay mask)
    Array.sumBy (Array.sumBy (fun c -> if c = '#' then 1 else 0)) corrected.Grid

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.filter (fun l -> l <> "")
        |> Seq.chunkBySize 11
        |> Seq.map parse
        |> Seq.toList
 
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0