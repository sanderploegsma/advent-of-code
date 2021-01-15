package nl.sanderp.aoc

object IO {
    fun readText(name: String) = IO.javaClass.getResource("/$name").readText()
    fun <T> readText(name: String, parse: (String) -> T) = readText(name).let(parse)

    fun readLines(name: String) = readText(name).split(System.lineSeparator())
    fun <T> readLines(name: String, parse: (String) -> T) = readLines(name).map(parse)
}