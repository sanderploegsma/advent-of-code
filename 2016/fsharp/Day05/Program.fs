module Day05

open System
open System.Text.RegularExpressions
open System.Security.Cryptography

// Source: http://fssnip.azurewebsites.net/lK/title/Compute-MD5-hash-of-a-string
let hash (input: string): string =
    use md5 = MD5.Create()
    input
    |> System.Text.Encoding.ASCII.GetBytes
    |> md5.ComputeHash
    |> Seq.map (fun c -> c.ToString("X2"))
    |> Seq.reduce (+)

let rec findPassword (doorId: string) (index: int) (password: string) =
    match password, hash (doorId + index.ToString()) with
    | p, _ when p.Length = 8 -> p
    | _, h when h.StartsWith("00000") -> findPassword doorId (index + 1) (password + h.[5].ToString())
    | _, _ -> findPassword doorId (index + 1) password

let rec findPassword2 (doorId: string) (index: int) (password: string) =
    let pos (h: string) = h.Substring(5, 1) |> int

    let newPassword (h: string) =
        let position = pos h
        let replacement = h.[6]
        Seq.indexed
        >> Seq.map (fun (i, c) -> if i = position then replacement else c)
        >> Seq.toArray
        >> String

    match password, hash (doorId + index.ToString()) with
    | p, _ when not (p.Contains('_')) -> p
    | p, h when Regex.IsMatch(h, "^00000[0-7]{1}") && p.[pos h] = '_' -> findPassword2 doorId (index + 1) (newPassword h password)
    | _, _ -> findPassword2 doorId (index + 1) password

let partOne input = findPassword input 0 ""
let partTwo input = findPassword2 input 0 "________"

[<EntryPoint>]
let main argv =
    let input = "cxdnnyjw"

    partOne input |> printfn "Part one: %s"
    partTwo input |> printfn "Part two: %s"
    0
