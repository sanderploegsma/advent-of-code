package nl.sanderp.aoc.aoc2015.day13

import nl.sanderp.aoc.common.permutations

val preferences = mapOf(
    "Alice" to mapOf(
        "Bob" to 54,
        "Carol" to -81,
        "David" to -42,
        "Eric" to 89,
        "Frank" to -89,
        "George" to 97,
        "Mallory" to -94,
    ),
    "Bob" to mapOf(
        "Alice" to 3,
        "Carol" to -70,
        "David" to -31,
        "Eric" to 72,
        "Frank" to -25,
        "George" to -95,
        "Mallory" to 11,
    ),
    "Carol" to mapOf(
        "Alice" to -83,
        "Bob" to 8,
        "David" to 35,
        "Eric" to 10,
        "Frank" to 61,
        "George" to 10,
        "Mallory" to 29,
    ),
    "David" to mapOf(
        "Alice" to 67,
        "Bob" to 25,
        "Carol" to 48,
        "Eric" to -65,
        "Frank" to 8,
        "George" to 84,
        "Mallory" to 9,
    ),
    "Eric" to mapOf(
        "Alice" to -51,
        "Bob" to -39,
        "Carol" to 84,
        "David" to -98,
        "Frank" to -20,
        "George" to -6,
        "Mallory" to 60
    ),
    "Frank" to mapOf(
        "Alice" to 51,
        "Bob" to 79,
        "Carol" to 88,
        "David" to 33,
        "Eric" to 43,
        "George" to 77,
        "Mallory" to -3
    ),
    "George" to mapOf(
        "Alice" to -14,
        "Bob" to -12,
        "Carol" to -52,
        "David" to 14,
        "Eric" to -62,
        "Frank" to -18,
        "Mallory" to -17,
    ),
    "Mallory" to mapOf(
        "Alice" to -36,
        "Bob" to 76,
        "Carol" to -34,
        "David" to 37,
        "Eric" to 40,
        "Frank" to 18,
        "George" to 7,
    )
)

fun happiness(name: String, nextTo: String) = preferences[name]?.get(nextTo) ?: 0

fun happiness(seating: List<String>) =
    seating
        .withIndex()
        .sumOf { (index, name) ->
            val left = if (index == 0) seating.last() else seating[index - 1]
            val right = if (index == seating.lastIndex) seating.first() else seating[index + 1]

            happiness(name, left) + happiness(name, right)
        }

fun main() {
    println("Part one: ${preferences.keys.toList().permutations().maxOf { happiness(it) }}")
    println("Part two: ${preferences.keys.toList().plus("Sander").permutations().maxOf { happiness(it) }}")
}
