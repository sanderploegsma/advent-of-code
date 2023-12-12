package nl.sanderp.aoc.aoc2015.day14

import nl.sanderp.aoc.common.transpose

data class Stats(val speed: Int, val flyTime: Int, val restTime: Int)

val input = mapOf(
    "Vixen" to Stats(19, 7, 124),
    "Rudolph" to Stats(3, 15, 28),
    "Donner" to Stats(19, 9, 164),
    "Blitzen" to Stats(19, 9, 158),
    "Comet" to Stats(13, 7, 82),
    "Cupid" to Stats(25, 6, 145),
    "Dasher" to Stats(14, 3, 38),
    "Dancer" to Stats(3, 16, 37),
    "Prancer" to Stats(25, 6, 143),
)

fun generate(stats: Stats) = sequence {
    var distance = 0L
    while (true) {
        repeat(stats.flyTime) {
            distance += stats.speed
            yield(distance)
        }
        repeat(stats.restTime) {
            yield(distance)
        }
    }
}

fun main() {
    val limit = 2503
    val trajectories = input.mapValues { generate(it.value).take(limit).toList() }

    println("Part one: ${trajectories.values.maxOf { it.last() }}")

    val scores = Array(limit) { t ->
        val distances = trajectories.values.map { it[t] }
        val max = distances.max()
        distances.map { if (it == max) 1 else 0 }.toTypedArray()
    }.transpose()

    println("Part two: ${scores.maxOf { it.sum() }}")
}
