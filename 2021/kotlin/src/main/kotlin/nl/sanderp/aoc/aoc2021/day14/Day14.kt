package nl.sanderp.aoc.aoc2021.day14

import nl.sanderp.aoc.aoc2021.increaseBy
import nl.sanderp.aoc.aoc2021.measureDuration
import nl.sanderp.aoc.aoc2021.prettyPrint
import nl.sanderp.aoc.aoc2021.readResource

fun main() {
    val input = readResource("Day14.txt").lines()
    val template = parseTemplate(input.first())
    val rules = input.drop(2).associate { parseRule(it) }

    val (answer1, duration1) = measureDuration<Long> { partOne(template, rules) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Long> { partTwo(template, rules) }
    println("Part one: $answer2 (took ${duration2.prettyPrint()})")
}

private fun parseTemplate(template: String) = template.zipWithNext { a, b -> a to b }.groupingBy { it }.eachCount().mapValues { it.value.toLong() }

private fun parseRule(rule: String) = rule.split(" -> ").let { (it[0][0] to it[0][1]) to it[1][0] }

private fun insert(polymer: Map<Pair<Char, Char>, Long>, rules: Map<Pair<Char, Char>, Char>) = buildMap<Pair<Char, Char>, Long> {
    for ((pair, n) in polymer) {
        val c = rules[pair]!!
        increaseBy(pair.first to c, n)
        increaseBy(c to pair.second, n)
    }
}

private fun build(polymer: Map<Pair<Char, Char>, Long>, rules: Map<Pair<Char, Char>, Char>) = generateSequence(polymer) { insert(it, rules) }

private fun partOne(polymer: Map<Pair<Char, Char>, Long>, rules: Map<Pair<Char, Char>, Char>): Long {
    val result = build(polymer, rules).drop(10).first()
    val counts = result.map { it.key.second to it.value }.groupBy { it.first }.mapValues { it.value.sumOf { it.second } }
    return counts.maxOf { it.value } - counts.minOf { it.value }
}

private fun partTwo(polymer: Map<Pair<Char, Char>, Long>, rules: Map<Pair<Char, Char>, Char>): Long {
    val result = build(polymer, rules).drop(40).first()
    val counts = result.map { it.key.second to it.value }.groupBy { it.first }.mapValues { it.value.sumOf { it.second } }
    return counts.maxOf { it.value } - counts.minOf { it.value }
}