module Day01

open System.IO

// https://stackoverflow.com/a/1231711
let rec comb n l = 
    match n, l with
    | 0, _ -> [[]]
    | _, [] -> []
    | k, (x::xs) -> List.map ((@) [x]) (comb (k-1) xs) @ comb k xs
    
let find n l =
    comb n l
    |> List.filter (fun l -> List.sum l = 2020)
    |> List.map (fun l -> List.fold (*) 1 l)
    |> List.head

let partOne = find 2
let partTwo = find 3

[<EntryPoint>]
let main argv =
    let input = File.ReadAllLines("input.txt") |> Seq.map int |> Seq.toList 
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %d"
    0 // return an integer exit code
