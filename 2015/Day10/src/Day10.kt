const val input = "1113222113"

fun lookAndSay(digits: String) = buildString {
    var current = digits.first()
    var count = 1

    for (digit in digits.drop(1)) {
        if (current == digit) {
            count++
        } else {
            append("$count$current")
            current = digit
            count = 1
        }
    }

    append("$count$current")
}

fun lookAndSay(digits: String, times: Int): String {
    var result = digits
    for (n in 0 until times) {
        result = lookAndSay(result)
    }
    return result
}

fun main() {
    println("Part one: ${lookAndSay(input, 40).length}")
    println("Part two: ${lookAndSay(input, 50).length}")
}