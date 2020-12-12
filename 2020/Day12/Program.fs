module Day12

open Common.Math
open Common.Navigation
open System.IO

let moveX pos dir x = 
    List.replicate x dir 
    |> List.fold move pos

let rotateDeg dir rot deg = 
    List.replicate (deg / 90) rot 
    |> List.fold turn dir

let partOne input = 
    let rec traverse pos dir instructions =
        match instructions with
        | [] -> pos
        | ('F', x) :: tail -> traverse (moveX pos dir x) dir tail
        | ('N', x) :: tail -> traverse (moveX pos North x) dir tail
        | ('E', x) :: tail -> traverse (moveX pos East x) dir tail
        | ('S', x) :: tail -> traverse (moveX pos South x) dir tail
        | ('W', x) :: tail -> traverse (moveX pos West x) dir tail
        | ('L', deg) :: tail -> traverse pos (rotateDeg dir Left deg) tail
        | ('R', deg) :: tail -> traverse pos (rotateDeg dir Right deg) tail

    manhattan (0, 0) (traverse (0, 0) East input)

let partTwo input =
    let rec traverse ship waypoint instructions =
        let moveShip x = 
            List.replicate x waypoint
            |> List.fold addCoordinates ship

        let rotateWaypoint deg =
            let (x, y) = waypoint
            let (newX, newY) = rotate (double x, double y) (double deg)
            (round newX |> int, round newY |> int)

        match instructions with
        | [] -> ship
        | ('F', x) :: tail -> traverse (moveShip x) waypoint tail
        | ('N', x) :: tail -> traverse ship (moveX waypoint North x) tail
        | ('E', x) :: tail -> traverse ship (moveX waypoint East x) tail
        | ('S', x) :: tail -> traverse ship (moveX waypoint South x) tail
        | ('W', x) :: tail -> traverse ship (moveX waypoint West x) tail
        | ('L', deg) :: tail -> traverse ship (rotateWaypoint deg) tail
        | ('R', deg) :: tail -> traverse ship (rotateWaypoint (-1 * deg)) tail

    manhattan (0, 0) (traverse (0, 0) (10, 1) input)

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt")
        |> Seq.map (fun s -> (s.[0], s.Substring(1) |> int))
        |> Seq.toList
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
