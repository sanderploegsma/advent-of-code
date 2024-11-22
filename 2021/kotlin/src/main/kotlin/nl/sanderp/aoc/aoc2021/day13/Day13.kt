package nl.sanderp.aoc.aoc2021.day13

import nl.sanderp.aoc.common.*

fun main() {
    val (points, folds) = parse(readResource("Day13.txt"))

    val (answer1, duration1) = measureDuration<Int> { partOne(points, folds) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    println("Part two:")
    val duration2 = measureDuration { partTwo(points, folds) }
    println("took ${duration2.prettyPrint()}")
}

private sealed class Fold
private data class FoldHorizontal(val y: Int) : Fold()
private data class FoldVertical(val x: Int) : Fold()

private fun parse(input: String): Pair<Set<Point2D>, List<Fold>> {
    val points = input.lines().takeWhile { it.isNotBlank() }.map { line ->
        val (x, y) = line.split(',').map { it.toInt() }
        x to y
    }

    val folds = input.lines().drop(points.size + 1).map { line ->
        val (instruction, value) = line.split('=')
        when (instruction) {
            "fold along x" -> FoldVertical(value.toInt())
            "fold along y" -> FoldHorizontal(value.toInt())
            else -> throw IllegalArgumentException("Cannot parse fold instruction '$line'")
        }
    }

    return points.toSet() to folds
}

private fun fold(points: Set<Point2D>, f: Fold) = when (f) {
    is FoldHorizontal -> points.map { p -> if (p.y < f.y) p else p.x to (p.y - 2 * (p.y - f.y)) }.toSet()
    is FoldVertical -> points.map { p -> if (p.x < f.x) p else (p.x - 2 * (p.x - f.x)) to p.y }.toSet()
}

private fun partOne(points: Set<Point2D>, folds: List<Fold>): Int {
    val result = fold(points, folds.first())
    return result.size
}

private fun partTwo(points: Set<Point2D>, folds: List<Fold>) {
    val result = folds.fold(points, ::fold)
    result.print()
}
