package nl.sanderp.aoc.aoc2019.day07

import kotlinx.coroutines.ExperimentalCoroutinesApi
import kotlinx.coroutines.channels.BroadcastChannel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import nl.sanderp.aoc.aoc2019.IO
import nl.sanderp.aoc.aoc2019.IntCode
import nl.sanderp.aoc.aoc2019.permutations

@ExperimentalCoroutinesApi
fun runAmplifiers(instructions: List<Int>, phaseSettings: List<Int>): Int {
    var result = -1
    runBlocking {
        val input = BroadcastChannel<Int>(2)
        val output = input.openSubscription()

        input.send(phaseSettings.first())
        input.send(0)

        val (ab, bc, cd, de) = phaseSettings.drop(1).map { phase -> Channel<Int>(1).also { it.send(phase) } }

        launch {
            for (value in input.openSubscription()) {
                result = value
            }
        }

        launch { IntCode(instructions).run(output, ab) }
        launch { IntCode(instructions).run(ab, bc) }
        launch { IntCode(instructions).run(bc, cd) }
        launch { IntCode(instructions).run(cd, de) }
        launch { IntCode(instructions).run(de, input) }
    }
    return result
}

@ExperimentalCoroutinesApi
fun findMaxSignal(instructions: List<Int>): Int =
    (0 .. 4).toList().permutations().maxOf { runAmplifiers(instructions, it) }

@ExperimentalCoroutinesApi
fun findMaxSignalWithFeedback(instructions: List<Int>): Int =
    (5 .. 9).toList().permutations().maxOf { runAmplifiers(instructions, it) }

@ExperimentalCoroutinesApi
fun main() {
    val input = IO.readText("day07.txt") { it.split(",") }.map(String::toInt)
    println("Part one: ${findMaxSignal(input)}")
    println("Part two: ${findMaxSignalWithFeedback(input)}")
}
