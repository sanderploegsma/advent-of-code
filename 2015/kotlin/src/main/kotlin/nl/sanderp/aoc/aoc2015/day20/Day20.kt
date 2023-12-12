package nl.sanderp.aoc.aoc2015.day20

const val target = 36000000

val partOne by lazy {
    val presents = Array(target / 10) { 0 }
    for (elf in 1 until target / 10) {
        for (house in elf until target / 10 step elf) {
            presents[house] += 10 * elf
        }
    }

    presents.indexOfFirst { it >= target }
}

val partTwo by lazy {
    val presents = Array(target / 10) { 0 }
    for (elf in 1 until target / 10) {
        for (house in elf until target / 10 step elf) {
            presents[house] += 11 * elf
            if (house == 50 * elf) {
                break
            }
        }
    }

    presents.indexOfFirst { it >= target }
}

fun main() {
    println("Part one: $partOne")
    println("Part two: $partTwo")
}
