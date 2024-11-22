open System

let play cups moves =
    let maxCup = List.max cups
    let nextCup cup = if cup = 1 then maxCup else cup - 1

    let mutable links = Array.init (moves + 1) (fun _ -> -1)
    links.[Seq.last cups] <- Seq.head cups
    for l, r in Seq.pairwise cups do
        links.[l] <- r

    let mutable current = Seq.head cups
    for _ in 0 .. moves - 1 do
        // Take out three cups next to current
        let mutable picked = [links.[current]]
        for _ in 0 .. 1 do
            picked <- picked @ [links.[List.last picked]]

        // Link current to the cup after the picked three
        links.[current] <- links.[List.last picked]

        // Select destination cup
        let mutable destination = nextCup current
        while List.contains destination picked do
            destination <- nextCup destination

        // Place three cups after destination
        let nextToDest = links.[destination]
        links.[destination] <- List.head picked
        links.[List.last picked] <- nextToDest
        current <- links.[current]

    seq {
        let mutable cup = 1
        yield cup
        while links.[cup] <> 1 do
            yield links.[cup]
            cup <- links.[cup]
    }

[<EntryPoint>]
let main argv =
    let input =
        "586439172"
        |> Seq.map (fun c -> Char.GetNumericValue(c) |> int)
        |> Seq.toList

    play input 100
    |> Seq.skip 1
    |> Seq.fold (sprintf "%s%d") ""
    |> printfn "Part one: %s"

    let bigInput = input @ Seq.toList (seq { 10 .. 1_000_000 })

    play bigInput 10_000_000
    |> Seq.skip 1
    |> Seq.take 2
    |> Seq.map int64
    |> Seq.fold (*) 1L
    |> printfn "Part two: %d"

    0
