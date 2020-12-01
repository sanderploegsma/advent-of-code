module Day03

let findRadius n =
    Seq.initInfinite id
    |> Seq.filter (fun x -> x % 2 = 1)
    |> Seq.skipWhile (fun x -> pown x 2 < n)
    |> Seq.head
    
type Direction = Left | Right | Up | Down

let findCoordinate n radius =
    let max = radius / 2
    let min = max * -1
    
    let traverse dir (x, y) =
        match dir with
        | Left -> (x - 1, y)
        | Right -> (x + 1, y)
        | Up -> (x, y - 1)
        | Down -> (x, y + 1)
        
    let newDirection dir (x, y) =
        match dir with
        | Left when x = min -> Up
        | Up when y = min -> Right
        | Right when x = max -> Down
        | Down when y = max -> Left
        | _ -> dir
    
    let mutable value = pown radius 2
    let mutable coordinate = (max, max)
    let mutable direction = Left
    
    while value > n do
        coordinate <- traverse direction coordinate
        direction <- newDirection direction coordinate
        value <- value - 1
    
    coordinate
    
let partOne n =
    let radius = findRadius n
    let (x, y) = findCoordinate n radius
    (abs x) + (abs y)

[<EntryPoint>]
let main argv =
    let input = 325489
    
    partOne input |> printfn "Part one: %d"
    
    0 // return an integer exit code
