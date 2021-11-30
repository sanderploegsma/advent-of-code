package nl.sanderp.aoc.aoc2021

import nl.sanderp.aoc.aoc2021.day01.Day01
import org.amshove.kluent.`should be equal to`
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.ValueSource

class Day01Test {
    @ParameterizedTest
    @ValueSource(strings = ["Hello, World!", "Advent of Code"])
    fun `Sample unit test with different examples`(example: String) {
        val solver = Day01(example)

        solver.partOne() `should be equal to` example
    }
}