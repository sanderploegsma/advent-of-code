module Day15

type State = int * int * Map<int, int>

let nextNumber ((number, turn, numbers): State) =
    match Map.tryFind number numbers with
    | None -> Some((number, turn), (0, turn + 1, numbers.Add(number, turn)))
    | Some previous -> Some ((number, turn), (turn - previous, turn + 1, numbers.Add(number, turn)))

let generate input =
    let (lastNumber, lastTurn) :: previous = 
        input 
        |> List.mapi (fun i n -> (n, i + 1)) 
        |> List.rev
    
    Seq.unfold nextNumber (lastNumber, lastTurn, Map.ofList previous)

let partOne input =
    generate input
    |> Seq.find (fun (_, turn) -> turn = 2020)
    |> fst

let partTwo input =
    generate input
    |> Seq.find (fun (_, turn) -> turn = 30000000)
    |> fst

[<EntryPoint>]
let main argv =
    let input = [9;6;0;10;18;2;1]
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
