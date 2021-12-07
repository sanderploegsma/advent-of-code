package nl.sanderp.aoc.aoc2021

import org.amshove.kluent.`should be equal to`
import org.junit.jupiter.api.DynamicTest
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.TestFactory

class DigitsTest {
    @TestFactory
    fun `Char to digit conversion`() = ('0'..'9').zip(0..9).map { (c, i) ->
        DynamicTest.dynamicTest("Converting '$c' to a digit should give $i") { c.asDigit() `should be equal to` i }
    }

    @Test
    fun `Retrieving digits of a positive number`() {
        123.toDigits() `should be equal to` listOf(1, 2, 3)
    }

    @Test
    fun `Retrieving digits of a negative number`() {
        (-123).toDigits() `should be equal to` listOf(1, 2, 3)
    }
}