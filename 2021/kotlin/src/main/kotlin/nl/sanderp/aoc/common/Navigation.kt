package nl.sanderp.aoc.common

enum class Direction { North, East, South, West }

private val cw = listOf(Direction.North, Direction.East, Direction.South, Direction.West)
private val ccw = listOf(Direction.North, Direction.West, Direction.South, Direction.East)

fun Direction.turnClockwise(times: Int = 1) = turn(cw, times)

fun Direction.turnCounterClockwise(times: Int = 1) = turn(ccw, times)

private fun Direction.turn(turns: List<Direction>, times: Int): Direction {
    val id = (turns.indexOf(this) + times) % turns.size
    return turns[id]
}

fun Point2D.move(direction: Direction, steps: Int = 1) = when (direction) {
    Direction.North -> x to y + steps
    Direction.East -> x + steps to y
    Direction.South -> x to y - steps
    Direction.West -> x - steps to y
}