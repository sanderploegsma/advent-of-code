package nl.sanderp.aoc.aoc2021.day24

import nl.sanderp.aoc.aoc2021.measureDuration
import nl.sanderp.aoc.aoc2021.prettyPrint
import nl.sanderp.aoc.aoc2021.readResource

fun main() {
    val input = readResource("Day24.txt").lines().map { parse(it) }

    val (answer1, duration1) = measureDuration<Long> { partOne(input) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")
}

private sealed class Instruction
private data class Input(val a: Int) : Instruction()
private data class Add(val a: Int, val b: Int) : Instruction()
private data class AddLiteral(val a: Int, val b: Int) : Instruction()
private data class Multiply(val a: Int, val b: Int) : Instruction()
private data class MultiplyLiteral(val a: Int, val b: Int) : Instruction()
private data class DivideBy(val a: Int, val b: Int) : Instruction()
private data class DivideByLiteral(val a: Int, val b: Int) : Instruction()
private data class Mod(val a: Int, val b: Int) : Instruction()
private data class ModLiteral(val a: Int, val b: Int) : Instruction()
private data class Equals(val a: Int, val b: Int) : Instruction()
private data class EqualsLiteral(val a: Int, val b: Int) : Instruction()

private fun parse(line: String): Instruction {
    val parts = line.split(' ')
    val a = parts[1][0] - 'w'

    if (parts[0] == "inp") {
        return Input(a)
    }

    val b = parts[2]

    return when (parts[0]) {
        "add" -> if (b[0].isLetter()) Add(a, b[0]-'w') else AddLiteral(a, b.toInt())
        "mul" -> if (b[0].isLetter()) Multiply(a, b[0]-'w') else MultiplyLiteral(a, b.toInt())
        "div" -> if (b[0].isLetter()) DivideBy(a, b[0]-'w') else DivideByLiteral(a, b.toInt())
        "mod" -> if (b[0].isLetter()) Mod(a, b[0]-'w') else ModLiteral(a, b.toInt())
        "eql" -> if (b[0].isLetter()) Equals(a, b[0]-'w') else EqualsLiteral(a, b.toInt())
        else -> throw IllegalArgumentException("Cannot parse line: $line")
    }
}

private fun run(instructions: List<Instruction>, w: Int, z: Int): Map<IntArray, Int> {
    val memory = arrayOf(w, 0, 0, z)
    val states = mutableMapOf<IntArray, Int>()
    for ((n, i) in instructions.withIndex()) {
        when (i) {
            is Input -> {
                val rest = instructions.drop(n + 1)
                for (d in 1..9) {
                    run(rest, d, memory[3]).forEach { (k, v) -> states.putIfAbsent(IntArray(1) { d } + k, v) }
                }

                return states
            }
            is Add -> memory[i.a] = memory[i.a] + memory[i.b]
            is AddLiteral -> memory[i.a] = memory[i.a] + i.b
            is Multiply -> memory[i.a] = memory[i.a] * memory[i.b]
            is MultiplyLiteral -> memory[i.a] = memory[i.a] * i.b
            is DivideBy -> memory[i.a] = memory[i.a] / memory[i.b]
            is DivideByLiteral -> memory[i.a] = memory[i.a] / i.b
            is Mod -> memory[i.a] = memory[i.a] % memory[i.b]
            is ModLiteral -> memory[i.a] = memory[i.a] % i.b
            is Equals -> if (memory[i.a] == memory[i.b]) memory[i.a] = 1 else memory[i.a] = 0
            is EqualsLiteral -> if (memory[i.a] == i.b) memory[i.a] = 1 else memory[i.a] = 0
        }
    }

    states[IntArray(0)] = memory[3]
    return states
}

private fun partOne(instructions: List<Instruction>): Long {
    val states = run(instructions, 0, 0)
        .mapKeys { it.key.joinToString("").toLong() }

    return states.filterValues { it == 0 }.maxOf { it.key }
}

