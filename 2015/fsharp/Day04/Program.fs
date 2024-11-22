module Day04

open System.Security.Cryptography

// Source: http://fssnip.azurewebsites.net/lK/title/Compute-MD5-hash-of-a-string
let hash (input: string): string =
    use md5 = MD5.Create()
    input
    |> System.Text.Encoding.ASCII.GetBytes
    |> md5.ComputeHash
    |> Seq.map (fun c -> c.ToString("X2"))
    |> Seq.reduce (+)

let findHashMatching (pred: string -> bool) (input: string) =
    let toHash num = hash (input + num.ToString())

    Seq.initInfinite id
    |> Seq.find (fun num -> pred (toHash num))

let partOne = findHashMatching (fun h -> h.StartsWith("00000"))
let partTwo = findHashMatching (fun h -> h.StartsWith("000000"))

[<EntryPoint>]
let main argv =
    let input = "bgvyzdsv"

    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
