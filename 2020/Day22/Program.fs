module Day22

open System.IO

type Winner =
    | Player1 of int list
    | Player2 of int list

let rec play player1 player2 =
    match player1, player2 with
    | deck1, [] -> Player1 deck1
    | [], deck2 -> Player2 deck2
    | card1 :: deck1, card2 :: deck2 when card1 > card2 ->
        play (deck1 @ [card1; card2]) deck2
    | card1 :: deck1, card2 :: deck2 when card2 > card1 ->
        play deck1 (deck2 @ [card2; card1])

let rec play2 player1 player2 =
    let rec playRound deck1 deck2 cache1 cache2 =
        let cache1', cache2' = (deck1 :: cache1), (deck2 :: cache2)
        match deck1, deck2 with
        | d1, d2 when List.contains d1 cache1 && List.contains d2 cache2 -> Player1 d1
        | [], d2 -> Player2 d2
        | d1, [] -> Player1 d1
        | card1 :: d1, card2 :: d2 when card1 <= List.length d1 && card2 <= List.length d2 ->
            match play2 (List.take card1 d1) (List.take card2 d2) with
            | Player1 _ -> playRound (d1 @ [card1; card2]) d2 cache1' cache2'
            | Player2 _ -> playRound d1 (d2 @ [card2; card1]) cache1' cache2'
        | card1 :: d1, card2 :: d2 when card1 > card2 ->
            playRound (d1 @ [card1; card2]) d2 cache1' cache2'
        | card1 :: d1, card2 :: d2 when card2 > card1 ->
            playRound d1 (d2 @ [card2; card1]) cache1' cache2'

    playRound player1 player2 [] []

let score deck =
    List.rev deck 
    |> List.mapi (fun i c -> (i+1) * c) 
    |> List.sum

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.toList

    let player1 = input |> List.skip 1 |> List.takeWhile (fun s -> s <> "") |> List.map int
    let player2 = input |> List.skip (List.length player1 + 3) |> List.map int

    match play player1 player2 with
    | Player1 deck -> printfn "Part one: Player 1 won with score %d" (score deck)
    | Player2 deck -> printfn "Part one: Player 2 won with score %d" (score deck) 

    match play2 player1 player2 with
    | Player1 deck -> printfn "Part two: Player 1 won with score %d" (score deck)
    | Player2 deck -> printfn "Part two: Player 2 won with score %d" (score deck) 
    0
