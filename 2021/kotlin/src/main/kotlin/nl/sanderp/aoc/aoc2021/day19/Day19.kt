package nl.sanderp.aoc.aoc2021.day19

import nl.sanderp.aoc.common.*
import java.util.*

fun main() {
    val input = readResource("Day19.txt")
        .split("\n\n")
        .map { parse(it) }
        .mapIndexed { i, b -> Scanner(i, Point3D(0, 0, 0), b) }

    val scanners = locateScanners(input)
    val beacons = scanners.flatMap { it.beaconsAbsolute }.toSet()
    println("Part one: ${beacons.size}")

    val distances = scanners.map { it.center }.allPairs().map { (s1, s2) -> s1.manhattanTo(s2) }
    println("Part two: ${distances.maxOf { it }}")
}

private val orientations2 = listOf<Point3D.() -> Point3D>(
    { Point3D(+x, +y, +z) },
    { Point3D(+x, +z, -y) },
    { Point3D(+x, -y, -z) },
    { Point3D(+x, -z, +y) },
    { Point3D(+y, +x, -z) },
    { Point3D(+y, +z, +x) },
    { Point3D(+y, -x, +z) },
    { Point3D(+y, -z, -x) },
    { Point3D(+z, +x, +y) },
    { Point3D(+z, +y, -x) },
    { Point3D(+z, -x, -y) },
    { Point3D(+z, -y, +x) },
    { Point3D(-x, +y, -z) },
    { Point3D(-x, +z, +y) },
    { Point3D(-x, -y, +z) },
    { Point3D(-x, -z, -y) },
    { Point3D(-y, +x, +z) },
    { Point3D(-y, +z, -x) },
    { Point3D(-y, -x, -z) },
    { Point3D(-y, -z, +x) },
    { Point3D(-z, +x, -y) },
    { Point3D(-z, +y, +x) },
    { Point3D(-z, -x, +y) },
    { Point3D(-z, -y, -x) },
)

private data class Scanner(val id: Int, val center: Point3D, val beacons: List<Point3D>) {
    val beaconsAbsolute = beacons.map { it + center }

    val orientations by lazy {
        orientations2.map { t -> this.copy(beacons = beacons.map(t)) }
    }
}

private fun parse(block: String) = block.lines().drop(1).map { line ->
    val (x, y, z) = line.split(',').map { it.toInt() }
    Point3D(x, y, z)
}

private fun locateScanners(input: List<Scanner>) = buildList {
    val queue = LinkedList<Scanner>()
    val pending = input.drop(1).toMutableList()

    add(input.first())
    queue.push(input.first())

    while (queue.isNotEmpty()) {
        val located = queue.poll()
        val matches = pending.mapNotNull { matchScanners(located, it) }

        for (match in matches) {
            print("#")
            add(match)
            queue.push(match)
            pending.removeIf { it.id == match.id }
        }
    }

    println()

    if (pending.isNotEmpty()) {
        throw Exception("Unable to locate scanners: ${pending.joinToString { it.id.toString() }}")
    }
}

private fun matchScanners(a: Scanner, b: Scanner): Scanner? {
    for (beaconA in a.beaconsAbsolute) {
        for (orientationB in b.orientations) {
            for (beaconB in orientationB.beaconsAbsolute) {
                val translatedB = orientationB.copy(center = beaconA - beaconB)

                if (a.beaconsAbsolute.intersect(translatedB.beaconsAbsolute.toSet()).size >= 12) {
                    return translatedB
                }
            }
        }
    }

    return null
}
