namespace Common

module Navigation =

    type Coordinate = int * int
    type Coordinate3D = int * int * int
    type Coordinate4D = int * int * int * int

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

    /// Get the neighbours of a 2D coordinate
    let neighbours8 c =
        let (x, y) = c
        seq {
            for x' in x-1 .. x+1 do
                for y' in y-1 .. y+1 do
                    (x', y')
        }
        |> Seq.filter (fun c' -> c' <> c)
        |> Seq.toList

    /// Get the neighbours of a 3D coordinate
    let neighbours3D c =
        let (x, y, z) = c
        seq {
            for x' in x-1 .. x+1 do
                for y' in y-1 .. y+1 do
                    for z' in z-1 .. z+1 do
                        (x', y', z')
        }
        |> Seq.filter (fun c' -> c' <> c)
        |> Seq.toList

    /// Get the neighbours of a 4D coordinate
    let neighbours4D c =
        let (x, y, z, w) = c
        seq {
            for x' in x-1 .. x+1 do
                for y' in y-1 .. y+1 do
                    for z' in z-1 .. z+1 do
                        for w' in w-1 .. w+1 do
                            (x', y', z', w')
        }
        |> Seq.filter (fun c' -> c' <> c)
        |> Seq.toList

    /// Get all coordinates that are within a direct line of sight from the given coordinate
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
