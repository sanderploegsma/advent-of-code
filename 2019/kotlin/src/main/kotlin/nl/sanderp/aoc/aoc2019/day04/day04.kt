package nl.sanderp.aoc.aoc2019.day04

fun hasLength6(password: Iterable<Int>) = password.count() == 6
fun isAscending(password: Iterable<Int>) = password.windowed(2).all { (a, b) -> a <= b }
fun hasRepeatDigit(password: Iterable<Int>) = password.windowed(2).any { (a, b) -> a == b }
fun hasRepeatDigit2(password: Iterable<Int>) = password.groupBy { it }.any { it.value.size == 2 }

fun isValid(password: Iterable<Int>) = hasLength6(password) and isAscending(password) and hasRepeatDigit(password)
fun isValid2(password: Iterable<Int>) = hasLength6(password) and isAscending(password) and hasRepeatDigit2(password)
fun toDigits(password: Int) = password.toString().toCharArray().map { it.toString().toInt() }

fun main() {
    val input = (254032..789860).map(::toDigits)
    println("Part one: ${input.count(::isValid)}")
    println("Part two: ${input.count(::isValid2)}")
}
