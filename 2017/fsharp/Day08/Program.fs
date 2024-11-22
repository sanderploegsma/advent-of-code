module Day08

open System.IO

type Register = string

type Operation =
    | Increment of Register * int
    | Decrement of Register * int

type Condition =
    | EqualTo of Register * int
    | NotEqualTo of Register * int
    | GreaterThan of Register * int
    | GreaterThanOrEqualTo of Register * int
    | LessThan of Register * int
    | LessThanOrEqualTo of Register * int

type Instruction = Operation * Condition

let parseInstruction (input: string) =
    let parseOperation register operator value =
        match operator with
        | "inc" -> Increment (register, value)
        | "dec" -> Decrement (register, value)
        | _ -> failwithf "Invalid operator %s" operator

    let parseCondition register operator value =
        match operator with
        | "==" -> EqualTo (register, value)
        | "!=" -> NotEqualTo (register, value)
        | ">" -> GreaterThan (register, value)
        | ">=" -> GreaterThanOrEqualTo (register, value)
        | "<" -> LessThan (register, value)
        | "<=" -> LessThanOrEqualTo (register, value)
        | _ -> failwithf "Invalid operator %s" operator

    let parts = input.Split(" ")
    let operation = parseOperation parts.[0] parts.[1] (int parts.[2])
    let condition = parseCondition parts.[4] parts.[5] (int parts.[6])
    (operation, condition)

let maxRegisterValue =
    Map.toSeq
    >> Seq.map (fun (_, v) -> v)
    >> Seq.max

let rec eval (instructions: List<Instruction>) (memory: Map<Register, int>) maxUsedMemory =
    let getValue register =
        if memory.ContainsKey(register) then memory.Item(register) else 0

    let isSatisfied condition =
        match condition with
        | EqualTo (register, value) -> (getValue register) = value
        | NotEqualTo (register, value) -> (getValue register) <> value
        | GreaterThan(register, value) -> (getValue register) > value
        | GreaterThanOrEqualTo (register, value) -> (getValue register) >= value
        | LessThan (register, value) -> (getValue register) < value
        | LessThanOrEqualTo (register, value) -> (getValue register) <= value

    let doOperation operation =
        match operation with
        | Increment (register, value) -> memory.Add(register, (getValue register) + value)
        | Decrement (register, value) -> memory.Add(register, (getValue register) - value)


    let newMaxUsedMemory = if memory.IsEmpty then maxUsedMemory else max maxUsedMemory (maxRegisterValue memory)

    match instructions with
    | [] -> (memory, newMaxUsedMemory)
    | (op, cond) :: tail when isSatisfied cond -> eval tail (doOperation op) newMaxUsedMemory
    | _ :: tail -> eval tail memory newMaxUsedMemory

[<EntryPoint>]
let main argv =
    let input = File.ReadLines("input.txt") |> Seq.map parseInstruction |> Seq.toList
    let (memory, limit) = eval input Map.empty 0
    printfn "Part one: %d" (maxRegisterValue memory)
    printfn "Part two: %d" limit
    0
