package nl.sanderp.aoc.day05

import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.dropWhile
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import nl.sanderp.aoc.IO

enum class ParameterMode { Position, Immediate }

sealed class Instruction(val parameterCount: Int)
data class Add(val p1: Int, val p2: Int, val p3: Int) : Instruction(3)
data class Multiply(val p1: Int, val p2: Int, val p3: Int) : Instruction(3)
data class Input(val p1: Int) : Instruction(1)
data class Output(val p2: Int) : Instruction(1)
data class JumpIfTrue(val p1: Int, val p2: Int) : Instruction(2)
data class JumpIfFalse(val p1: Int, val p2: Int) : Instruction(2)
data class LessThan(val p1: Int, val p2: Int, val p3: Int): Instruction(3)
data class Equals(val p1: Int, val p2: Int, val p3: Int): Instruction(3)
object Halt : Instruction(0)

class IntCode(instructions: List<Int>) {
    private val memory = instructions.toIntArray()

    suspend fun run(input: Channel<Int>, output: Channel<Int>) {
        var pointer = 0
        while (true) {
            val instruction = parseInstruction(pointer)
            pointer += instruction.parameterCount + 1
            when (instruction) {
                is Add -> memory[instruction.p3] = memory[instruction.p1] + memory[instruction.p2]
                is Multiply -> memory[instruction.p3] = memory[instruction.p1] * memory[instruction.p2]
                is Input -> memory[instruction.p1] = input.receive()
                is Output -> output.send(memory[instruction.p2])
                is JumpIfTrue -> {
                    if (memory[instruction.p1] != 0) {
                        pointer = memory[instruction.p2]
                    }
                }
                is JumpIfFalse -> {
                    if (memory[instruction.p1] == 0) {
                        pointer = memory[instruction.p2]
                    }
                }
                is LessThan -> memory[instruction.p3] = if (memory[instruction.p1] < memory[instruction.p2]) 1 else 0
                is Equals -> memory[instruction.p3] = if (memory[instruction.p1] == memory[instruction.p2]) 1 else 0
                is Halt -> break
            }
        }
        output.close()
    }

    private fun parseInstruction(pointer: Int): Instruction {
        val opcode = memory[pointer] % 100
        val parameters: Sequence<Int> = sequence {
            var mask = memory[pointer] / 100
            for (i in 1..3) {
                when (parseParameterMode(mask % 10)) {
                    ParameterMode.Immediate -> yield(pointer + i)
                    ParameterMode.Position -> yield(memory[pointer + i])
                }
                mask /= 10
            }
        }

        return when (opcode) {
            1 -> Add(parameters.elementAt(0), parameters.elementAt(1), parameters.elementAt(2))
            2 -> Multiply(parameters.elementAt(0), parameters.elementAt(1), parameters.elementAt(2))
            3 -> Input(parameters.first())
            4 -> Output(parameters.first())
            5 -> JumpIfTrue(parameters.elementAt(0), parameters.elementAt(1))
            6 -> JumpIfFalse(parameters.elementAt(0), parameters.elementAt(1))
            7 -> LessThan(parameters.elementAt(0), parameters.elementAt(1), parameters.elementAt(2))
            8 -> Equals(parameters.elementAt(0), parameters.elementAt(1), parameters.elementAt(2))
            99 -> Halt
            else -> throw IllegalArgumentException("Unknown opcode '$opcode'")
        }
    }

    private fun parseParameterMode(digit: Int) = when (digit) {
        0 -> ParameterMode.Position
        1 -> ParameterMode.Immediate
        else -> throw IllegalArgumentException("Unknown parameter mode '$digit'")
    }
}

fun partOne(instructions: List<Int>): Int {
    val intCode = IntCode(instructions)
    val input = Channel<Int>()
    val output = Channel<Int>()
    return runBlocking {
        launch {
            input.send(1)
            input.close()
        }
        launch { intCode.run(input, output) }
        output.receiveAsFlow().dropWhile { it == 0 }.first()
    }
}

fun partTwo(instructions: List<Int>): Int {
    val intCode = IntCode(instructions)
    val input = Channel<Int>()
    val output = Channel<Int>()
    return runBlocking {
        launch {
            input.send(5)
            input.close()
        }
        launch { intCode.run(input, output) }
        output.receive()
    }
}

fun main() {
    val instructions = IO.readText("day05.txt") { it.split(',') }.map { it.toInt() }

    println("Part one: ${partOne(instructions)}")
    println("Part two: ${partTwo(instructions)}")
}