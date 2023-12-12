package nl.sanderp.aoc.aoc2015.day15

import kotlin.math.max

data class Properties(val capacity: Int, val durability: Int, val flavor: Int, val texture: Int, val calories: Int) {
    operator fun plus(other: Properties): Properties = Properties(
        capacity + other.capacity,
        durability + other.durability,
        flavor + other.flavor,
        texture + other.texture,
        calories + other.calories,
    )

    operator fun times(factor: Int): Properties = Properties(
        capacity * factor,
        durability * factor,
        flavor * factor,
        texture * factor,
        calories * factor,
    )

    val score = max(0, capacity) * max(0, durability) * max(0, flavor) * max(0, texture)
}

val ingredients = listOf(
    Properties(3, 0, 0, -3, 2),
    Properties(-3, 3, 0, 0, 9),
    Properties(-1, 0, 4, 0, 1),
    Properties(0, 0, -2, 2, 8),
)

fun amounts() = sequence {
    for (a in 0..100) {
        for (b in 0..100 - a) {
            for (c in 0..100 - a - b) {
                yield(listOf(a, b, c, 100 - a - b - c))
            }
        }
    }
}

fun main() {
    val recipes = amounts()
        .map { it.mapIndexed { index, factor -> ingredients[index] * factor } }
        .map { it.reduce(Properties::plus) }
        .toList()

    println("Part one: ${recipes.maxOf { it.score }}")
    println("Part two: ${recipes.filter { it.calories == 500 }.maxOf { it.score }}")
}
