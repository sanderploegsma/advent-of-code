package nl.sanderp.aoc.common

import org.amshove.kluent.`should be equal to`
import kotlin.test.Test

class NavigationTest {
    @Test
    fun `navigating multiple steps`() {
        val finish = Point2D(0, 0)
            .move(Direction.North, steps = 5)
            .move(Direction.West, steps = 2)
            .move(Direction.South)
            .move(Direction.East, steps = 4)

        finish `should be equal to` Point2D(2, 4)
    }
}
