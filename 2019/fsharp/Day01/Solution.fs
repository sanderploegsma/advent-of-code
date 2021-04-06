module AdventOfCode2019.Day01.Solution

let calculateFuelRequirements mass =
    float mass
    |> fun x -> x / 3.0
    |> floor
    |> fun x -> x - 2.0
    |> int

let rec calculateFuelRequirementsRec mass =
    let fuel = calculateFuelRequirements mass
    if fuel < 0 then 0 else fuel + calculateFuelRequirementsRec fuel

let sumFuelRequirements: seq<int> -> int = Seq.sumBy calculateFuelRequirements

let sumFuelRequirementsRec: seq<int> -> int = Seq.sumBy calculateFuelRequirementsRec
