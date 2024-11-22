module Day07

open System
open System.IO

type Address = { Supernet: string list; Hypernet: string list }

let parseAddress (address: string) =
    let segments = address.Split('[', ']')
    let supernet = Array.indexed segments |> Array.filter (fun (i, _) -> i % 2 = 0) |> Array.map (fun (_, s) -> s)
    let hypernet = Array.indexed segments |> Array.filter (fun (i, _) -> i % 2 = 1) |> Array.map (fun (_, s) -> s)
    { Supernet = Array.toList supernet; Hypernet = Array.toList hypernet }

let isAbba (chars: char array) =
    Array.rev chars = chars && Array.distinct chars |> Array.length > 1

let hasAbba (segment: string) =
    segment
    |> Seq.windowed 4
    |> Seq.exists isAbba

let getAba (segment: string) =
    segment
    |> Seq.windowed 3
    |> Seq.filter isAbba // Also works for window size 3
    |> Seq.map String
    |> Seq.toList

let supportsTLS address =
    List.exists hasAbba address.Supernet && not (List.exists hasAbba address.Hypernet)

let supportsSSL address =
    let aba = address.Supernet |> List.collect getAba
    let bab = address.Hypernet |> List.collect getAba

    let areComplements s1 s2 =
        let chars = Seq.distinct >> Seq.sort >> Seq.toArray >> String
        s1 <> s2 && chars s1 = chars s2

    List.exists (fun a -> List.exists (fun b -> areComplements a b) bab) aba

let partOne =
    List.filter supportsTLS
    >> List.length

let partTwo =
    List.filter supportsSSL
    >> List.length

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.map parseAddress
        |> Seq.toList

    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
