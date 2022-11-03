package nl.sanderp.aoc.aoc2021.day05

import nl.sanderp.aoc.common.*

fun main() {
    val input = readResource("Day05.txt").lines().map { parse(it) }

    val (answer1, duration1) = measureDuration<Int> { countOverlappingPoints(input.filter { it.type != LineType.Diagonal }) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> { countOverlappingPoints(input) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

enum class LineType { Horizontal, Vertical, Diagonal }

data class LineSegment(val start: Point2D, val end: Point2D) {
    val points by lazy {
        val xs = if (start.x < end.x) start.x..end.x else start.x downTo end.x
        val ys = if (start.y < end.y) start.y..end.y else start.y downTo end.y

        when {
            start.x == end.x -> ys.map { start.x to it }
            start.y == end.y -> xs.map { it to start.y }
            else -> xs.zip(ys)
        }
    }

    val type = when {
        start.y == end.y -> LineType.Horizontal
        start.x == end.x -> LineType.Vertical
        else -> LineType.Diagonal
    }
}

private fun parse(line: String): LineSegment {
    val (a, b) = line.split(" -> ")
        .map { p -> p.split(',').map { it.toInt() } }
        .map { (p1, p2) -> p1 to p2 }

    return LineSegment(a, b)
}

private fun countOverlappingPoints(segments: List<LineSegment>) =
    segments.flatMap { it.points }.groupingBy { it }.eachCount().filterValues { it > 1 }.keys.size
