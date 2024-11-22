package nl.sanderp.aoc.aoc2021.day23

import nl.sanderp.aoc.common.Point2D
import nl.sanderp.aoc.common.measureDuration
import nl.sanderp.aoc.common.prettyPrint

fun main() {
    val input = listOf(
        Amphipod.Copper to Point2D(-3, -1),
        Amphipod.Bronze to Point2D(-3, -2),
        Amphipod.Bronze to Point2D(-1, -1),
        Amphipod.Copper to Point2D(-1, -2),
        Amphipod.Desert to Point2D(1, -1),
        Amphipod.Amber to Point2D(1, -2),
        Amphipod.Desert to Point2D(3, -1),
        Amphipod.Amber to Point2D(3, -2),
    )

    val (answer1, duration1) = measureDuration<Int> { TODO() }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> { TODO() }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

enum class Amphipod { Amber, Bronze, Copper, Desert }
