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

val firstRules = mapOf<String, (Int) -> Boolean>(
    "children" to { it == 3 },
    "cats" to { it == 7 },
    "samoyeds" to { it == 2 },
    "pomeranians" to { it == 3 },
    "akitas" to { it == 0 },
    "vizslas" to { it == 0 },
    "goldfish" to { it == 5 },
    "trees" to { it == 3 },
    "cars" to { it == 2 },
    "perfumes" to { it == 1 },
)

val secondRules = firstRules + mapOf(
    "cats" to { it > 7 },
    "pomeranians" to { it < 3 },
    "goldfish" to { it < 5 },
    "trees" to { it > 3 },
)

private fun Int?.check(predicate: (Int) -> Boolean) = this?.let(predicate) ?: true

fun main() {
    val input = readResource("16.txt").lines().map(::parseLine)

    val firstMatch = input.withIndex().single { (_, facts) ->
        firstRules.all { (key, predicate) -> facts[key].check(predicate) }
    }

    println("Part one: ${firstMatch.index + 1}")

    val secondMatch = input.withIndex().single { (_, facts) ->
        secondRules.all { (key, predicate) -> facts[key].check(predicate) }
    }

    println("Part two: ${secondMatch.index + 1}")
}
