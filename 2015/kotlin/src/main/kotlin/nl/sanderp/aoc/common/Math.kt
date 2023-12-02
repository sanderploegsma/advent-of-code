package nl.sanderp.aoc.common

import kotlin.math.PI
import kotlin.math.abs

/**
 * Calculates the greatest common divisor between two numbers.
 */
fun gcd(a: Int, b: Int): Int = if (b == 0) a else gcd(b, a % b)

/**
 * Calculates the least common multiple between two numbers.
 */
fun lcm(a: Int, b: Int): Int = abs(a * b) / gcd(a, b)

fun Double.radians() = PI * this / 180.0
fun Int.radians() = toDouble().radians()
