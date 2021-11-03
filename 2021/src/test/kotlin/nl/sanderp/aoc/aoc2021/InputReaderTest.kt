package nl.sanderp.aoc.aoc2021

import kotlin.test.Test
import kotlin.test.assertContentEquals
import kotlin.test.assertEquals
import kotlin.test.assertFails

class InputReaderTest {
    @Test
    fun `it should return null when file does not exist`() {
        assertFails { InputReader.read("UnknownFile.txt") }
    }

    @Test
    fun `it should return full contents of single line input`() {
        val contents = InputReader.read("InputSingleLine.txt")

        assertEquals("apples bananas coconuts", contents)
    }

    @Test
    fun `it should return full contents of multiline input`() {
        val contents = InputReader.read("InputWithLineBreaks.txt")

        assertContentEquals(listOf("apples", "bananas", "coconuts"), contents.lines())
    }
}