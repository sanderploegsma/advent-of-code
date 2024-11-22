package nl.sanderp.aoc.aoc2021.day09

import nl.sanderp.aoc.common.*

fun main() {
    val input = readResource("Day09.txt")
        .lines()
        .flatMapIndexed { y, s -> s.mapIndexed { x, c -> Point2D(x, y) to c.asDigit() } }
        .toMap()

    val (answer1, duration1) = measureDuration<Int> { partOne(input) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> { partTwo(input) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private fun findLowPoints(input: Map<Point2D, Int>) =
    input.filter { it.key.pointsNextTo().mapNotNull { p -> input[p] }.all { h -> h > it.value } }

private fun partOne(input: Map<Point2D, Int>): Int = findLowPoints(input).values.sumOf { 1 + it }

private fun partTwo(input: Map<Point2D, Int>): Int {
    val lowPoints = findLowPoints(input).keys
    val basins = mutableListOf<Set<Point2D>>()

    for (p in lowPoints) {
        val basin = mutableSetOf(p)
        var newPoints = setOf<Point2D>()

        do {
            basin.addAll(newPoints)

            newPoints = basin
                .flatMap { x -> x.pointsNextTo().filter { input[it] != null && input[it]!! < 9 } }
                .filterNot { basin.contains(it) }
                .toSet()
        } while (newPoints.isNotEmpty())

        basins.add(basin)
    }

    return basins.map { it.size }.sortedDescending().take(3).fold(1) { a, b -> a * b }
}
