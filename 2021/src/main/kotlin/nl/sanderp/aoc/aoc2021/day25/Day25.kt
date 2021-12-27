package nl.sanderp.aoc.aoc2021.day25

import nl.sanderp.aoc.aoc2021.*

private typealias Grid = Map<Point2D, Char>

fun main() {
    val input = readResource("Day25.txt").lines().flatMapIndexed { y, row ->
        row.mapIndexed { x, c -> Point2D(x, y) to c }
    }.toMap()

    val width = input.maxOf { it.key.x } + 1
    val height = input.maxOf { it.key.y } + 1

    val steps = listOf<(Grid) -> Grid>(
        { grid -> step(grid, '>') { Point2D((it.x + 1) % width, it.y) } },
        { grid -> step(grid, 'v') { Point2D(it.x, (it.y + 1) % height) } },
    )

    val (answer, duration) = measureDuration<Int> {
        generateSequence(input) { steps.fold(it) { state, step -> step(state) } }
            .zipWithNext()
            .indexOfFirst { (a, b) -> a == b } + 1
    }

    println("Answer: $answer (took ${duration.prettyPrint()})")
}

private fun step(state: Map<Point2D, Char>, direction: Char, stepFn: (Point2D) -> Point2D): Map<Point2D, Char> {
    val next = state.toMutableMap()

    for ((point, value) in state) {
        val nextPoint = stepFn(point)
        if (value == direction && state[nextPoint] == '.') {
            next[point] = '.'
            next[nextPoint] = direction
        }
    }

    return next
}