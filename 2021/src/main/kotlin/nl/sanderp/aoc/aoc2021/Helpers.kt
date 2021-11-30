package nl.sanderp.aoc.aoc2021

import java.io.IOException
import java.time.Duration

internal object Resources

/**
 * Reads a file from the resources.
 * @param fileName the name of the file, relative to the resources root
 * @return the contents of the resource
 */
fun readResource(fileName: String) = Resources.javaClass.getResource("/$fileName")?.readText()?.trim()
    ?: throw IOException("File does not exist: $fileName")

/**
 * Generates all permutations of a list.
 * @see <a href="https://rosettacode.org/wiki/Permutations#Kotlin">Source on RosettaCode</a>
 */
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

/**
 * Invokes the given block and measures how long it takes to execute.
 */
fun measureDuration(block: () -> Unit): Duration {
    val start = System.nanoTime()
    block()
    val end = System.nanoTime()

    return Duration.ofNanos(end - start)
}

/**
 * Invokes the given block and measures how long it takes to execute. The result of the block is returned together with
 * the execution duration.
 * @param block block with a return value
 * @param T return type of the block
 */
fun <T> measureDuration(block: () -> T): Pair<T, Duration> {
    val start = System.nanoTime()
    val result = block()
    val end = System.nanoTime()

    return Pair(result, Duration.ofNanos(end - start))
}

/**
 * Prints the duration in a human-readable format, like "1h 30m 10s 100ms".
 */
fun Duration.prettyPrint(): String {
    val parts = listOf(
        Pair(toHoursPart(), "h"),
        Pair(toMinutesPart(), "m"),
        Pair(toSecondsPart(), "s"),
        Pair(toMillisPart(), "ms"),
        // toNanosPart returns the number of ns within the second, so for some reason it includes milliseconds too...
        Pair(toNanosPart() % 1000, "ns"),
    )

    return parts.filter { it.first > 0 }.joinToString(" ") { it.first.toString() + it.second }
}