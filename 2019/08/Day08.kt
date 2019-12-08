import java.io.File

const val width = 25
const val height = 6

fun main() {
    val input = File("input.txt").useLines { it.toList()[0] }.map { it.toString().toInt() }
    val layers = input.chunked(width * height)

    // Part 1
    val leastZeroes = layers.minBy { layer -> layer.count { it == 0 } }!!
    val ones = leastZeroes.count { it == 1 }
    val twos = leastZeroes.count { it == 2 }
    println(ones * twos)

    // Part 2
    val image = layers.reduce { img, layer ->
        img.zip(layer).map { (a, b) -> if (a < 2) a else b }
    }
    image.chunked(width).forEach { println(it) }
}
