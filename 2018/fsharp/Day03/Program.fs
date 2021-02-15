open System.IO
open System.Text.RegularExpressions

type Claim =
    { ID: string
      X: int
      Y: int
      W: int
      H: int }

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)

    if m.Success
    then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

let parseClaim line =
    match line with
    | Regex @"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)" [ id; x; y; w; h ] ->
        { ID = id
          X = int x
          Y = int y
          W = int w
          H = int h }
    | _ -> failwithf "Unable to parse claim %s" line

let toCoords claim =
    seq {
        for x in claim.X .. claim.X + claim.W - 1 do
            for y in claim.Y .. claim.Y + claim.H - 1 do
                yield (x, y)
    }

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.map parseClaim
        |> Seq.map (fun claim -> claim.ID, toCoords claim)

    let squareCount =
        input
        |> Seq.collect snd
        |> Seq.countBy id
        |> Map.ofSeq

    let overlapCount =
        squareCount
        |> Map.filter (fun _ count -> count >= 2)
        |> Map.count

    printfn "Part one: %d" overlapCount

    let nonOverlappingClaim =
        input
        |> Seq.find (fun (_, coords) -> Seq.forall (fun xy -> squareCount.[xy] = 1) coords)
        |> fst

    printfn "Part two: %s" nonOverlappingClaim

    0 // return an integer exit code
