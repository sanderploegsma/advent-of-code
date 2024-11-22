module Day02

open System.IO

type Instruction = Up | Down | Left | Right

let parseInstruction instruction =
    match instruction with
    | 'U' -> Up
    | 'D' -> Down
    | 'L' -> Left
    | 'R' -> Right
    | _ -> failwithf "Unknown instruction %c" instruction

let rec move (p: int) sequence =
    match sequence with
    | [] -> p
    | Up :: tail when List.contains p [1;2;3] -> move p tail
    | Down :: tail when List.contains p [7;8;9] -> move p tail
    | Left :: tail when List.contains p [1;4;7] -> move p tail
    | Right :: tail when List.contains p [3;6;9] -> move p tail
    | Up :: tail -> move (p - 3) tail
    | Down :: tail -> move (p + 3) tail
    | Left :: tail -> move (p - 1) tail
    | Right :: tail -> move (p + 1) tail

let rec move2 (p: int) sequence =
    match sequence with
    | [] -> p
    | Up :: tail when List.contains p [5;2;1;4;9] -> move2 p tail
    | Down :: tail when List.contains p [5;0xA;0xD;0xC;9] -> move2 p tail
    | Left :: tail when List.contains p [1;2;5;0xA;0xD] -> move2 p tail
    | Right :: tail when List.contains p [1;4;9;0xC;0xD] -> move2 p tail
    | Up :: tail when p = 3 || p = 0xD -> move2 (p - 2) tail
    | Up :: tail -> move2 (p - 4) tail
    | Down :: tail when p = 1 || p = 0xB -> move2 (p + 2) tail
    | Down :: tail -> move2 (p + 4) tail
    | Left :: tail -> move2 (p - 1) tail
    | Right :: tail -> move2 (p + 1) tail

let findCode instructions moveFn =
    let getDigit state sequence =
        let digit = moveFn state sequence
        (digit, digit)

    let (digits, _) =
        instructions
        |> List.mapFold getDigit 5

    digits

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.map (Seq.map parseInstruction >> Seq.toList)
        |> Seq.toList

    printfn "Part one: %A" (findCode input move)
    printfn "Part two: %A" (findCode input move2)
    0 // return an integer exit code
