package nl.sanderp.aoc.aoc2021.day22

import nl.sanderp.aoc.aoc2021.*
import kotlin.math.max
import kotlin.math.min

fun main() {
    val input = readResource("Day22.txt").lines().map { parse(it) }

    val (answer1, duration1) = measureDuration<Int> { partOne(input) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Long> { partTwo(input) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private data class Cuboid(val x: IntRange, val y: IntRange, val z: IntRange)
private enum class CuboidState { On, Off }
private data class RebootStep(val state: CuboidState, val cuboid: Cuboid)

private fun parse(line: String): RebootStep {
    val (state, coordinates) = line.split(' ')
    val (x, y, z) = coordinates.split(',').map { c ->
        val (lo, hi) = c.drop(2).split("..").map { it.toInt() }
        lo..hi
    }

    return RebootStep(if (state == "on") CuboidState.On else CuboidState.Off, Cuboid(x, y, z))
}

private fun partOne(steps: List<RebootStep>): Int {
    val roi = -50..50

    val state = steps
        .filter { roi.contains(it.cuboid.x) && roi.contains(it.cuboid.y) && roi.contains(it.cuboid.z) }
        .fold(emptySet<Point3D>()) { cubes, step ->
            val cuboid = step.cuboid.cubes().toSet()
            when (step.state) {
                CuboidState.On -> cubes.union(cuboid)
                CuboidState.Off -> cubes.minus(cuboid)
            }
        }

    return state.size
}

private fun partTwo(steps: List<RebootStep>): Long {
    val cuboids = mutableMapOf<Cuboid, Int>()

    for ((newState, cuboid) in steps) {
        val diff = mutableMapOf<Cuboid, Int>()

        for ((other, count) in cuboids) {
            val (minX, maxX) = (max(other.x.first, cuboid.x.first) to min(other.x.last, cuboid.x.last))
            val (minY, maxY) = (max(other.y.first, cuboid.y.first) to min(other.y.last, cuboid.y.last))
            val (minZ, maxZ) = (max(other.z.first, cuboid.z.first) to min(other.z.last, cuboid.z.last))

            if (maxX >= minX && maxY >= minY && maxZ >= minZ) {
                val overlap = Cuboid(minX..maxX, minY..maxY, minZ..maxZ)
                diff.merge(overlap, -count) { a, b -> a + b }
            }
        }

        if (newState == CuboidState.On) {
            diff.merge(cuboid, 1) { a, b -> a + b }
        }

        for (x in diff) {
            cuboids.merge(x.key, x.value) { a, b -> a + b }
        }
    }

    return cuboids.entries.sumOf { it.key.size() * it.value }
}

private fun IntRange.contains(other: IntRange) = this.contains(other.first) && this.contains(other.last)

private fun IntRange.size() = last - first + 1

private fun Cuboid.cubes() = allTriples(x, y, z)

private fun Cuboid.size() = x.size().toLong() * y.size().toLong() * z.size().toLong()