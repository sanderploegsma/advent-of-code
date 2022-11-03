package nl.sanderp.aoc.aoc2021.day07

import nl.sanderp.aoc.common.measureDuration
import nl.sanderp.aoc.common.prettyPrint
import nl.sanderp.aoc.common.readResource
import kotlin.math.abs

fun main() {
    val input = readResource("Day07.txt").split(',').map { it.toInt() }

    val (answer1, duration1) = measureDuration<Int> { partOne(input) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> { partTwo(input) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private fun partOne(input: List<Int>) = findMinimumFuel(input) { from, to -> abs(from - to) }

private fun partTwo(input: List<Int>) = findMinimumFuel(input) { from, to -> (1..abs(from - to)).sum() }

private fun findMinimumFuel(positions: List<Int>, calculateFuel: (from: Int, to: Int) -> Int) =
    (positions.minOf { it }..positions.maxOf { it }).minOf { x -> positions.sumOf { calculateFuel(it, x) } }