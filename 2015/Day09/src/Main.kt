import java.io.File

data class Distance(val from: String, val to: String, val distance: Int)

fun parse(line: String): List<Distance> {
    val segments = line.split(" to ", " = ")
    val distance = Distance(from = segments[0], to = segments[1], distance = segments[2].toInt())
    return listOf(distance, distance.copy(from = distance.to, to = distance.from))
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

fun main() {
    val distances = File("Day09/Input.txt").readLines().flatMap { parse(it) }
    val locations = distances.map { it.from }.distinct()

    val routes = locations
        .permutations()
        .map { it.zipWithNext { a, b -> distances.first { d -> d.from == a && d.to == b } } }
        .map { it to it.sumOf { route -> route.distance }}

    println("Part one: ${routes.minOf { it.second }}")
    println("Part two: ${routes.maxOf { it.second }}")
}