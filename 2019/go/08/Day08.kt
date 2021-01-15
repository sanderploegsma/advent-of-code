import java.io.File

const val width = 25
const val height = 6

fun main() {
    val input = File("input.txt").useLines { it.toList()[0] }.map { it.toString().toInt() }
    val layers = input.chunked(width * height)

    // Part 1
    val leastZeroes = layers.minBy { layer -> layer.count { it == 0 } }!!
    println(leastZeroes.count { it == 1 } * leastZeroes.count { it == 2 })

    // Part 2
    val image = layers
            .reduce { img, layer -> img.zip(layer).map { (a, b) -> if (a < 2) a else b } }
            .map { if (it == 1) '█' else ' ' }
            .chunked(width)
            .map { it.joinToString("") }
            .joinToString("\n")

    println(image)
}
