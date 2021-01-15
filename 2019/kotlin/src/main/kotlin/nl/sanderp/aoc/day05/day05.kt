package nl.sanderp.aoc.day05

import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.dropWhile
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import nl.sanderp.aoc.IO
import nl.sanderp.aoc.IntCode

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