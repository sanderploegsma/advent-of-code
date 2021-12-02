package nl.sanderp.aoc.aoc2021.day01

import nl.sanderp.aoc.aoc2021.measureDuration
import nl.sanderp.aoc.aoc2021.prettyPrint
import nl.sanderp.aoc.aoc2021.readResource

fun main() {
    val input = readResource("Day01.txt")
    val measurements = input.lines().map { it.toInt() }

    val (answer1, duration1) = measureDuration<Int> { partOne(measurements) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> { partTwo(measurements) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private fun partOne(measurements: List<Int>) =
    measurements.zipWithNext().count { (a, b) -> b > a }

private fun partTwo(measurements: List<Int>) =
    measurements.windowed(3, 1).zipWithNext().count { (a, b) -> b.sum() > a.sum() }
