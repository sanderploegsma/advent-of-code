package nl.sanderp.aoc.aoc2015.day16

import nl.sanderp.aoc.common.readResource

fun parseLine(line: String): Map<String, Int> {
    val facts = line.split(": ", limit = 2)[1].split(", ")
    return buildMap {
        for (fact in facts) {
            val (key, value) = fact.split(": ")
            put(key, value.toInt())
        }
    }
}

fun main() {
    val input = readResource("16.txt").lines().map(::parseLine)

    val firstMatch = input.withIndex().single { (_, facts) ->
        facts["children"]?.let { it == 3 } ?: true &&
                facts["cats"]?.let { it == 7 } ?: true &&
                facts["samoyeds"]?.let { it == 2 } ?: true &&
                facts["pomeranians"]?.let { it == 3 } ?: true &&
                facts["akitas"]?.let { it == 0 } ?: true &&
                facts["vizslas"]?.let { it == 0 } ?: true &&
                facts["goldfish"]?.let { it == 5 } ?: true &&
                facts["trees"]?.let { it == 3 } ?: true &&
                facts["cars"]?.let { it == 2 } ?: true &&
                facts["perfumes"]?.let { it == 1 } ?: true
    }

    println("Part one: ${firstMatch.index + 1}")

    val secondMatch = input.withIndex().single { (_, facts) ->
        facts["children"]?.let { it == 3 } ?: true &&
                facts["cats"]?.let { it > 7 } ?: true &&
                facts["samoyeds"]?.let { it == 2 } ?: true &&
                facts["pomeranians"]?.let { it < 3 } ?: true &&
                facts["akitas"]?.let { it == 0 } ?: true &&
                facts["vizslas"]?.let { it == 0 } ?: true &&
                facts["goldfish"]?.let { it < 5 } ?: true &&
                facts["trees"]?.let { it > 3 } ?: true &&
                facts["cars"]?.let { it == 2 } ?: true &&
                facts["perfumes"]?.let { it == 1 } ?: true
    }

    println("Part one: ${secondMatch.index + 1}")
}