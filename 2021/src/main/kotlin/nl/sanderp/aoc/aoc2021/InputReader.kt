package nl.sanderp.aoc.aoc2021

object InputReader {
    fun read(fileName: String) = InputReader.javaClass.getResource("/$fileName")?.readText()?.trim()
        ?: throw Exception("File does not exist: $fileName")
}