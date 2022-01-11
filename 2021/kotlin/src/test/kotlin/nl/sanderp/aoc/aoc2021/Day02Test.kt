package nl.sanderp.aoc.aoc2021

import nl.sanderp.aoc.aoc2021.day02.*
import org.amshove.kluent.`should be equal to`
import kotlin.test.Test

class Day02Test {
    private val example = listOf(
        Forward(5),
        Down(5),
        Forward(8),
        Up(3),
        Down(8),
        Forward(2),
    )

    @Test
    fun `example part one`() {
        partOne(example) `should be equal to` 150
    }

    @Test
    fun `example part two`() {
        partTwo(example) `should be equal to` 900L
    }
}