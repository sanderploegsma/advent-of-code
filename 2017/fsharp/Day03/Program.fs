module Day03

type Coordinate = int * int
type Square = Coordinate * int

type Direction =
    | Left
    | Right
    | Up
    | Down

let generate limit: seq<Square> =
    let nextCoordinate dir (x, y) =
        match dir with
        | Left -> (x - 1, y)
        | Right -> (x + 1, y)
        | Up -> (x, y - 1)
        | Down -> (x, y + 1)

    let nextDirectionAndRadius dir (x, y) r =
        let max = r / 2
        let min = max * -1

        match dir with
        | Right when x = max + 1 -> (Up, r + 2)
        | Up when y = min -> (Left, r)
        | Left when x = min -> (Down, r)
        | Down when y = max -> (Right, r)
        | _ -> (dir, r)

    seq {
        let mutable direction = Right
        let mutable radius = 1
        let mutable coordinate = (0, 0)

        for i in 1 .. limit do
            yield (coordinate, i)
            coordinate <- nextCoordinate direction coordinate

            let (newDirection, newRadius) =
                nextDirectionAndRadius direction coordinate radius

            direction <- newDirection
            radius <- newRadius
    }

let partOne n =
    let ((x, y), _) = generate n |> Seq.last
    (abs x) + (abs y)

[<EntryPoint>]
let main argv =
    let input = 325489
    partOne input |> printfn "Part one: %d"
    0 // return an integer exit code
