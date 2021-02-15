open System.IO
open System.Text.RegularExpressions

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)

    if m.Success
    then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

type Event =
    | GuardBeginsShift of int
    | GuardFallsAsleep
    | GuardWakesUp

type Record =
    { Date: string
      Minute: int
      Event: Event }

let parse (line: string) =
    match line with
    | Regex @"\[(\d{4}-\d{2}-\d{2}) \d{2}:(\d{2})\] Guard #(\d+) begins shift" [ date; minute; guard ] ->
        { Date = date
          Minute = int minute
          Event = GuardBeginsShift(int guard) }
    | Regex @"\[(\d{4}-\d{2}-\d{2}) \d{2}:(\d{2})\] falls asleep" [ date; minute ] ->
        { Date = date
          Minute = int minute
          Event = GuardFallsAsleep }
    | Regex @"\[(\d{4}-\d{2}-\d{2}) \d{2}:(\d{2})\] wakes up" [ date; minute ] ->
        { Date = date
          Minute = int minute
          Event = GuardWakesUp }
    | _ -> failwithf "Unable to parse input: %s" line

type State =
    { CurrentGuard: int
      AsleepSince: int option }

type SleepingGuard = { Guard: int; Minute: int }

/// Construct a sequence of sleeping guards - the guard ID x each minute they are asleep - from a sequence of records
let createSchedule records =
    let nextState state record =
        match record, state with
        | { Event = GuardBeginsShift (guard) }, _ ->
            Seq.empty,
            { CurrentGuard = guard
              AsleepSince = None }
        | { Event = GuardFallsAsleep }, _ ->
            Seq.empty,
            { state with
                  AsleepSince = Some(record.Minute) }
        | { Event = GuardWakesUp }, { AsleepSince = Some (asleepSince) } ->
            Seq.init (record.Minute - asleepSince) (fun i ->
                { Guard = state.CurrentGuard
                  Minute = asleepSince + i }),
            { state with AsleepSince = None }
        | _, _ -> Seq.empty, state

    let initialState =
        { CurrentGuard = -1
          AsleepSince = None }

    records
    |> Seq.mapFold nextState initialState
    |> fst
    |> Seq.collect id

/// Find the guard that spends the most minutes asleep, multiplied by the minute they are asleep the most
let partOne schedule =
    let guard, minutes =
        schedule
        |> Seq.groupBy (fun x -> x.Guard)
        |> Seq.maxBy (snd >> Seq.length)

    let minute =
        minutes
        |> Seq.countBy (fun x -> x.Minute)
        |> Seq.maxBy snd
        |> fst

    guard * minute

/// Find the guard that is most frequently asleep on the same minute
let partTwo schedule =
    let minute, guards =
        schedule
        |> Seq.groupBy (fun x -> x.Minute)
        |> Seq.maxBy (snd >> Seq.length)

    let guard =
        guards
        |> Seq.countBy (fun x -> x.Guard)
        |> Seq.maxBy snd
        |> fst

    guard * minute

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("Input.txt")
        |> Seq.map parse
        |> Seq.sortBy (fun r -> r.Date, r.Minute)

    let schedule = createSchedule input

    partOne schedule |> printfn "Part one: %d"
    partTwo schedule |> printfn "Part two: %d"

    0 // return an integer exit code
