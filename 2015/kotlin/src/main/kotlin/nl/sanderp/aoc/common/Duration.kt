package nl.sanderp.aoc.common

import java.time.Duration

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
