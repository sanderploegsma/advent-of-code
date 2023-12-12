package nl.sanderp.aoc.aoc2015.day15

import kotlin.math.max

fun Array<Array<Int>>.totals(): Array<Int> {
    return Array(this[0].size) { j ->
        this.sumOf { it[j] }
    }
}

operator fun Array<Array<Int>>.times(factors: Array<Int>): Array<Array<Int>> {
    return Array(size) { i ->
        Array(this[i].size) { j ->
            this[i][j] * factors[i]
        }
    }
}

val ingredients = arrayOf(
    arrayOf(3, 0, 0, -3, 2),
    arrayOf(-3, 3, 0, 0, 9),
    arrayOf(-1, 0, 4, 0, 1),
    arrayOf(0, 0, -2, 2, 8),
)

fun score(recipe: Array<Int>) = recipe.dropLast(1).fold(1) { acc, i -> acc * max(0, i) }

fun main() {
    val amounts = sequence {
        for (a in 0..100) {
            for (b in 0..100 - a) {
                for (c in 0..100 - a - b) {
                    yield(arrayOf(a, b, c, 100 - a - b - c))
                }
            }
        }
    }

    val recipes = amounts
        .map { ingredients * it }
        .map { it.totals() }
        .toList()

    println("Part one: ${recipes.maxOf { score(it) }}")
    println("Part two: ${recipes.filter { it.last() == 500 }.maxOf { score(it) }}")
}
