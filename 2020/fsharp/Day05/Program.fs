module Day05

open System.IO

let findPosition lst positions leftOption rightOption =
    let rec find lst' positions' =
        let (left, right) = List.splitAt (List.length lst' / 2) lst'
        match positions' with
        | [] -> List.item 0 right
        | position :: tail when position = leftOption -> find left tail
        | position :: tail when position = rightOption -> find right tail
        | _ -> failwithf "Invalid position in list %A" positions

    find lst positions

let findSeat (input: string) =
    let rows = seq { 0 .. 127 } |> Seq.toList
    let cols = seq { 0 .. 7 } |> Seq.toList
    let rowPositions = input.Substring(0, 7) |> Seq.toList
    let colPositions = input.Substring(7, 3) |> Seq.toList
    let row = findPosition rows rowPositions 'F' 'B'
    let col = findPosition cols colPositions 'L' 'R'
    (row, col)

let toSeatId (row, col) = row * 8 + col

let toSeats = List.map findSeat >> List.map toSeatId

let findMissingSeat seats =
    let allSeats = seq {
        for row in 1 .. 126 do // Not in front or back
            for col in 0 .. 7 do
                (row, col)
    }
    let allSeatIds = allSeats |> Seq.map toSeatId
    allSeatIds
    |> Seq.find (fun id -> List.contains id seats |> not)

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.toList

    let seats = toSeats input
    List.max seats |> printfn "Part one: %d"
    findMissingSeat seats |> printfn "Part two %d"

    0
