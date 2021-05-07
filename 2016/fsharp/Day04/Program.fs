module Day04

open System
open System.IO

type Room = { Name: string; SectorID: int; Checksum: string }

let parseRoom (line: string) =
    let room = line.Substring(0, line.Length - 7)
    let checksum = line.Substring(line.Length - 6, 5)
    let nameIdSeparator = line.LastIndexOf('-')
    let name = room.Substring(0, nameIdSeparator)
    let id = room.Substring(nameIdSeparator + 1, room.Length - nameIdSeparator - 1) |> int
    { Name = name; SectorID = id; Checksum = checksum }

let calculateChecksum =
    Seq.filter Char.IsLetter
    >> Seq.countBy id
    >> Seq.sortBy (fun (c, _) -> c)
    >> Seq.sortByDescending (fun (_, count) -> count)
    >> Seq.map (fun (c, _) -> c)
    >> Seq.take 5
    >> Seq.toArray
    >> String

let matchesChecksum room =
    let actualChecksum = calculateChecksum room.Name
    room.Checksum = actualChecksum

let decryptName room =
    let shift c =
        match c with
        | '-' -> ' '
        | ' ' -> ' '
        | 'z' -> 'a'
        | _ -> char (int c + 1)

    let shiftN c =
        { 1 .. room.SectorID }
        |> Seq.fold (fun c' _ -> shift c') c

    let decryptedName = 
        room.Name
        |> Seq.map shiftN
        |> Seq.toArray
        |> String

    { room with Name = decryptedName }

let partOne = 
    List.filter matchesChecksum
    >> List.sumBy (fun room -> room.SectorID)

let partTwo =
    List.filter matchesChecksum
    >> List.map decryptName
    >> List.find (fun room -> room.Name = "northpole object storage")

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.map parseRoom
        |> Seq.toList
        
    partOne input |> printfn "Part one: %d"
    partTwo input |> printfn "Part two: %A"
    0
