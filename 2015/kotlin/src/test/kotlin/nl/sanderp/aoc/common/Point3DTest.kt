package nl.sanderp.aoc.common

import org.amshove.kluent.`should be equal to`
import org.amshove.kluent.`should contain same`
import kotlin.math.sqrt
import kotlin.test.Test

class Point3DTest {
    @Test
    fun `points around a point`() {
        val point = Point3D(0, 0, 0)

        point.pointsAround() `should contain same` listOf(
            Point3D(-1, -1, -1),
            Point3D(-1, -1, 0),
            Point3D(-1, -1, 1),
            Point3D(-1, 0, -1),
            Point3D(-1, 0, 0),
            Point3D(-1, 0, 1),
            Point3D(-1, 1, -1),
            Point3D(-1, 1, 0),
            Point3D(-1, 1, 1),

            Point3D(0, -1, -1),
            Point3D(0, -1, 0),
            Point3D(0, -1, 1),
            Point3D(0, 0, -1),
            Point3D(0, 0, 1),
            Point3D(0, 1, -1),
            Point3D(0, 1, 0),
            Point3D(0, 1, 1),

            Point3D(1, -1, -1),
            Point3D(1, -1, 0),
            Point3D(1, -1, 1),
            Point3D(1, 0, -1),
            Point3D(1, 0, 0),
            Point3D(1, 0, 1),
            Point3D(1, 1, -1),
            Point3D(1, 1, 0),
            Point3D(1, 1, 1),
        )
    }

    @Test
    fun `distance to point`() {
        Point3D(1, 2, 3).distance() `should be equal to` sqrt(14.0)
    }

    @Test
    fun `distance between points`() {
        Point3D(2, 2, 0).distanceTo(Point3D(-2, -2, 0)) `should be equal to` sqrt(32.0)
    }

    @Test
    fun `manhattan distance to point`() {
        Point3D(1, 1, 1).manhattan() `should be equal to` 3
    }

    @Test
    fun `manhattan distance between points`() {
        Point3D(1, 1, 1).manhattanTo(Point3D(-1, -1, -1)) `should be equal to` 6
    }
}