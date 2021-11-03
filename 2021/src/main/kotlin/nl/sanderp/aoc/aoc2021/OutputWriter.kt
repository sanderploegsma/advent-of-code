package nl.sanderp.aoc.aoc2021

import java.time.Duration

object OutputWriter {
    fun answerPartOne(solve: () -> Any) = answer("Part One", solve)
    fun answerPartTwo(solve: () -> Any) = answer("Part Two", solve)

    private fun answer(prefix: String, solve: () -> Any) {
        val (answer, time) = timed(solve)

        println("[$prefix] $answer (took ${time.pretty()})")
    }

    private fun <T> timed(block: () -> T) : Pair<T, Duration> {
        val start = System.nanoTime()
        val result = block()
        val end = System.nanoTime()

        return Pair(result, Duration.ofNanos(end - start))
    }

    private fun Duration.pretty(): String {
        val parts = listOf(
            Pair(toHoursPart(), "h"),
            Pair(toMinutesPart(), "m"),
            Pair(toSecondsPart(), "s"),
            Pair(toMillisPart(), "ms"),
            Pair(toNanosPart(), "ns"),
        )

        return parts.filter { it.first > 0 }.map { it.first.toString() + it.second }.joinToString(" ")
    }
}