package nl.sanderp.aoc.common

import org.amshove.kluent.`should contain same`
import org.amshove.kluent.shouldContain
import kotlin.random.Random
import kotlin.test.Test

class ListCombinationsTest {
    @Test
    fun `combinations of one should result in the same items`() {
        val items = (1..20).toList()
        val combinations = items.combinations(1).map { it.single() }.toList()

        combinations `should contain same` items
    }

    @Test
    fun `all combinations of a small list`() {
        val combinations = listOf(1, 2, 3).combinations(2).toList()

        combinations `should contain same` listOf(
            listOf(1, 2),
            listOf(1, 3),
            listOf(2, 3),
        )
    }

    @Test
    fun `combining two small lists`() {
        val a = listOf(1, 2, 3)
        val b = listOf(4, 5)

        allPairs(a, b).toList() `should contain same` listOf(
            1 to 4,
            1 to 5,
            2 to 4,
            2 to 5,
            3 to 4,
            3 to 5,
        )
    }

    @Test
    fun `random combination of two large lists`() {
        val odd = 1..99 step 2
        val even = 2..100 step 2
        val randomCombination = odd.elementAt(Random.nextInt(50)) to even.elementAt(Random.nextInt(50))

        allPairs(odd, even) shouldContain randomCombination
    }

    @Test
    fun `combining three small lists`() {
        val a = listOf(1, 2)
        val b = listOf(3, 4)
        val c = listOf(5, 6)

        allTriples(a, b, c).toList() `should contain same` listOf(
            Triple(1, 3, 5),
            Triple(1, 3, 6),
            Triple(1, 4, 5),
            Triple(1, 4, 6),
            Triple(2, 3, 5),
            Triple(2, 3, 6),
            Triple(2, 4, 5),
            Triple(2, 4, 6),
        )
    }

    @Test
    fun `random combination of three large lists`() {
        val ones = 1..97 step 3
        val twos = 2..98 step 3
        val threes = 3..99 step 3
        val randomCombination = Triple(ones.elementAt(Random.nextInt(33)), twos.elementAt(Random.nextInt(33)), threes.elementAt(Random.nextInt(33)))

        allTriples(ones, twos, threes) shouldContain randomCombination
    }
}