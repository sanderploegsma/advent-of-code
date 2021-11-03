package nl.sanderp.aoc.aoc2021

import nl.sanderp.aoc.aoc2021.day01.Day01
import kotlin.test.Test
import kotlin.test.assertEquals

class Day01Test {
    @Test
    fun `Part one - sample unit test`() {
        val input = "Hello, World!"
        val solver = Day01(input)

        assertEquals(input, solver.partOne())
    }
}