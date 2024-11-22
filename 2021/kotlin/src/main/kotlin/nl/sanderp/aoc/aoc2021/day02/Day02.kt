package nl.sanderp.aoc.aoc2021.day02

import nl.sanderp.aoc.common.measureDuration
import nl.sanderp.aoc.common.prettyPrint
import nl.sanderp.aoc.common.readResource

fun main() {
    val input = readResource("Day02.txt").lines().map { parse(it) }

    val (answer1, duration1) = measureDuration<Int> { partOne(input) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Long> { partTwo(input) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

sealed class Command(val x: Int)
class Forward(x: Int) : Command(x)
class Up(x: Int) : Command(x)
class Down(x: Int) : Command(x)

private fun parse(line: String) = line.split(' ').let { (action, x) ->
    when (action) {
        "forward" -> Forward(x.toInt())
        "up" -> Up(x.toInt())
        "down" -> Down(x.toInt())
        else -> throw IllegalArgumentException(action)
    }
}

fun partOne(commands: List<Command>): Int {
    data class State(val position: Int, val depth: Int)

    val (position, depth) = commands.fold(State(0, 0)) { state, command ->
        when (command) {
            is Forward -> state.copy(position = state.position + command.x)
            is Down -> state.copy(depth = state.depth + command.x)
            is Up -> state.copy(depth = state.depth - command.x)
        }
    }

    return position * depth
}

fun partTwo(commands: List<Command>): Long {
    data class State(val position: Int, val depth: Int, val aim: Int)

    val (position, depth) = commands.fold(State(0, 0, 0)) { state, command ->
        when (command) {
            is Forward -> state.copy(position = state.position + command.x, depth = state.depth + command.x * state.aim)
            is Up -> state.copy(aim = state.aim - command.x)
            is Down -> state.copy(aim = state.aim + command.x)
        }
    }

    return position.toLong() * depth.toLong()
}
