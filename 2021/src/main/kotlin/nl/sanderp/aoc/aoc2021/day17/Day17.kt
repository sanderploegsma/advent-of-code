package nl.sanderp.aoc.aoc2021.day17

import nl.sanderp.aoc.aoc2021.*

private val targetX = 29..73
private val targetY = -248..-194

fun main() {
    val (answer1, duration1) = measureDuration<Int> { partOne() }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> { partTwo() }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private fun trajectory(initialVelocity: Point2D) = velocity(initialVelocity).runningFold(0 to 0) { p, v -> p + v }

private fun velocity(initial: Point2D) = generateSequence(initial) {
    val nextX = when {
        it.x > 0 -> it.x - 1
        it.x < 0 -> it.x + 1
        else -> 0
    }

    Point2D(nextX, it.y - 1)
}

private fun partOne(): Int {
    val valid = buildList {
        for (x in 0..targetX.last) {
            for (y in 0..1000) {
                val t = trajectory(x to y).takeWhile { it.x <= targetX.last && it.y >= targetY.first }

                if (t.any { it.x >= targetX.first && it.y <= targetY.last }) {
                    add(t)
                }
            }
        }
    }

    return valid.maxOf { t -> t.maxOf { it.y } }
}

private fun partTwo(): Int {
    val valid = buildList {
        for (x in 0..targetX.last) {
            for (y in targetY.first..1000) {
                val t = trajectory(x to y).takeWhile { it.x <= targetX.last && it.y >= targetY.first }

                if (t.any { it.x >= targetX.first && it.y <= targetY.last }) {
                    add(x to y)
                }
            }
        }
    }

    return valid.distinct().size
}
