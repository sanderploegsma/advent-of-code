package nl.sanderp.aoc.aoc2021.day01

import nl.sanderp.aoc.aoc2021.InputReader
import nl.sanderp.aoc.aoc2021.OutputWriter

class Day01(private val input: String) {
    fun partOne() = input
    fun partTwo() = input.reversed()
}

fun main() {
    val input = InputReader.read("Day01.txt")
    val solver = Day01(input)

    OutputWriter.answerPartOne { solver.partOne() }
    OutputWriter.answerPartTwo { solver.partTwo() }
}