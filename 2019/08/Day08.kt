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
    val result = image.reduce { img, layer -> overlay(img, layer) }
    result.forEach { println(it) }
}

fun overlay(a: List<List<Int>>, b: List<List<Int>>): List<List<Int>> {
    val result = mutableListOf<List<Int>>()
    for (i in 0 until height) {
        val row = mutableListOf<Int>()
        for (j in 0 until width) {
            val res = if (a[i][j] < 2) a[i][j] else b[i][j]
            row.add(res)
        }
        result.add(row)
    }
    return result
}
