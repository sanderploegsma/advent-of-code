package nl.sanderp.aoc.common

import org.amshove.kluent.`should be in range`
import org.amshove.kluent.`should be`
import kotlin.test.Test

class DurationMeasurementTest {
    @Test
    fun `measurement with return type returns result of given block`() {
        val (result, _) = measureDuration<Int> { 42 }

        result `should be` 42
    }

    @Test
    fun `measurement with return type measures execution duration`() {
        val (_, duration) = measureDuration<Int> {
            Thread.sleep(1000)
            42
        }

        duration.toMillis() `should be in range` 1000L..2000L
    }

    @Test
    fun `measurement without return type measures execution duration`() {
        val duration = measureDuration { Thread.sleep(1000) }

        duration.toMillis() `should be in range` 1000L..2000L
    }
}