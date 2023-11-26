module Day05

open System
open System.IO

let isNice (input: string) =
    // It contains at least three vowels (aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
    let rule1 = 
        let vowels = ['a';'e';'i';'o';'u']
        Seq.filter (fun c -> List.contains c vowels) input |> Seq.length >= 3
    
    // It contains at least one letter that appears twice in a row, like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd).
    let rule2 = input |> Seq.pairwise |> Seq.exists (fun (a, b) -> a = b)
    
    // It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.
    let rule3 =
        ["ab";"cd";"pq";"xy"]
        |> List.map (fun str -> input.Contains(str))
        |> List.forall not

    [rule1; rule2; rule3] |> List.forall id

let isNice2 (input: string) =
    // It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
    let rule1 =
        Seq.windowed 2 input
        |> Seq.map (Seq.toArray >> String)
        |> Seq.indexed
        |> Seq.filter (fun (i, s) -> input.IndexOf(s, i + 2) >= 0)
        |> Seq.length > 0

    // It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.
    let rule2 = 
        Seq.windowed 3 input 
        |> Seq.map Seq.toArray 
        |> Seq.exists (fun chars -> chars.[0] = chars.[2])
    
    [rule1; rule2] |> List.forall id

let partOne = List.filter isNice >> List.length
let partTwo = List.filter isNice2 >> List.length

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.toList
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0
