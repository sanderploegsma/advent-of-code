package nl.sanderp.aoc.aoc2021.day18

import nl.sanderp.aoc.aoc2021.*

fun main() {
    val input = readResource("Day18.txt").lines().map { parse(it) }

    val (answer1, duration1) = measureDuration<Int> { input.reduce { a, b -> a + b }.magnitude }
    println("Part one: $answer1 (took ${duration1.prettyPrint()})")

    val (answer2, duration2) = measureDuration<Int> {
        input.combinations(2).flatMap { (a, b) -> listOf(a + b, b + a) }.maxOf { it.magnitude }
    }
    println("Part two: $answer2 (took ${duration2.prettyPrint()})")
}

private data class ExplosionResult(val element: Element, val leftValue: Int, val rightValue: Int)

private sealed class Element(val depth: Int) {
    abstract val magnitude: Int
    abstract fun incrementDepth(): Element
    abstract fun addValueLeftToRight(value: Int): Element
    abstract fun addValueRightToLeft(value: Int): Element
    abstract fun tryExplode(): ExplosionResult?
    abstract fun trySplit(): Element?

    operator fun plus(other: Element) = Node(this.incrementDepth(), other.incrementDepth(), 0).reduce()

    protected fun reduce(): Element = tryExplode()?.element?.reduce() ?: trySplit()?.reduce() ?: this
}

private class Leaf(val value: Int, depth: Int) : Element(depth) {
    override val magnitude: Int = value
    override fun incrementDepth() = Leaf(value, depth + 1)
    override fun addValueLeftToRight(value: Int) = Leaf(this.value + value, depth)
    override fun addValueRightToLeft(value: Int) = Leaf(this.value + value, depth)
    override fun tryExplode(): ExplosionResult? = null

    override fun trySplit(): Element? = if (value < 10) null else {
        val left = Leaf(value / 2, depth + 1)
        val right = Leaf((value + 1) / 2, depth + 1)
        Node(left, right, depth)
    }
}

private class Node(val left: Element, val right: Element, depth: Int) : Element(depth) {
    override val magnitude: Int by lazy { 3 * left.magnitude + 2 * right.magnitude }
    override fun incrementDepth(): Element = Node(left.incrementDepth(), right.incrementDepth(), depth + 1)
    override fun addValueLeftToRight(value: Int) = Node(left.addValueLeftToRight(value), right, depth)
    override fun addValueRightToLeft(value: Int) = Node(left, right.addValueRightToLeft(value), depth)

    override fun tryExplode(): ExplosionResult? {
        if (left is Leaf && right is Leaf && depth == 4) {
            return ExplosionResult(Leaf(0, 4), left.value, right.value)
        }

        return tryExplodeLeft() ?: tryExplodeRight()
    }

    override fun trySplit(): Element? =
        left.trySplit()?.let { Node(it, right, depth) } ?: right.trySplit()?.let { Node(left, it, depth) }

    private fun tryExplodeLeft(): ExplosionResult? =
        left.tryExplode()?.let {
            ExplosionResult(
                element = Node(it.element, right.addValueLeftToRight(it.rightValue), depth),
                leftValue = it.leftValue,
                rightValue = 0,
            )
        }

    private fun tryExplodeRight(): ExplosionResult? =
        right.tryExplode()?.let {
            ExplosionResult(
                element = Node(left.addValueRightToLeft(it.leftValue), it.element, depth),
                leftValue = 0,
                rightValue = it.rightValue,
            )
        }
}

private fun parse(line: String): Element {
    fun parseSegment(s: String, depth: Int): Pair<Element, Int> = when (val c = s.first()) {
        '[' -> {
            val (left, leftOffset) = parseSegment(s.drop(1), depth + 1)
            val (right, rightOffset) = parseSegment(s.drop(1 + leftOffset), depth + 1)
            Node(left, right, depth) to 1 + leftOffset + rightOffset
        }
        ']', ',' -> parseSegment(s.drop(1), depth).let { (x, offset) -> x to offset + 1 }
        else -> Leaf(c.asDigit(), depth) to 1
    }

    return parseSegment(line, 0).first
}
