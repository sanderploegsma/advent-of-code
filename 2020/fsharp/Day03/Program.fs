module Day03

open System.IO

type Point = Square | Tree

let parsePoint p =
    match p with
    | '.' -> Square
    | '#' -> Tree
    | _ -> failwithf "Unexpected character %c in grid" p

let traverse dx dy grid =
    let mutable x = 0
    let mutable y = 0
    let mutable trees = 0

    while y < Array.length grid - 1 do
        x <- (x + dx) % Array.length grid.[y]
        y <- y + dy

        if grid.[y].[x] = Tree then
            trees <- trees + 1

    trees

let partOne = traverse 3 1

let partTwo grid =
    [(1, 1); (3, 1); (5, 1); (7, 1); (1, 2)]
    |> List.map (fun (dx, dy) -> traverse dx dy grid)
    |> List.map int64
    |> List.fold (*) 1L

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("input.txt")
        |> Seq.map (Seq.map parsePoint >> Seq.toArray)
        |> Seq.toArray
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
