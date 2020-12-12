namespace Common

module Navigation =

    type Coordinate = int * int
    type Direction = North | East | South | West
    type Rotation = Left | Right

    let addCoordinates (x1, y1) (x2, y2) = (x1 + x2, y1 + y2)

    /// Calculate the manhattan distance between two coordinates
    let manhattan (x1, y1) (x2, y2) = abs (x2 - x1) + abs (y2 - y1)

    /// Move one step from a coordinate in the given direction
    let move (x, y) direction =
        match direction with
        | North -> (x, y + 1)
        | East -> (x + 1, y)
        | South -> (x, y - 1)
        | West -> (x - 1, y)

    /// Given a direction, turn to the given rotation
    let turn direction rotation =
        match direction, rotation with
        | North, Left | South, Right -> West
        | East, Left | West, Right -> North
        | South, Left | North, Right -> East
        | West, Left | East, Right -> South

    /// Get the horizontal and vertical neighbours of a coordinate
    let neighbours4 (x, y) =
        [
            (x, y-1)
            (x, y+1)
            (x-1, y)
            (x+1, y)
        ]

    /// Get the horizontal, vertical and diagonal neighbours of a coordinate
    let neighbours8 (x, y) =
        [
            (x-1, y-1)
            (x, y-1)
            (x+1, y-1)
            (x-1, y)
            (x+1, y)
            (x-1, y+1)
            (x, y+1)
            (x+1, y+1)
        ]

    let lineOfSight (x, y) =
        [
            Seq.initInfinite (fun d -> (x - d, y - d)) |> Seq.skip 1
            Seq.initInfinite (fun d -> (x, y - d)) |> Seq.skip 1
            Seq.initInfinite (fun d -> (x + d, y - d)) |> Seq.skip 1
            Seq.initInfinite (fun d -> (x + d, y)) |> Seq.skip 1
            Seq.initInfinite (fun d -> (x + d, y + d)) |> Seq.skip 1
            Seq.initInfinite (fun d -> (x, y + d)) |> Seq.skip 1
            Seq.initInfinite (fun d -> (x - d, y + d)) |> Seq.skip 1
            Seq.initInfinite (fun d -> (x - d, y)) |> Seq.skip 1
        ]