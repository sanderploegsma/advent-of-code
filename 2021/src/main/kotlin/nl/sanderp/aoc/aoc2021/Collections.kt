package nl.sanderp.aoc.aoc2021

import java.util.*

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
 * Generates all combinations of a list of size m.
 * @see <a href="https://rosettacode.org/wiki/Combinations#Kotlin">Source on RosettaCode</a>
 */
inline fun <reified T> List<T>.combinations(m: Int): Sequence<List<T>> {
    val items = this

    return sequence {
        val result = MutableList(m) { items[0] }
        val stack = LinkedList<Int>()
        stack.push(0)
        while (stack.isNotEmpty()) {
            var resIndex = stack.size - 1
            var arrIndex = stack.pop()

            while (arrIndex < size) {
                result[resIndex++] = items[arrIndex++]
                stack.push(arrIndex)

                if (resIndex == m) {
                    yield(result.toList())
                    break
                }
            }
        }
    }
}


fun <T, U> allPairs(first: Iterable<T>, second: Iterable<U>) = sequence {
    for (a in first) {
        for (b in second) {
            yield(Pair(a, b))
        }
    }
}

fun <T, U, V> allTriples(first: Iterable<T>, second: Iterable<U>, third: Iterable<V>) = sequence {
    for (a in first) {
        for (b in second) {
            for (c in third) {
                yield(Triple(a, b, c))
            }
        }
    }
}

fun <T> Iterable<T>.allPairs() = allPairs(this, this)
fun <T> Iterable<T>.allTriples() = allTriples(this, this, this)