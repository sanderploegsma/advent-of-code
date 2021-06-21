module Day12

open System.IO
open System.Text.RegularExpressions

type Register =
    | A
    | B
    | C
    | D

type Instruction =
    | CopyLiteralValue of Value: int * Destination: Register
    | CopyRegisterValue of Source: Register * Destination: Register
    | IncreaseRegisterValue of Destination: Register
    | DecreaseRegisterValue of Destination: Register
    | JumpLiteralValueNonZero of Value: int * Offset: int
    | JumpRegisterValueNonZero of Source: Register * Offset: int

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)

    if m.Success then
        Some(List.tail [ for g in m.Groups -> g.Value ])
    else
        None

let register =
    function
    | "a" -> A
    | "b" -> B
    | "c" -> C
    | "d" -> D
    | str -> failwithf "Invalid register %s" str

let parse =
    function
    | Regex @"cpy ([abcd]) ([abcd])" [ source; dest ] -> CopyRegisterValue(register source, register dest)
    | Regex @"cpy (-?\d+) ([abcd])" [ value; dest ] -> CopyLiteralValue(int value, register dest)
    | Regex @"inc ([abcd])" [ dest ] -> IncreaseRegisterValue(register dest)
    | Regex @"dec ([abcd])" [ dest ] -> DecreaseRegisterValue(register dest)
    | Regex @"jnz ([abcd]) (-?\d+)" [ source; offset ] -> JumpRegisterValueNonZero(register source, int offset)
    | Regex @"jnz (\d+) (-?\d+)" [ value; offset ] -> JumpLiteralValueNonZero(int value, int offset)
    | str -> failwithf "Invalid instruction %s" str

let rec run memory instructions i =
    if i >= Array.length instructions then
        memory
    else
        match instructions.[i] with
        | CopyLiteralValue (value, dest) -> run (Map.add dest value memory) instructions (i + 1)
        | CopyRegisterValue (source, dest) -> run (Map.add dest memory.[source] memory) instructions (i + 1)
        | IncreaseRegisterValue dest -> run (Map.add dest (memory.[dest] + 1) memory) instructions (i + 1)
        | DecreaseRegisterValue dest -> run (Map.add dest (memory.[dest] - 1) memory) instructions (i + 1)
        | JumpLiteralValueNonZero (value, offset) when value <> 0 -> run memory instructions (i + offset)
        | JumpRegisterValueNonZero (source, offset) when memory.[source] <> 0 -> run memory instructions (i + offset)
        | _ -> run memory instructions (i + 1)

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines "Input.txt"
        |> Seq.map parse
        |> Seq.toArray

    let memory1 =
        run ([ (A, 0); (B, 0); (C, 0); (D, 0) ] |> Map.ofSeq) input 0

    printfn "Part one: %d" memory1.[A]

    let memory2 =
        run ([ (A, 0); (B, 0); (C, 1); (D, 0) ] |> Map.ofSeq) input 0

    printfn "Part two: %d" memory2.[A]

    0
