package nl.sanderp.aoc.common

import kotlin.math.abs

fun Char.asDigit(): Int = code - '0'.code

fun Int.toDigits() = abs(this).toString().map { it.asDigit() }
