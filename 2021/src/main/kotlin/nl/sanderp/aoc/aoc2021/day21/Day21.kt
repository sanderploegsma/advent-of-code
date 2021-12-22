package nl.sanderp.aoc.aoc2021.day21

import nl.sanderp.aoc.aoc2021.increaseBy
import nl.sanderp.aoc.aoc2021.measureDuration
import nl.sanderp.aoc.aoc2021.prettyPrint
import kotlin.math.min


fun main() {
    val initialGame = Game(8, 0, 7, 0)

    val (answer1, duration1) = measureDuration<Int> { partOne(initialGame) }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Long> { partTwo(initialGame) }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

enum class Player {
    Player1, Player2;

    fun next() = when (this) {
        Player1 -> Player2
        Player2 -> Player1
    }
}

private data class Game(val position1: Int, val score1: Int, val position2: Int, val score2: Int) {
    fun next(player: Player, moves: Int) = when (player) {
        Player.Player1 -> {
            val nextPosition = (position1 + moves - 1) % 10 + 1
            this.copy(position1 = nextPosition, score1 = score1 + nextPosition)
        }
        Player.Player2 -> {
            val nextPosition = (position2 + moves - 1) % 10 + 1
            this.copy(position2 = nextPosition, score2 = score2 + nextPosition)
        }
    }

    fun winner(score: Int) = when {
        score1 >= score -> Player.Player1
        score2 >= score -> Player.Player2
        else -> null
    }
}

private fun partOne(initial: Game): Int {
    data class State(val game: Game, val rolls: List<Int>)

    val dice = generateSequence(1) { it % 100 + 1 }
    val turns = generateSequence(Player.Player1) { it.next() }
    val initialState = State(initial, emptyList())

    val states = dice.chunked(3).zip(turns).runningFold(initialState) { state, (rolls, turn) ->
        State(state.game.next(turn, rolls.sum()), state.rolls + rolls)
    }

    val (game, rolls) = states.first { it.game.winner(1000) != null }
    return min(game.score1, game.score2) * rolls.size
}

private fun partTwo(initial: Game): Long {
    val moves = buildList {
        for (a in 1..3) {
            for (b in 1..3) {
                for (c in 1..3) {
                    add(a + b + c)
                }
            }
        }
    }.groupingBy { it }.eachCount()

    val winningScore = 21
    val wins = mutableMapOf(Player.Player1 to 0L, Player.Player2 to 0L)
    var openGames = mapOf(initial to 1L)
    var turn = Player.Player1

    while (openGames.isNotEmpty()) {
        val games = buildMap<Game, Long> {
            for ((game, gameCount) in openGames) {
                for ((move, moveCount) in moves) {
                    val newGame = game.next(turn, move)
                    val newGameCount = gameCount * moveCount

                    increaseBy(newGame, newGameCount)
                }
            }
        }

        games.entries.mapNotNull { it.key.winner(winningScore)?.to(it.value) }.forEach { (winner, count) ->
            wins.increaseBy(winner, count)
        }

        openGames = games.filterKeys { it.winner(winningScore) == null }.toMap()
        turn = turn.next()
    }

    return wins.values.maxOf { it }
}
