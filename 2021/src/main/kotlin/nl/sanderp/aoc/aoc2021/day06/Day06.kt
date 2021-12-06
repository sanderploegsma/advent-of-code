package nl.sanderp.aoc.aoc2021.day06

import nl.sanderp.aoc.aoc2021.measureDuration
import nl.sanderp.aoc.aoc2021.prettyPrint
import nl.sanderp.aoc.aoc2021.readResource

fun main() {
    val input = readResource("Day06.txt").split(',').map { it.toInt() }

    val (answer1, duration1) = measureDuration<Long> { simulate(input, 80) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Long> { simulate(input, 256) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private fun simulate(initial: List<Int>, days: Int): Long {
    var counts = initial.groupingBy { it }.eachCount().mapValues { it.value.toLong() }
    var next = mutableMapOf<Int, Long>()

    for (i in 1..days) {
        for ((x, n) in counts) {
            if (x == 0) {
                next.merge(6, n) { a, b -> a + b }
                next[8] = n
            } else {
                next.merge(x - 1, n) { a, b -> a + b }
            }
        }
        counts = next
        next = mutableMapOf()
    }

    return counts.values.sum()
}
