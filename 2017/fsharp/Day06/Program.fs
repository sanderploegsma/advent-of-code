module Day06

open System.IO

let redistribute (memory: byref<int[]>) =
    let mutable (position, value) = memory |> Array.indexed |> Array.maxBy (fun (_, v) -> v)
    memory.[position] <- 0
    while value > 0 do
        position <- (position + 1) % Array.length memory
        memory.[position] <- memory.[position] + 1
        value <- value - 1

let findLoop input =
    let mutable memory = List.toArray input
    let mutable steps = 0
    
    let mutable visited = []
    
    while List.contains memory visited |> not do
        visited <- visited @ [Array.copy memory]
        steps <- steps + 1
        redistribute &memory
    (steps, Array.toList memory)

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText("input.txt").Split("\t") |> Seq.map int |> Seq.toList
    
    let (steps1, sequence) = findLoop input
    printfn "Part one: %d" steps1
    
    let (steps2, _) = findLoop sequence
    printfn "Part two: %d" steps2
    
    0
