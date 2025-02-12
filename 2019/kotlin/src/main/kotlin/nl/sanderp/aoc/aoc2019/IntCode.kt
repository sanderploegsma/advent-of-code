package nl.sanderp.aoc.aoc2019

import kotlinx.coroutines.channels.ReceiveChannel
import kotlinx.coroutines.channels.SendChannel

internal enum class ParameterMode { Position, Immediate }

internal sealed class Instruction(val parameterCount: Int)
internal data class Add(val p1: Int, val p2: Int, val p3: Int) : Instruction(3)
internal data class Multiply(val p1: Int, val p2: Int, val p3: Int) : Instruction(3)
internal data class Input(val p1: Int) : Instruction(1)
internal data class Output(val p2: Int) : Instruction(1)
internal data class JumpIfTrue(val p1: Int, val p2: Int) : Instruction(2)
internal data class JumpIfFalse(val p1: Int, val p2: Int) : Instruction(2)
internal data class LessThan(val p1: Int, val p2: Int, val p3: Int): Instruction(3)
internal data class Equals(val p1: Int, val p2: Int, val p3: Int): Instruction(3)
internal object Halt : Instruction(0)

class IntCode(instructions: List<Int>) {
    private val memory = instructions.toIntArray()

    suspend fun run(input: ReceiveChannel<Int>, output: SendChannel<Int>) {
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
