package nl.sanderp.aoc.aoc2021.day01

import nl.sanderp.aoc.aoc2021.measureDuration
import nl.sanderp.aoc.aoc2021.prettyPrint
import nl.sanderp.aoc.aoc2021.readResource

fun main() {
    val input = readResource("Day01.txt")
    val measurements = input.lines().map { it.toInt() }

    val (answer1, duration1) = measureDuration<Int> {
        measurements.zipWithNext().count { (a, b) -> b > a }
    }

    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> {
        measurements.windowed(3, 1).zipWithNext().count { (a, b) -> b.sum() > a.sum() }
    }

    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}