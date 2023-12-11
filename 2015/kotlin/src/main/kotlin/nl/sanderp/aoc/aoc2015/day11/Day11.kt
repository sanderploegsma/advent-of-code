package nl.sanderp.aoc.aoc2015.day11

import nl.sanderp.aoc.common.allPairs

const val input = "cqjxjnds"

val String.isValid
    get(): Boolean {
        if (windowed(3, 1).all { Pair(it[1] - it[0], it[2] - it[1]) != Pair(1, 1) }) {
            return false
        }

        if (any { listOf('i', 'o', 'l').contains(it) }) {
            return false
        }

        val pairs = withIndex()
            .windowed(2)
            .filter { it.first().value == it.last().value }
            .map { setOf(it.first().index, it.last().index) }

        return pairs.allPairs().any { (a, b) -> a.intersect(b).isEmpty() }
    }

fun increment(password: String): String {
    val chars = password.toCharArray().reversed().toMutableList()
    for (i in chars.indices) {
        if (chars[i] < 'z') {
            chars[i]++
            break
        }
        chars[i] = 'a'
    }
    return chars.reversed().joinToString(separator = "")
}

fun findNextPassword(start: String) = generateSequence(start, ::increment).drop(1).first { it.isValid }

fun main() {
    val next = findNextPassword(input)
    println("Part one: $next")
    println("Part two: ${findNextPassword(next)}")
}