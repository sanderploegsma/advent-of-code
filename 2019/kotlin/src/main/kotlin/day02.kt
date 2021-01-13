class IntCode(private val memory: IntArray) {
    fun run(): Int {
        var pointer = 0

        while (memory[pointer] != 99) {
            val x = memory[memory[pointer + 1]]
            val y = memory[memory[pointer + 2]]
            val dest = memory[pointer + 3]

            memory[dest] = when (memory[pointer]) {
                1 -> x + y
                2 -> x * y
                else -> throw IllegalArgumentException()
            }

            pointer += 4
        }

        return memory[0]
    }
}

fun findNounAndVerb(instructions: List<Int>): Pair<Int, Int> {
    for (noun in 0 .. 99) {
        for (verb in 0 .. 99) {
            val input = instructions.toIntArray().apply {
                set(1, noun)
                set(2, verb)
            }

            if (IntCode(input).run() == 19690720) {
                return Pair(noun, verb)
            }
        }
    }

    throw IllegalStateException()
}

fun main() {
    val instructions = readText("day02.txt") { it.split(',') }.map { it.toInt() }

    val input = instructions.toIntArray().apply {
        set(1, 12)
        set(2, 2)
    }
    println("Part one: ${IntCode(input).run()}")

    val (noun, verb) = findNounAndVerb(instructions)
    println("Part two: ${100 * noun + verb}")
}