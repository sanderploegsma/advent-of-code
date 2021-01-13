import kotlin.math.floor

fun calculateFuel(mass: Int) = floor(mass.toDouble() / 3.0).toInt() - 2

fun calculateFuelRec(mass: Int): Int {
    val fuel = calculateFuel(mass)
    return if (fuel < 0) 0
    else fuel + calculateFuelRec(fuel)
}

fun main(args: Array<String>) {
    val input = readLines("day01.txt") { it.toInt() }

    println("Part one: ${input.sumBy { calculateFuel(it) }}")
    println("Part two: ${input.sumBy { calculateFuelRec(it) }}")
}