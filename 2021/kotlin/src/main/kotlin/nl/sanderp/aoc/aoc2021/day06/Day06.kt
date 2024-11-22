package nl.sanderp.aoc.aoc2021.day06

import nl.sanderp.aoc.common.increaseBy
import nl.sanderp.aoc.common.measureDuration
import nl.sanderp.aoc.common.prettyPrint
import nl.sanderp.aoc.common.readResource

fun main() {
    val input = readResource("Day06.txt").split(',').map { it.toInt() }

    val (answer1, duration1) = measureDuration<Long> { simulate(input, 80) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Long> { simulate(input, 256) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private fun simulate(initial: List<Int>, days: Int): Long {
    val initialCounts = initial.groupingBy { it }.eachCount().mapValues { it.value.toLong() }
    return simulate(initialCounts).drop(days).first().values.sum()
}

private fun simulate(initial: Map<Int, Long>) = generateSequence(initial) {
    buildMap {
        for ((x, n) in it) {
            if (x == 0) {
                increaseBy(6, n)
                set(8, n)
            } else {
                increaseBy(x - 1, n)
            }
        }
    }
}
