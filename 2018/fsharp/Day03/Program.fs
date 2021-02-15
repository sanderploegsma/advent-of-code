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
        Some
            ({ ID = id
               X = int x
               Y = int y
               W = int w
               H = int h })
    | _ -> None

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
        |> Seq.choose id
        |> Seq.collect toCoords

    let partOne =
        Seq.countBy id
        >> Seq.filter (fun (_, c) -> c >= 2)
        >> Seq.length

    printfn "Part one: %d" <| partOne input
    
    0 // return an integer exit code
