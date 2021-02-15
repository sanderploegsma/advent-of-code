open System.IO
open System.Text.RegularExpressions

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)

    if m.Success
    then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

type Guard = Guard of int

type Event =
    | GuardBeginsShift of Guard
    | GuardFallsAsleep
    | GuardWakesUp

type Record =
    { Date: string
      Minute: int
      Event: Event }

let parse (line: string): Record =
    match line with
    | Regex @"\[(\d{4}-\d{2}-\d{2}) \d{2}:(\d{2})\] Guard #(\d+) begins shift" [ date; minute; guard ] ->
        { Date = date
          Minute = int minute
          Event = GuardBeginsShift(Guard(int guard)) }
    | Regex @"\[(\d{4}-\d{2}-\d{2}) \d{2}:(\d{2})\] falls asleep" [ date; minute ] ->
        { Date = date
          Minute = int minute
          Event = GuardFallsAsleep }
    | Regex @"\[(\d{4}-\d{2}-\d{2}) \d{2}:(\d{2})\] wakes up" [ date; minute ] ->
        { Date = date
          Minute = int minute
          Event = GuardWakesUp }
    | _ -> failwithf "Unable to parse input: %s" line

type GuardState =
    | Initial
    | GuardAwake of Guard
    | GuardAsleep of Guard * asleepSince: int

type SleepingGuard = Guard * int

type Schedule = SleepingGuard seq

/// Construct a sequence of sleeping guards - the guard ID x each minute they are asleep - from a sequence of records
let createSchedule records: Schedule =
    let nextState state record =
        match state, record.Event with
        | _, GuardBeginsShift (guard) -> Seq.empty, GuardAwake(guard)
        | GuardAwake (guard), GuardFallsAsleep -> Seq.empty, GuardAsleep(guard, record.Minute)
        | GuardAsleep (guard, asleepSince), GuardWakesUp ->
            let minutesSlept = record.Minute - asleepSince
            Seq.init minutesSlept (fun i -> guard, asleepSince + i), GuardAwake(guard)
        | _, _ -> Seq.empty, state

    records
    |> Seq.mapFold nextState Initial
    |> fst
    |> Seq.collect id

/// Find the guard that spends the most minutes asleep, multiplied by the minute they are asleep the most
let partOne (schedule: Schedule): int =
    let (Guard guardId), minutes =
        schedule
        |> Seq.groupBy fst
        |> Seq.maxBy (snd >> Seq.length)

    let minute =
        minutes |> Seq.countBy snd |> Seq.maxBy snd |> fst

    guardId * minute

/// Find the guard that is most frequently asleep on the same minute
let partTwo (schedule: Schedule): int =
    let minute, guards =
        schedule
        |> Seq.groupBy snd
        |> Seq.maxBy (snd >> Seq.length)

    let (Guard guardId) =
        guards |> Seq.countBy fst |> Seq.maxBy snd |> fst

    guardId * minute

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
