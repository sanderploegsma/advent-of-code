package nl.sanderp.aoc.aoc2021.day15

import nl.sanderp.aoc.aoc2021.*
import java.util.*
import kotlin.math.min

fun main() {
    val input = parse(readResource("Day15.txt"))

    val (answer1, duration1) = measureDuration<Int> { shortestPath(input) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val input2 = expand(input)
    val (answer2, duration2) = measureDuration<Int> { shortestPath(input2) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private fun parse(input: String): Map<Point2D, Int> {
    val rows = input.lines()

    return buildMap {
        rows.forEachIndexed { y, row ->
            row.forEachIndexed { x, c ->
                put(x to y, c.asDigit())
            }
        }
    }
}

private fun expand(input: Map<Point2D, Int>) = buildMap {
    val (w, h) = (input.keys.maxOf { it.x } + 1) to (input.keys.maxOf { it.y } + 1)

    for (x in 0 until (w * 5)) {
        for (y in 0 until (h * 5)) {
            val cx = x / w
            val cy = y / h
            val original = (x % w) to (y % h)
            val risk = input[original]!! + cx + cy
            put(x to y, if (risk > 9) risk - 9 else risk)
        }
    }
}

private data class Risk(val point: Point2D, val risk: Int) : Comparable<Risk> {
    override fun compareTo(other: Risk): Int = compareValues(risk, other.risk)
}

private fun shortestPath(input: Map<Point2D, Int>): Int {
    val end = input.keys.maxOf { it.x } to input.keys.maxOf { it.y }
    val remaining = input.keys.toMutableSet()

    val queue = PriorityQueue<Risk>()
    queue.add(Risk(0 to 0, 0))

    while (queue.isNotEmpty()) {
        val next = queue.poll()

        if (next.point == end) {
            return next.risk
        }

        val neighbours = next.point.pointsNextTo().filter { remaining.contains(it) }
        for (neighbour in neighbours) {
            val risk = next.risk + input[neighbour]!!
            val existing = queue.firstOrNull { it.point == neighbour }

            if (existing != null) {
                queue.remove(existing)
            }

            queue.add(Risk(neighbour, min(risk, existing?.risk ?: Int.MAX_VALUE)))
        }

        remaining.remove(next.point)
        queue.removeAll { it.point == next.point }
    }

    return -1
}
