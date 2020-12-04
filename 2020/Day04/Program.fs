module Day04

open System
open System.IO
open System.Text.RegularExpressions

let isValidHeight (height: string) =
    let hasValidValue (h: string) =
        match h.Substring(h.Length - 2), h.Substring(0, h.Length - 2) with
        | "cm", cm -> cm >= "150" && cm <= "193"
        | "in", inch -> inch >= "59" && inch <= "76"
        | unit, _ -> failwithf "Invalid height unit %s" unit

    let containsUnit (h: string) = 
        ["cm"; "in"] 
        |> List.exists (fun unit -> h.Contains(unit))

    containsUnit height && hasValidValue height

let isValidField (field: string) =
    let keyAndValue = field.Split(":")
    match keyAndValue.[0], keyAndValue.[1] with
    | "byr", birthYear -> birthYear >= "1920" && birthYear <= "2002"
    | "iyr", issueYear -> issueYear >= "2010" && issueYear <= "2020"
    | "eyr", expirationYear -> expirationYear >= "2020" && expirationYear <= "2030"
    | "hgt", height -> isValidHeight height
    | "hcl", hairColor -> Regex.IsMatch(hairColor, "^#[0-9a-f]{6}$")
    | "ecl", eyeColor -> List.contains eyeColor ["amb";"blu";"brn";"gry";"grn";"hzl";"oth"]
    | "pid", passportId -> Regex.IsMatch(passportId, "^[0-9]{9}$")
    | "cid", _ -> true
    | field, _ -> failwithf "Invalid field %s" field

let hasValidFields (passport: string) =
    let delimiters = [|" "; "\r\n"|]
    let fields = passport.Split(delimiters, StringSplitOptions.None)
    Array.forall isValidField fields

let hasRequiredFields (passport: string) =
    ["byr";"iyr";"eyr";"hgt";"hcl";"ecl";"pid"]
    |> List.map (sprintf "%s:")
    |> List.forall (fun field -> passport.Contains(field))

let partOne = List.filter hasRequiredFields
let partTwo = List.filter hasRequiredFields >> List.filter hasValidFields

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText("input.txt").Split("\r\n\r\n") |> Array.toList
    partOne input |> List.length |> printfn "Part one: %d"
    partTwo input |> List.length |> printfn "Part two: %d"
    0
