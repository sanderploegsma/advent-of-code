open System
open System.IO
open System.Text.RegularExpressions

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)

    if m.Success
    then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

type Velocity =
    { Dx: int
      Dy: int }
    static member (*)(v: Velocity, n) = { Dx = n * v.Dx; Dy = n * v.Dy }

type Coordinate =
    { X: int
      Y: int }
    static member (+)(c: Coordinate, v: Velocity) = { X = c.X + v.Dx; Y = c.Y + v.Dy }

type Point =
    { Coordinate: Coordinate
      Velocity: Velocity }

    member this.move seconds =
        { this with
              Coordinate = this.Coordinate + this.Velocity * seconds }

type Rect =
    { BottomLeft: Coordinate
      TopRight: Coordinate }

    member this.size =
        (this.TopRight.X - this.BottomLeft.X)
        * (this.TopRight.Y - this.BottomLeft.Y)

let parse line =
    match line with
    | Regex @"^position=<(\s?-?\d+), (\s?-?\d+)> velocity=<(\s?-?\d+), (\s?-?\d+)>$" [ x; y; dx; dy ] ->
        { Coordinate =
              { X = int (x.Trim())
                Y = int (y.Trim()) }
          Velocity =
              { Dx = int (dx.Trim())
                Dy = int (dy.Trim()) } }
    | _ -> failwithf "Unable to parse line: %s" line

let bounds points =
    let minX, maxX, minY, maxY =
        Seq.fold (fun (minX, maxX, minY, maxY) point ->
            let { X = x; Y = y } = point.Coordinate
            min minX x, max maxX x, min minY y, max maxY y)
            (Int32.MaxValue, Int32.MinValue, Int32.MaxValue, Int32.MinValue) points

    { BottomLeft = { X = minX; Y = minY }
      TopRight = { X = maxX; Y = maxY } }

let findSmallestRect min max points =
    let movePoints seconds = points |> Seq.map (fun (point: Point) -> point.move seconds)

    Seq.init (max - min) ((+) min)
    |> Seq.map (fun seconds -> seconds, movePoints seconds)
    |> Seq.map (fun (seconds, points) -> (seconds, points, bounds points))
    |> Seq.minBy (fun (_, _, rect) -> rect.size)

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt") |> Seq.map parse

    let seconds, points, rect = findSmallestRect 9900 11000 input

    let isPoint x y =
        Seq.exists (fun p -> p.Coordinate.X = x && p.Coordinate.Y = y) points

    for y in rect.BottomLeft.Y .. rect.TopRight.Y do
        String.Concat [ for x in rect.BottomLeft.X .. rect.TopRight.X -> if isPoint x y then '#' else ' ' ]
        |> printfn "%s"

    printfn "Seconds: %d" seconds

    0
