module Day09

open System.IO

let traverse (input: string) =
    let mutable level = 0
    let mutable isGarbage = false
    let mutable isCanceled = false
    let mutable score = 0
    let mutable garbage = 0

    for i in 0 .. input.Length - 1 do
        if isGarbage && isCanceled then
            isCanceled <- false
        else if isGarbage && input.[i] = '!' then
            isCanceled <- true
        else if isGarbage && input.[i] = '>' then
            isGarbage <- false
        else if isGarbage then
            garbage <- garbage + 1
        else if input.[i] = '<' then
            isGarbage <- true
        else if not isGarbage && input.[i] = '{' then
            level <- level + 1
        else if not isGarbage && input.[i] = '}' then
            score <- score + level
            level <- level - 1

    (score, garbage)

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText("input.txt")
    let (score, garbage) = traverse input
    printfn "Part one: %d" score
    printfn "Part two: %d" garbage
    0
