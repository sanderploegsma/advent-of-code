const val input = "1113222113"

fun lookAndSay(digits: List<Int>): List<Int> {
    val out = mutableListOf<Int>()
    var current = digits.first()
    var count = 1

    for (digit in digits.drop(1)) {
        if (current == digit) {
            count++
        } else {
            out += count
            out += current
            current = digit
            count = 1
        }
    }

    out += count
    out += current

    return out
}

fun lookAndSay(digits: List<Int>, times: Int): List<Int> {
    var result = digits
    for (n in 0 until times) {
        result = lookAndSay(result)
    }
    return result
}

fun main() {
    val digits = input.map { it.toString().toInt() }
    println("Part one: ${lookAndSay(digits, 40).size}")
    println("Part two: ${lookAndSay(digits, 50).size}")
}