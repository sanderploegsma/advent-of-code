module Day22

open System.IO

type Winner =
    | Player1 of int list
    | Player2 of int list

let rec play deck1 deck2 =
    match deck1, deck2 with
    | d1, [] -> Player1 d1
    | [], d2 -> Player2 d2
    | c1 :: d1, c2 :: d2 when c1 > c2 ->
        play (d1 @ [c1; c2]) d2
    | c1 :: d1, c2 :: d2 when c2 > c1 ->
        play d1 (d2 @ [c2; c1])

let rec play2 player1 player2 =
    let rec playRound deck1 deck2 cache =
        let cache' = deck1 :: cache
        match deck1, deck2 with
        | d1, [] -> Player1 d1
        | [], d2 -> Player2 d2
        | d1, _ when List.contains d1 cache -> Player1 d1
        | c1 :: d1, c2 :: d2 when c1 <= List.length d1 && c2 <= List.length d2 ->
            match play2 (List.take c1 d1) (List.take c2 d2) with
            | Player1 _ -> playRound (d1 @ [c1; c2]) d2 cache'
            | Player2 _ -> playRound d1 (d2 @ [c2; c1]) cache'
        | c1 :: d1, c2 :: d2 when c1 > c2 ->
            playRound (d1 @ [c1; c2]) d2 cache'
        | c1 :: d1, c2 :: d2 when c2 > c1 ->
            playRound d1 (d2 @ [c2; c1]) cache'

    playRound player1 player2 []

let score deck =
    List.rev deck 
    |> List.mapi (fun i c -> (i+1) * c) 
    |> List.sum

[<EntryPoint>]
let main argv =
    let player1, player2 =
        File.ReadAllText("Input.txt").Split("\r\n\r\n")
        |> Array.map (fun s -> s.Split("\r\n"))
        |> Array.map (Array.skip 1 >> Array.map int >> Array.toList)
        |> fun players -> players.[0], players.[1]

    match play player1 player2 with
    | Player1 deck -> printfn "Part one: Player 1 won with score %d" (score deck)
    | Player2 deck -> printfn "Part one: Player 2 won with score %d" (score deck) 

    match play2 player1 player2 with
    | Player1 deck -> printfn "Part two: Player 1 won with score %d" (score deck)
    | Player2 deck -> printfn "Part two: Player 2 won with score %d" (score deck) 
    0
