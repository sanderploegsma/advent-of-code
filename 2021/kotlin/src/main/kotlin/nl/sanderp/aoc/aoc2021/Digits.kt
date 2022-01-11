package nl.sanderp.aoc.aoc2021

import kotlin.math.abs

fun Char.asDigit(): Int = code - '0'.code

fun Int.toDigits() = abs(this).toString().map { it.asDigit() }