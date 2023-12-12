package nl.sanderp.aoc.aoc2015.day18

import nl.sanderp.aoc.common.Point2D
import nl.sanderp.aoc.common.bounds
import nl.sanderp.aoc.common.pointsAround
import nl.sanderp.aoc.common.readResource

fun simulate(grid: Map<Point2D, Int>, alwaysOn: Collection<Point2D> = emptyList()) = buildMap {
    alwaysOn.forEach { point ->
        put(point, 1)
    }

    grid.entries
        .filterNot { alwaysOn.contains(it.key) }
        .forEach { (point, isOn) ->
            val neighborsOn = point.pointsAround().mapNotNull { grid[it] }.sum()
            val newState = when (neighborsOn) {
                3 -> 1
                2 -> isOn
                else -> 0
            }
            put(point, newState)
        }
}

fun main() {
    val grid = buildMap {
        readResource("18.txt").lines().forEachIndexed { y, row ->
            row.forEachIndexed { x, cell ->
                put(Pair(x, y), if (cell == '#') 1 else 0)
            }
        }
    }

    val simulation = generateSequence(grid, ::simulate)
    println("Part one: ${simulation.elementAt(100).values.sum()}")

    val corners = grid.keys.bounds()
    val gridWithCornersOn = grid + corners.associateWith { 1 }
    val simulation2 = generateSequence(gridWithCornersOn) { simulate(it, alwaysOn = corners) }
    println("Part two: ${simulation2.elementAt(100).values.sum()}")
}
