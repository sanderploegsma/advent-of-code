package nl.sanderp.aoc.common

import org.amshove.kluent.`should contain all`
import org.amshove.kluent.`should contain`
import kotlin.test.Test

class ListPermutationsTest {
    @Test
    fun `all permutations of a small list`() {
        val actual = listOf(1, 2, 3).permutations()

        actual `should contain all` listOf(
            listOf(1, 2, 3),
            listOf(1, 3, 2),
            listOf(2, 1, 3),
            listOf(2, 3, 1),
            listOf(3, 1, 2),
            listOf(3, 2, 1)
        )
    }

    @Test
    fun `random permutation of a big list`() {
        val items = (1 until 10).toList()
        val actual = items.permutations()

        actual `should contain` items.shuffled()
    }
}
