package nl.sanderp.aoc.aoc2021.day20

import nl.sanderp.aoc.common.Point2D
import nl.sanderp.aoc.common.readResource
import nl.sanderp.aoc.common.x
import nl.sanderp.aoc.common.y

fun main() {
    val input = readResource("Day20.txt").replace('.', '0').replace('#', '1').lines()
    val algorithm = input.first()
    val image = parseImage(input.drop(2))

    val enhanced1 = image.enhance(algorithm, 2)
    println("Part one: ${enhanced1.pixels.count { it.value == '1' }}")

    val enhanced2 = image.enhance(algorithm, 50)
    println("Part two: ${enhanced2.pixels.count { it.value == '1' }}")
}

private data class Image(val pixels: Map<Point2D, Char>, val default: Char) {
    val minX = pixels.minOf { it.key.x }
    val minY = pixels.minOf { it.key.y }
    val maxX = pixels.maxOf { it.key.x }
    val maxY = pixels.maxOf { it.key.y }
}

private fun parseImage(image: List<String>): Image {
    val pixels = buildMap {
        for ((y, row) in image.withIndex()) {
            for ((x, c) in row.withIndex()) {
                put(x to y, c)
            }
        }
    }

    return Image(pixels, '0')
}

private fun Image.enhance(algorithm: String, times: Int) =
    generateSequence(this) { it.enhance(algorithm) }.drop(times).first()

private fun Image.enhance(algorithm: String): Image {
    val nextPixels = mutableMapOf<Point2D, Char>()
    for (nextY in minY - 1..maxY + 1) {
        for (nextX in minX - 1..maxX + 1) {
            var bits = ""

            for (y in nextY - 1..nextY + 1) {
                for (x in nextX - 1..nextX + 1) {
                    bits += pixels[Point2D(x, y)] ?: default
                }
            }

            nextPixels[nextX to nextY] = algorithm[bits.toInt(2)]
        }
    }

    return Image(nextPixels, default = algorithm[default.toString().repeat(9).toInt(2)])
}
