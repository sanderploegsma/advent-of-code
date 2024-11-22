package nl.sanderp.aoc.aoc2021.day10

import nl.sanderp.aoc.common.measureDuration
import nl.sanderp.aoc.common.prettyPrint
import nl.sanderp.aoc.common.readResource
import java.util.*

fun main() {
    val input = readResource("Day10.txt").lines().map { parse(it) }

    val (answer1, duration1) = measureDuration<Long> { partOne(input) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Long> { partTwo(input) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private val brackets = mapOf(
    ')' to '(',
    ']' to '[',
    '}' to '{',
    '>' to '<',
)

private val corruptLineScores = mapOf(
    ')' to 3L,
    ']' to 57L,
    '}' to 1197L,
    '>' to 25137L,
)

private val incompleteLineScores = mapOf(
    ')' to 1,
    ']' to 2,
    '}' to 3,
    '>' to 4,
)

sealed class Line(val score: Long)

class CorruptLine(val char: Char) : Line(corruptLineScores[char]!!) {
    override fun toString(): String {
        return "Incorrect token '$char' (score: $score)"
    }
}

class IncompleteLine(val missing: List<Char>): Line(missing.fold(0) { s, c -> 5 * s + incompleteLineScores[c]!!}) {
    override fun toString(): String {
        return "Incomplete line, missing '${missing.joinToString("")}' (score: $score)"
    }
}

private fun parse(line: String): Line {
    val open = Stack<Char>()
    for (c in line) {
        if (brackets.containsValue(c)) {
            open.push(c)
        } else if (open.empty() || open.peek() != brackets[c]) {
            return CorruptLine(c)
        } else {
            open.pop()
        }
    }

    val missing = buildList {
        while (open.isNotEmpty()) {
            val opening = open.pop()
            add(brackets.filterValues { it == opening }.keys.first())
        }
    }

    return IncompleteLine(missing)
}

private fun partOne(input: List<Line>) = input.filterIsInstance(CorruptLine::class.java).sumOf { it.score }

private fun partTwo(input: List<Line>): Long {
    val scores = input.filterIsInstance(IncompleteLine::class.java).map { it.score }
    return scores.sorted()[scores.size / 2]
}
