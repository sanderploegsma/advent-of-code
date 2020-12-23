open System

let play cups moves: seq<int> =
    let maxCup = List.max cups
    let nextCup cup = if cup = 1 then maxCup else cup - 1

    let mutable links = cups |> Seq.pairwise |> Map.ofSeq
    links <- links.Add(Seq.last cups, Seq.head cups)

    let mutable current = Seq.head cups

    for _ in 0 .. moves - 1 do
        // Take out three cups next to current
        let mutable pick = [links.[current]]
        for _ in 0 .. 1 do
            pick <- pick @ [links.[List.last pick]]

        // Link current to the cup after the picked three
        links <- links.Add(current, (links.[List.last pick]))

        // Select destination cup
        let mutable dest = nextCup current
        while List.contains dest pick do
            dest <- nextCup dest

        // Place three cups after destination
        let destLink = links.[dest]
        links <- links.Add(dest, List.head pick).Add(List.last pick, destLink)
        current <- links.[current]

    seq {
        let mutable pointer = 1
        while links.[pointer] <> 1 do
            yield links.[pointer]
            pointer <- links.[pointer]
    }

[<EntryPoint>]
let main argv =
    let input = "586439172" |> Seq.map (fun c -> Char.GetNumericValue(c) |> int) |> Seq.toList

    let partOne = play input 100 |> Seq.takeWhile (fun n -> n <> 1) |> Seq.fold (sprintf "%s%d") ""
    printfn "Part one: %s" partOne

    let bigInput = Seq.append input (seq { 10 .. 1_000_000 }) |> Seq.toList
    let partTwo = play bigInput 10_000_000 |> Seq.take 2 |> Seq.map int64 |> Seq.fold (*) 1L
    printfn "Part two: %d" partTwo

    0
