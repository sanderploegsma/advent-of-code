import kotlin.math.abs

enum class Direction { Up, Down, Left, Right }
data class Instruction(val direction: Direction, val steps: Int)

fun parse(instruction: String): Instruction {
    val direction = when (instruction.first()) {
        'U' -> Direction.Up
        'D' -> Direction.Down
        'L' -> Direction.Left
        'R' -> Direction.Right
        else -> throw IllegalArgumentException()
    }

    return Instruction(direction, instruction.drop(1).toInt())
}

typealias Coordinate = Pair<Int, Int>
typealias Movement = (Coordinate) -> Coordinate

fun expand(instruction: Instruction): Iterable<Movement> = List(instruction.steps) {
    { (x, y) ->
        when (instruction.direction) {
            Direction.Up -> Pair(x, y + 1)
            Direction.Right -> Pair(x + 1, y)
            Direction.Down -> Pair(x, y - 1)
            Direction.Left -> Pair(x - 1, y)
        }
    }
}

fun generatePath(directions: Iterable<Instruction>, start: Coordinate = Coordinate(0, 0)) =
    directions
        .flatMap { expand(it) }
        .runningFold(start) { position, movement -> movement(position) }

fun main() {
    val (pathA, pathB) = readLines("day03.txt") { line -> line.split(',').map { parse(it) } }.map { generatePath(it) }

    val intersections = pathA.intersect(pathB).drop(1) // Drop (0, 0)
    println("Part one: ${intersections.map { abs(it.first) + abs(it.second) }.minOrNull()}")
    println("Part two: ${intersections.map { pathA.indexOf(it) + pathB.indexOf(it) }.minOrNull()}")
}