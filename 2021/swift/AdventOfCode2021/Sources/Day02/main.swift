import Foundation
import Common

enum Command {
    case forward(Int)
    case down(Int)
    case up(Int)
}

let input = Bundle.module.readLines(fromResource: "Input").map { line -> Command in
    let parts = line.split(separator: " ")
    let value = Int(parts[1])!

    switch parts[0] {
    case "down":
        return .down(value)
    case "up":
        return .up(value)
    default:
        return .forward(value)
    }
}

typealias State1 = (position: Int, depth: Int)

func reducePartOne(_ state: State1, _ command: Command) -> State1 {
    switch command {
    case let .forward(x):
        return State1(state.position + x, state.depth)
    case let .down(y):
        return State1(state.position, state.depth + y)
    case let .up(y):
        return State1(state.position, state.depth - y)
    }
}

let partOne = input.reduce(State1(position: 0, depth: 0), reducePartOne)
print("Part one: \(partOne.position * partOne.depth)")


typealias State2 = (position: Int, depth: Int, aim: Int)

func reducePartTwo(_ state: State2, _ command: Command) -> State2 {
    switch command {
    case let .forward(x):
        return State2(state.position + x, state.depth + state.aim * x, state.aim)
    case let .down(y):
        return State2(state.position, state.depth, state.aim + y)
    case let .up(y):
        return State2(state.position, state.depth, state.aim - y)
    }
}

let partTwo = input.reduce(State2(position: 0, depth: 0, aim: 0), reducePartTwo)
print("Part two: \(partTwo.position * partTwo.depth)")
