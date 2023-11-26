package nl.sanderp.aoc.aoc2015.day12

import nl.sanderp.aoc.common.readResource

fun findNumbers(input: String) = Regex("""[0-9\-]+""").findAll(input).map { it.value.toInt() }

fun main() {
    val input = readResource("Day12.txt")
    println("Part one: ${findNumbers(input).sum()}")
}
