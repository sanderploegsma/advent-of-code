import java.io.File

const val width = 25
const val height = 6

fun main() {
    val input = File("input.txt").useLines { it.toList()[0] }.map { it.toString().toInt() }
    val image = input.chunked(width * height).map { it.chunked(width) }

    // Part 1
    val leastZeroes = image.minBy { layer -> layer.flatten().count { it == 0 } }!!
    val ones = leastZeroes.flatten().filter { it == 1 }.count()
    val twos = leastZeroes.flatten().filter { it == 2 }.count()
    println(ones * twos)

    // Part 2
    val result = image.reduce { img, layer ->
        img.flatten().zip(layer.flatten()).map { (a, b) -> if (a < 2) a else b }.chunked(width)
    }
    result.forEach { println(it) }
}
