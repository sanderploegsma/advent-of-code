fun readText(name: String) = {}.javaClass.getResource(name).readText()
fun <T> readText(name: String, parse: (String) -> T) = readText(name).let(parse)

fun readLines(name: String) = readText(name).split(System.lineSeparator())
fun <T> readLines(name: String, parse: (String) -> T) = readLines(name).map(parse)
