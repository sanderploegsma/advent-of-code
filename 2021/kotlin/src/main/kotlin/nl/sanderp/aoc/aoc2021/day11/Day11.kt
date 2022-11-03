package nl.sanderp.aoc.aoc2021.day11

import nl.sanderp.aoc.common.*
import java.util.*

fun main() {
    val input = readResource("Day11.txt").lines().map { line -> line.map { it.asDigit() } }

    val (answer1, duration1) = measureDuration<Int> { partOne(input) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> { partTwo(input) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private fun partOne(grid: List<List<Int>>): Int {
    val state = grid.map { it.toTypedArray() }.toTypedArray()
    return (1..100).fold(0) { x, _ -> x + step(state) }
}

private fun partTwo(grid: List<List<Int>>): Int {
    val state = grid.map { it.toTypedArray() }.toTypedArray()
    var flashes = 0
    var i = 0

    while (flashes < 100) {
        flashes = step(state)
        i += 1
    }

    return i
}

private fun step(state: Array<Array<Int>>): Int {
    val willFlash = LinkedList<Point2D>()
    val flashed = mutableSetOf<Point2D>()

    for (y in 0..9) {
        for (x in 0 ..9) {
            state[y][x] += 1

            if (state[y][x] > 9) {
                willFlash.add(x to y)
            }
        }
    }

    while (willFlash.isNotEmpty()) {
        val point = willFlash.poll()

        if (flashed.add(point)) {
            for (p in point.pointsAround().filter { (0..9).contains(it.x) && (0..9).contains(it.y) }) {
                state[p.y][p.x] += 1

                if (state[p.y][p.x] > 9) {
                    willFlash.add(p)
                }
            }
        }
    }

    for (point in flashed) {
        state[point.y][point.x] = 0
    }

    return flashed.size
}