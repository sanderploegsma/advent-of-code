package nl.sanderp.aoc.aoc2015.day17

const val capacity = 150
val input = listOf(50, 44, 11, 49, 42, 46, 18, 32, 26, 40, 21, 7, 18, 43, 10, 47, 36, 24, 22, 40)

fun combinations(options: List<Int> = input.indices.toList(), choices: List<Int> = emptyList()): Set<List<Int>> {
    val currentSum = choices.sumOf { input[it] }
    if (currentSum == capacity) {
        return setOf(choices)
    }

    val candidate = options.firstOrNull { input[it] + currentSum <= capacity }
    if (candidate == null) {
        return emptySet()
    }

    return combinations(options - candidate, choices + candidate) + combinations(options - candidate, choices)
}

fun main() {
    val combinations = combinations()
    println("Part one: ${combinations.size}")

    val minimum = combinations.minOf { it.size }
    println("Part two: ${combinations.filter { it.size == minimum }.size}")
}
