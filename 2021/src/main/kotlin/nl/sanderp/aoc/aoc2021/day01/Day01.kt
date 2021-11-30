package nl.sanderp.aoc.aoc2021.day01

import nl.sanderp.aoc.aoc2021.measureDuration
import nl.sanderp.aoc.aoc2021.prettyPrint
import nl.sanderp.aoc.aoc2021.readResource

class Day01(private val input: String) {
    fun partOne() = input
    fun partTwo() = input.reversed()
}

fun main() {
    val input = readResource("Day01.txt")
    val solver = Day01(input)

    val (answer1, duration1) = measureDuration<String> { solver.partOne() }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<String> { solver.partTwo() }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}