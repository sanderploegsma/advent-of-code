package nl.sanderp.aoc.aoc2021

import kotlin.math.*

typealias Point2D = Pair<Int, Int>

val Point2D.x: Int
    get() = first

val Point2D.y: Int
    get() = second

operator fun Point2D.plus(other: Point2D) = Point2D(x + other.x, y + other.y)
operator fun Point2D.minus(other: Point2D) = Point2D(x - other.x, y - other.y)
operator fun Point2D.times(factor: Int) = Point2D(x * factor, y * factor)

fun Point2D.distance() = sqrt(x.toDouble().pow(2) + y.toDouble().pow(2))
fun Point2D.distanceTo(other: Point2D) = (this - other).distance()
fun Point2D.manhattan() = abs(x) + abs(y)
fun Point2D.manhattanTo(other: Point2D) = (this - other).manhattan()
fun Point2D.rotateDegrees(degrees: Int) = rotateRadians(degrees.radians())
fun Point2D.rotateRadians(radians: Double) =
    (x * cos(radians) - y * sin(radians)).roundToInt() to (y * cos(radians) + x * sin(radians)).roundToInt()

fun Point2D.pointsNextTo() = listOf(-1 to 0, 1 to 0, 0 to -1, 0 to 1).map { this + it }
fun Point2D.pointsAround() = (-1..1).allPairs().filterNot { it == Pair(0, 0) }.map { this + it }.toList()

fun Iterable<Point2D>.print() {
    for (y in 0..this.maxOf { it.y }) {
        for (x in 0..this.maxOf { it.x }) {
            if (this.contains(x to y)) {
                print('█')
            } else {
                print(' ')
            }
        }
        println()
    }
}

typealias Point3D = Triple<Int, Int, Int>

val Point3D.x: Int
    get() = first

val Point3D.y: Int
    get() = second

val Point3D.z: Int
    get() = third

operator fun Point3D.plus(other: Point3D) = Point3D(x + other.x, y + other.y, z + other.z)
operator fun Point3D.minus(other: Point3D) = Point3D(x - other.x, y - other.y, z - other.z)
operator fun Point3D.times(factor: Int) = Point3D(x * factor, y * factor, z * factor)

fun Point3D.distance() = sqrt(x.toDouble().pow(2) + y.toDouble().pow(2) + z.toDouble().pow(2))
fun Point3D.distanceTo(other: Point3D) = (this - other).distance()
fun Point3D.manhattan() = abs(x) + abs(y) + abs(z)
fun Point3D.manhattanTo(other: Point3D) = (this - other).manhattan()
fun Point3D.pointsAround() = (-1..1).allTriples().filterNot { it == Triple(0, 0, 0) }.map { this + it }.toList()
