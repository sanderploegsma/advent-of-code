package nl.sanderp.aoc.aoc2021.day03

import nl.sanderp.aoc.common.measureDuration
import nl.sanderp.aoc.common.prettyPrint
import nl.sanderp.aoc.common.readResource

data class Result(val a: String, val b: String) {
    val product by lazy { a.toLong(2) * b.toLong(2) }
}

fun main() {
    val input = readResource("Day03.txt").lines()

    val (answer1, duration1) = measureDuration<Result> { partOne(input) }
    println("Part one: ${answer1.product} (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Result> { partTwo(input) }
    println("Part two: ${answer2.product} (took ${duration2.prettyPrint()})")
}

private fun partOne(input: List<String>): Result {
    val bitCounts = (0 until input.first().length).map { i -> input.map { it[i] }.bitCount() }

    val gamma = bitCounts.map { it.max.first }.joinToString("")
    val epsilon = bitCounts.map { it.min.first }.joinToString("")

    return Result(gamma, epsilon)
}

private fun partTwo(input: List<String>): Result {
    var oxygenGenerator = input
    var co2Scrubber = input

    for (i in 0 until input.first().length) {
        val oxygenGeneratorBit =  oxygenGenerator.map { it[i] }.bitCount().maxOrDefault
        oxygenGenerator = oxygenGenerator.filter { it[i] == oxygenGeneratorBit }

        val co2ScrubberBit = co2Scrubber.map { it[i] }.bitCount().minOrDefault
        co2Scrubber = co2Scrubber.filter { it[i] == co2ScrubberBit }
    }

    return Result(oxygenGenerator.first(), co2Scrubber.first())
}

data class BitCount(val min: Pair<Char, Int>, val max: Pair<Char, Int>) {
    val minOrDefault = if (min.second < max.second) min.first else '0'
    val maxOrDefault = if (max.second > min.second) max.first else '1'
}

private fun List<Char>.bitCount(): BitCount {
    val count = this.groupingBy { it }.eachCount()
    return BitCount(min = count.minByOrNull { it.value }!!.toPair(), max = count.maxByOrNull { it.value }!!.toPair())
}