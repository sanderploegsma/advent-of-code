import java.io.File

fun encode(value: String): String {
    val encoded = value.map {
        when (it) {
            '"' -> "\\\""
            '\\' -> "\\\\"
            else -> "$it"
        }
    }

    return encoded.joinToString(separator = "", prefix = "\"", postfix = "\"")
}

fun decode(value: String): String {
    val trimmed = value
        .replace(Regex("""\\x[0-9a-f]{2}"""), ".")
        .replace(Regex("""\\""""), ".")
        .replace(Regex("""\\\\"""), ".")

    return trimmed.substring(1, trimmed.lastIndex)
}

fun main() {
    val input = File("Day08/Input.txt").readLines()

    val partOne = input.map { it to decode(it) }.sumOf { it.first.length - it.second.length }
    println("Part one: $partOne")

    val partTwo = input.map { it to encode(it) }.sumOf { it.second.length - it.first.length }
    println("Part two: $partTwo")
}