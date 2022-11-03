package nl.sanderp.aoc.aoc2021.day12

import nl.sanderp.aoc.common.measureDuration
import nl.sanderp.aoc.common.prettyPrint
import nl.sanderp.aoc.common.readResource

fun main() {
    val input = readResource("Day12.txt").lines().map { parse(it) }

    val (answer1, duration1) = measureDuration<Int> { partOne(input) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> { partTwo(input) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private fun parse(line: String): Pair<String, String> {
    val (from, to) = line.split('-')
    return from to to
}

private fun List<Pair<String, String>>.traverse(
    path: List<String>,
    isNextCaveValid: (id: String, path: List<String>) -> Boolean
): Set<List<String>> {
    val cave = path.last()

    return if (cave == "end") setOf(path) else getNextCaves(cave)
        .filter { isNextCaveValid(it, path) }
        .flatMap { traverse(path + it, isNextCaveValid) }
        .toSet()
}

private fun partOne(connections: List<Pair<String, String>>): Int {
    val paths = connections.traverse(listOf("start")) { cave, path ->
        cave.isLarge || !path.contains(cave)
    }

    return paths.size
}

private fun partTwo(connections: List<Pair<String, String>>): Int {
    val paths = connections.traverse(listOf("start")) { cave, path ->
        cave.isLarge || !path.contains(cave) || path.containsNoSmallCaveTwice
    }

    return paths.size
}

private val List<String>.containsNoSmallCaveTwice get() =
    groupingBy { it }.eachCount().filter { it.key.isSmall }.all { it.value <= 1 }

private fun List<Pair<String, String>>.getNextCaves(id: String) =
    (filter { it.first == id }.map { it.second } + filter { it.second == id }.map { it.first }).filter { it != "start" }

private val String.isLarge get() = this == this.uppercase()
private val String.isSmall get() = this == this.lowercase()