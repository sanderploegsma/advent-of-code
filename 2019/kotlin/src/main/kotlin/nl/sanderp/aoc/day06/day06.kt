package nl.sanderp.aoc.day06

import nl.sanderp.aoc.IO
import java.util.*

fun checksum(data: List<Pair<String, String>>): Int {
    var checksum = 0
    val visited = mutableSetOf<String>()
    val queue = LinkedList<Pair<String, Int>>()
    queue.add("COM" to 0)

    while (queue.isNotEmpty()) {
        val (obj, level) = queue.pop()
        visited.add(obj)
        checksum += level

        data.neighboursOf(obj)
            .filterNot { visited.contains(it) }
            .forEach { queue.add(it to level + 1) }
    }

    return checksum
}

fun orbitalTransfers(data: List<Pair<String, String>>): Int {
    val start = "YOU"
    val end = "SAN"

    val distances = mutableMapOf(start to 0)
    val visited = mutableSetOf<String>()
    val queue = LinkedList<String>()
    queue.add(start)

    while (queue.isNotEmpty()) {
        val obj = queue.pop()
        visited.add(obj)

        data.neighboursOf(obj)
            .filterNot { visited.contains(it) }
            .forEach { neighbour ->
                val distance = distances[obj]!! + 1
                distances.merge(neighbour, distance) { old, new -> if (new < old) new else old }
                queue.add(neighbour)
            }
    }

    return distances[end]!! - 2
}

fun List<Pair<String, String>>.neighboursOf(obj: String): List<String> =
    this.filter { it.first == obj || it.second == obj }
        .map { if (it.first == obj) it.second else it.first }

fun main() {
    val input = IO.readLines("day06.txt") { it.split(')') }.map { it[0] to it[1] }
    println("Part one: ${checksum(input)}")
    println("Part two: ${orbitalTransfers(input)}")
}