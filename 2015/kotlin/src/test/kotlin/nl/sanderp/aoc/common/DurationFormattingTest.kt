package nl.sanderp.aoc.common

import org.amshove.kluent.`should be equal to`
import java.time.Duration
import kotlin.test.Test

class DurationFormattingTest {
    private val second = 1000L
    private val minute = 60 * second
    private val hour = 60 * minute

    @Test
    fun `should correctly calculate the value of each time unit`() {
        val duration = Duration.ofMillis(2 * hour + 42 * minute + 24 * second + 123).plusNanos(456)

        duration.prettyPrint() `should be equal to` "2h 42m 24s 123ms 456ns"
    }

    @Test
    fun `pretty-printing a duration should skip time units that are zero`() {
        val duration = Duration.ofMillis(2 * hour + 7 * second)

        duration.prettyPrint() `should be equal to` "2h 7s"
    }

    @Test
    fun `pretty-printing a duration should start at the first non-zero time unit`() {
        val duration = Duration.ofMillis(4 * minute + 2 * second)

        duration.prettyPrint() `should be equal to` "4m 2s"
    }
}