module Day13

open System.IO

let parse (lines: string array) =
    let t = int64 lines.[0]
    let buses =
        lines.[1].Split(",")
        |> Array.mapi (fun i s -> if s = "x" then None else Some (int64 i, int64 s))
        |> Array.choose id
        |> Array.toList

    t, buses

let partOne t buses =
    let waitTime busId = (busId - (t % busId)) % busId

    let (busId, t') =
        buses
        |> Seq.map (fun (_, busId) -> (busId, waitTime busId))
        |> Seq.minBy snd

    busId * t'

// Source: https://rosettacode.org/wiki/Modular_inverse#F.23
let MI n g =
    let rec fN n i g e l a =
        match e with
        | 0L -> g
        | _ -> let o = n/e
               fN e l a (n-o*e) (i-o*l) (g-o*a)
    (n+(fN n 1L 0L g 0L 1L))%n

let partTwo buses =
    let N =
        buses
        |> Seq.map snd
        |> Seq.fold (*) 1L

    buses
        |> Seq.map (fun (offset, busId) -> (busId - offset, busId))
        // Source: https://rosettacode.org/wiki/Chinese_remainder_theorem#F.23
        |> Seq.fold (fun n (rem, busId) -> n+rem*(N/busId)*(MI busId ((N/busId)%busId))) 0L
        |> fun n -> n % N

[<EntryPoint>]
let main argv =
    let (t, buses) = File.ReadAllLines("Input.txt") |> parse

    partOne t buses |> printfn "Part one: %d"
    partTwo buses |> printfn "Part two: %d"
    0
