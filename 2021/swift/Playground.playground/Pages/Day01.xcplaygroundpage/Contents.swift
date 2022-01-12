import Algorithms

func countIncreasing(items: [Int]) -> Int {
    let pairs = zip(items.dropLast(), items.dropFirst())
    return pairs.filter { $0 < $1 }.count
}

let input = readLines(fromResource: "Day01").map { Int($0)! }

let partOne = countIncreasing(items: input)
print("Part one: \(partOne)")

let partTwo = countIncreasing(items: input.windows(ofCount: 3).map { $0.reduce(0, +) })
print("Part two: \(partTwo)")
