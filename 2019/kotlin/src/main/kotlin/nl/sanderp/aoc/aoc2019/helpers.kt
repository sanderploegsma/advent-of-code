package nl.sanderp.aoc.aoc2019

object IO {
    fun readText(name: String) = IO.javaClass.getResource("/$name").readText()
    fun <T> readText(name: String, parse: (String) -> T) = readText(name).let(parse)

    fun readLines(name: String) = readText(name).split(System.lineSeparator())
    fun <T> readLines(name: String, parse: (String) -> T) = readLines(name).map(parse)
}

fun <T> List<T>.permutations(): List<List<T>> {
    if (this.size == 1) return listOf(this)
    val perms = mutableListOf<List<T>>()
    val toInsert = this[0]
    for (perm in this.drop(1).permutations()) {
        for (i in 0..perm.size) {
            val newPerm = perm.toMutableList()
            newPerm.add(i, toInsert)
            perms.add(newPerm)
        }
    }
    return perms
}
