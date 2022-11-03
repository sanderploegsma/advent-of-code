package nl.sanderp.aoc.aoc2021.day04

import nl.sanderp.aoc.common.measureDuration
import nl.sanderp.aoc.common.prettyPrint
import nl.sanderp.aoc.common.readResource

fun main() {
    val (numbers, boards) = parse(readResource("Day04.txt").lines())

    val (answer1, duration1) = measureDuration<Int> { partOne(numbers, boards) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> { partTwo(numbers, boards) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private fun parse(input: List<String>): Pair<List<Int>, List<Board>> {
    val numbers = input.first().split(",").map { it.toInt() }
    val boards = input.drop(1).chunked(6).map { lines ->
        Board(lines.drop(1).map { l -> l.split(" ").filterNot { it == "" }.map { it.toInt() } })
    }

    return numbers to boards
}

class Board(numbers: List<List<Int>>) {
    private val rows = numbers.map { row -> row.map { it to false }.toTypedArray() }.toTypedArray()
    private val cols
        get() = rows.indices.map { i -> rows.indices.map { j -> rows[j][i] } }

    val unmarked
        get() = rows.flatMap { row -> row.filterNot { it.second } }.map { it.first }

    fun mark(number: Int): Boolean {
        for (i in rows.indices) {
            for (j in rows[i].indices) {
                if (rows[i][j].first == number) {
                    rows[i][j] = number to true
                }
            }
        }

        return rows.any { row -> row.all { it.second } } || cols.any { col -> col.all { it.second }}
    }
}

private fun partOne(numbers: List<Int>, boards: List<Board>): Int {
    for (number in numbers) {
        for (board in boards) {
            if (board.mark(number)) {
                return board.unmarked.sum() * number
            }
        }
    }

    return -1
}

private fun partTwo(numbers: List<Int>, boards: List<Board>): Int {
    val playing = boards.toMutableList()

    for (number in numbers) {
        val wonRound = mutableListOf<Board>()
        for (board in playing) {
            if (board.mark(number)) {
                if (playing.size - wonRound.size == 1) {
                    return board.unmarked.sum() * number
                }

                wonRound.add(board)
            }
        }
        playing.removeAll(wonRound)
    }

    return -1
}