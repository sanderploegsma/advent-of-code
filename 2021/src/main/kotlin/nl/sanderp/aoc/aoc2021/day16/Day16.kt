package nl.sanderp.aoc.aoc2021.day16

import nl.sanderp.aoc.aoc2021.readResource

fun main() {
    val input = readResource("Day16.txt")
        .map { it.toString().toInt(16) }
        .joinToString("") { it.toString(2).padStart(4, '0') }

    val packets = Reader(input).next()

    println("Part one: ${packets.versions.sum()}")
    println("Part two: ${packets.value}")
}

private sealed class Packet(val version: Int) {
    abstract val versions: List<Int>
    abstract val value: Long
}

private class LiteralValue(version: Int, override val value: Long) : Packet(version) {
    override val versions: List<Int>
        get() = listOf(version)
}

private abstract class Operator(version: Int, val packets: List<Packet>) : Packet(version) {
    override val versions: List<Int>
        get() = listOf(version).plus(packets.flatMap { it.versions })

    override val value: Long
        get() = calculateValue()

    abstract fun calculateValue(): Long
}

private class Sum(version: Int, packets: List<Packet>) : Operator(version, packets) {
    override fun calculateValue() = packets.sumOf { it.value }
}

private class Product(version: Int, packets: List<Packet>) : Operator(version, packets) {
    override fun calculateValue() = packets.fold(1L) { p, v -> p * v.value }
}

private class Minimum(version: Int, packets: List<Packet>) : Operator(version, packets) {
    override fun calculateValue() = packets.minOf { it.value }
}

private class Maximum(version: Int, packets: List<Packet>) : Operator(version, packets) {
    override fun calculateValue() = packets.maxOf { it.value }
}

private class GreaterThan(version: Int, packets: List<Packet>) : Operator(version, packets) {
    override fun calculateValue() = if (packets[0].value > packets[1].value) 1L else 0L
}

private class LessThan(version: Int, packets: List<Packet>) : Operator(version, packets) {
    override fun calculateValue() = if (packets[0].value < packets[1].value) 1L else 0L
}

private class EqualTo(version: Int, packets: List<Packet>) : Operator(version, packets) {
    override fun calculateValue() = if (packets[0].value == packets[1].value) 1L else 0L
}

private class Reader(data: String) {
    private var buffer = data

    fun next(): Packet {
        val version = readBits(3).toInt(2)
        val typeId = readBits(3).toInt(2)

        return when (typeId) {
            0 -> Sum(version, readOperatorPackets())
            1 -> Product(version, readOperatorPackets())
            2 -> Minimum(version, readOperatorPackets())
            3 -> Maximum(version, readOperatorPackets())
            4 -> LiteralValue(version, readLiteralValue())
            5 -> GreaterThan(version, readOperatorPackets())
            6 -> LessThan(version, readOperatorPackets())
            7 -> EqualTo(version, readOperatorPackets())
            else -> throw IllegalArgumentException("Invalid type ID '$typeId'")
        }
    }

    private fun readLiteralValue(): Long {
        val bits = buildString {
            while (buffer.first() == '1') {
                val chunk = readBits(5)
                append(chunk.drop(1))
            }

            append(readBits(5).drop(1))
        }

        return bits.toLong(2)
    }

    private fun readOperatorPackets(): List<Packet> {
        if (readBits(1) == "0") {
            val length = readBits(15).toLong(2)
            val bufferStopSize = buffer.length - length

            return buildList {
                while (buffer.length > bufferStopSize) {
                    add(next())
                }
            }
        } else {
            val packets = readBits(11).toLong(2)
            return buildList {
                for (i in 1..packets) {
                    add(next())
                }
            }
        }
    }

    private fun readBits(n: Int) = buffer.take(n).also { buffer = buffer.drop(n) }
}

