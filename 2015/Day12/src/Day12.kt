import java.io.File

fun findNumbers(input: String) = Regex("""[0-9\-]+""").findAll(input).map { it.value.toInt() }

fun main() {
    val input = File("Day12/Input.txt").readText()
    println("Part one: ${findNumbers(input).sum()}")
}