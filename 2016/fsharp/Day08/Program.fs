module Day08

open System
open System.IO
open System.Text.RegularExpressions

type Instruction =
    | Rect of int * int
    | RotateRow of int * int
    | RotateCol of int * int

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)
    if m.Success
    then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

let parseInstruction instruction =
    match instruction with
    | Regex @"rect (\d+)x(\d+)" [w; h] -> Rect (int w, int h)
    | Regex @"rotate row y=(\d+) by (\d+)" [row; diff] -> RotateRow (int row, int diff)
    | Regex @"rotate column x=(\d+) by (\d+)" [col; diff] -> RotateCol (int col, int diff)
    | _ -> failwithf "Invalid instruction: %s" instruction

type Display(width: int, height: int) =
    let mutable grid = Array.init height (fun _ -> Array.init width (fun _ -> 0))

    member this.drawRect w h =
        for row in 0 .. h - 1 do
            for col in 0 .. w - 1 do
                grid.[row].[col] <- 1

    member this.rotateRow row amount =
        let originalRow = Array.copy grid.[row]
        for i in 0 .. width - 1 do
            let j = (i + amount) % width
            grid.[row].[j] <- originalRow.[i]

    member this.rotateCol col amount =
        let originalCol = grid |> Array.map (fun row -> row.[col])
        for i in 0 .. height - 1 do
            let j = (i + amount) % height
            grid.[j].[col] <- originalCol.[i]

    member this.eval instruction =
        match instruction with
        | Rect (w, h) -> this.drawRect w h
        | RotateRow (row, amount) -> this.rotateRow row amount
        | RotateCol (col, amount) -> this.rotateCol col amount

    member this.voltage () =
        grid |> Array.sumBy (Array.sum)

    member this.draw () =
        grid
        |> Array.map (Array.map (fun col -> if col = 1 then '#' else '.'))
        |> Array.map String
        |> Array.map (sprintf "%s\n")
        |> Array.fold (+) ""

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.map parseInstruction
        |> Seq.toList

    let display = new Display(50, 6)
    List.iter display.eval input

    printfn "Part one: %d" (display.voltage())
    printfn "Part two:\n%s" (display.draw())

    0
