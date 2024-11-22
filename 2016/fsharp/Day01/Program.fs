module Day01

open System.IO

type Rotation = Left | Right
type Instruction = Rotation * int

let parseInstruction (instruction: string) =
    match instruction.[0], instruction.Substring(1) with
    | 'L', steps -> (Left, (int steps))
    | 'R', steps -> (Right, (int steps))
    | _, _ -> failwithf "Invalid instruction %s" instruction

type Position = int * int
type Direction = North | East | South | West
type State = Position list * Direction

let move (state: State) (instruction: Instruction): State =
    let (path, direction) = state
    let (rotation, steps) = instruction

    let newDirection =
        match direction, rotation with
        | North, Right | South, Left -> East
        | East, Right | West, Left -> South
        | South, Right | North, Left -> West
        | West, Right | East, Left -> North

    let (x, y) = List.last path
    let takeStep: int -> Position =
        match newDirection with
        | North -> (fun dy -> (x, y + dy))
        | East -> (fun dx -> (x + dx, y))
        | South -> (fun dy -> (x, y - dy))
        | West -> (fun dx -> (x - dx, y))

    let pathSegment =
        seq { 1 .. steps }
        |> Seq.map takeStep
        |> Seq.toList

    (List.append path pathSegment, newDirection)

let traverse instructions =
    let initialState = ([(0, 0)], North)
    let (path, _) = instructions |> List.fold move initialState
    path

let distanceFromOrigin (x, y) = (abs x) + (abs y)

let firstPositionVisitedTwice (path: Position list) =
    let visitedMoreThanOnce =
        path
        |> List.countBy id
        |> List.filter (fun (_, count) -> count > 1)
        |> List.map (fun (p, _) -> p)

    let isVisitedMoreThanOnce p = List.contains p visitedMoreThanOnce

    path |> List.find isVisitedMoreThanOnce

[<EntryPoint>]
let main argv =
    let input =
        File.ReadAllText("Input.txt").Split(", ")
        |> Array.map parseInstruction
        |> Array.toList

    let path = traverse input
    printfn "Part one: %d" (distanceFromOrigin (List.last path))
    printfn "Part two: %d" (distanceFromOrigin (firstPositionVisitedTwice path))
    0
