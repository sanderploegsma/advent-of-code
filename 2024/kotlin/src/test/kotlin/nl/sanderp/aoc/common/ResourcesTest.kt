package nl.sanderp.aoc.common

import org.amshove.kluent.AnyException
import org.amshove.kluent.`should be equal to`
import org.amshove.kluent.`should throw`
import org.amshove.kluent.invoking
import kotlin.test.Test

class ResourcesTest {
    @Test
    fun `reading a non-existing resource should fail`() {
        invoking { readResource("UnknownFile.txt") } `should throw` AnyException
    }

    @Test
    fun `reading a single-line resource`() {
        val contents = readResource("InputSingleLine.txt")

        contents `should be equal to` "apples bananas coconuts"
    }

    @Test
    fun `reading a multi-line resource`() {
        val contents = readResource("InputWithLineBreaks.txt")

        contents.lines() `should be equal to` listOf("apples", "bananas", "coconuts")
    }
}
