module Day16

open System.IO

type Range = int * int
type Rule = { Field: string; Range1: Range; Range2: Range }

let parseRule (line: string) =
    let parseRange (range: string) = 
        range.Split('-')
        |> fun s -> (int s.[0], int s.[1])

    let field :: ranges :: _ = 
        line.Split(':')
        |> Array.toList

    let range1 :: range2 :: _ = 
        ranges.Trim().Split(" or ") 
        |> Array.map parseRange 
        |> Array.toList 

    { 
        Field = field
        Range1 = range1
        Range2 = range2 
    }

let parseTicket (line: string) = 
    line.Split(',') 
    |> Array.map int
    |> Array.toList

let isInRange i (lo, hi) = lo <= i && hi >= i
let satisfiesRule i rule = isInRange i rule.Range1 || isInRange i rule.Range2

let isInvalidTicketValue rules i = List.forall (fun rule -> not (satisfiesRule i rule)) rules

let matchRulesWithTickets rules tickets =
    let rec resolve rules cols found =
        if rules = [] then found
        else
            let fitsCol rule col = List.forall (fun i -> satisfiesRule i rule) col
            let fitsCols rule =
                Map.toList cols
                |> List.filter (fun (_, col) -> fitsCol rule col)
                |> List.map fst
               
            let (rule, matches) =
                rules
                |> List.map (fun rule -> (rule, fitsCols rule))
                |> List.find (fun (_, matches) -> List.length matches = 1)

            let col = List.head matches

            resolve (List.except [rule] rules) (Map.remove col cols) (Map.add col rule found)

    let cols = List.transpose tickets |> List.indexed |> Map.ofList
    resolve rules cols Map.empty |> Map.toList

let partOne rules tickets = 
    tickets
    |> List.collect (List.filter (isInvalidTicketValue rules))
    |> List.sum

let partTwo rules tickets (ticket: int list) =
    let invalid = tickets |> List.filter (List.exists (isInvalidTicketValue rules))
    let valid = List.except invalid tickets

    matchRulesWithTickets rules valid
    |> List.filter (fun (_, rule) -> rule.Field.StartsWith("departure"))
    |> List.map (fun (i, _) -> int64 ticket.[i])
    |> List.fold (*) 1L

[<EntryPoint>]
let main argv =
    let input = 
        File.ReadLines("Input.txt") 
        |> Seq.toList

    let rules = List.take 20 input |> List.map parseRule
    let ticket = parseTicket input.[22]
    let tickets = List.skip 25 input |> List.map parseTicket
        
    partOne rules tickets |> printfn "Part one: %d"
    partTwo rules tickets ticket |> printfn "Part two: %d"
    0
