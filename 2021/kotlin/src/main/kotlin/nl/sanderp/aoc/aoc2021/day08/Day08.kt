package nl.sanderp.aoc.aoc2021.day08

import nl.sanderp.aoc.common.measureDuration
import nl.sanderp.aoc.common.prettyPrint
import nl.sanderp.aoc.common.readResource

data class Entry(val pattern: List<String>, val values: List<String>)

fun main() {
    val input = readResource("Day08.txt").lines().map { line ->
        val (patterns, values) = line.split(" | ").map { it.split(" ") }
        Entry(patterns, values)
    }

    val (answer1, duration1) = measureDuration<Int> { partOne(input) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> { partTwo(input) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private fun solve(entry: Entry): Int {
    val mapping = mutableMapOf(
        1 to entry.pattern.first { it.length == 2 },
        4 to entry.pattern.first { it.length == 4 },
        7 to entry.pattern.first { it.length == 3 },
        8 to entry.pattern.first { it.length == 7 },
    )

    mapping[9] = entry.pattern.first { it.length == 6 && it.containsCharsOf(mapping[4]!!) }
    mapping[6] = entry.pattern.first { it.length == 6 && !it.containsCharsOf(mapping[1]!!) }
    mapping[0] = entry.pattern.first { it.length == 6 && !mapping.containsValue(it) }
    mapping[3] = entry.pattern.first { it.length == 5 && it.containsCharsOf(mapping[1]!!) }
    mapping[5] = entry.pattern.first { it.length == 5 && (it + mapping[1]!!).hasSameCharsAs(mapping[9]!!) }
    mapping[2] = entry.pattern.first { !mapping.containsValue(it) }

    return entry.values.map { x -> mapping.filterValues { it.hasSameCharsAs(x) }.keys.first() }.joinToString("").toInt()
}

private fun partOne(entries: List<Entry>) = entries.sumOf { entry ->
    entry.values.count { it.length == 2 || it.length == 3 || it.length == 4 || it.length == 7 }
}

private fun partTwo(entries: List<Entry>) = entries.sumOf { solve(it) }

private fun String.hasSameCharsAs(other: String) = this.containsCharsOf(other) && other.containsCharsOf(this)
private fun String.containsCharsOf(other: String) = other.all { this.contains(it) }
