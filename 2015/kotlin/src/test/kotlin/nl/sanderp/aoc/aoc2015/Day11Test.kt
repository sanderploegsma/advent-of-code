package nl.sanderp.aoc.aoc2015

import nl.sanderp.aoc.aoc2015.day11.increment
import nl.sanderp.aoc.aoc2015.day11.isValid
import org.amshove.kluent.`should be equal to`
import org.junit.jupiter.api.DynamicTest
import org.junit.jupiter.api.TestFactory

class Day11Test {
    @TestFactory
    fun `password validation`() = mapOf(
        "hijklmmn" to false,
        "abbceffg" to false,
        "abbcegjk" to false,
        "abcdffaa" to true,
        "ghjaabcc" to true
    ).map {
        DynamicTest.dynamicTest("Password ${it.key}") { it.key.isValid `should be equal to` it.value }
    }

    @TestFactory
    fun `increment password`() = mapOf("a" to "b", "aa" to "ab", "az" to "ba").map {
        DynamicTest.dynamicTest("${it.key} -> ${it.value}") { increment(it.key) `should be equal to` it.value }
    }
}
