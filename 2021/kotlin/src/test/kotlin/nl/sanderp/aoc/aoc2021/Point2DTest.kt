package nl.sanderp.aoc.aoc2021

import org.amshove.kluent.`should be equal to`
import org.amshove.kluent.`should contain same`
import kotlin.test.Test

class Point2DTest {
    @Test
    fun `points next to a point`() {
        val point = Point2D(2, 5)

        point.pointsNextTo() `should contain same` listOf(
            1 to 5,
            3 to 5,
            2 to 4,
            2 to 6,
        )
    }

    @Test
    fun `points around a point`() {
        val point = Point2D(0, 0)

        point.pointsAround() `should contain same` listOf(
            -1 to -1,
            0 to -1,
            1 to -1,
            -1 to 0,
            1 to 0,
            -1 to 1,
            0 to 1,
            1 to 1,
        )
    }

    @Test
    fun `rotating a point`() {
        val point = Point2D(3, 4)

        point.rotateDegrees(90) `should be equal to` Point2D(-4, 3)
        point.rotateDegrees(180) `should be equal to` Point2D(-3, -4)
        point.rotateDegrees(270) `should be equal to` Point2D(4, -3)
        point.rotateDegrees(360) `should be equal to` Point2D(3, 4)
    }

    @Test
    fun `distance to point`() {
        Point2D(3, 4).distance() `should be equal to` 5.0
    }

    @Test
    fun `distance between points`() {
        Point2D(1, 1).distanceTo(Point2D(5, 1)) `should be equal to` 4.0
    }

    @Test
    fun `manhattan distance to point`() {
        Point2D(3, 4).manhattan() `should be equal to` 7
    }

    @Test
    fun `manhattan distance between points`() {
        Point2D(2, -3).manhattanTo(Point2D(-2, 8)) `should be equal to` 15
    }
}