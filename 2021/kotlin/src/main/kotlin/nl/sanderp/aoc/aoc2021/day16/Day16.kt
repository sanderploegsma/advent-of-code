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
    override val versions: List<Int> = listOf(version)
}

private enum class OperatorType { Sum, Product, Minimum, Maximum, GreaterThan, LessThan, EqualTo }

private class Operator(version: Int, val type: OperatorType, val packets: List<Packet>) : Packet(version) {
    override val versions: List<Int>
        get() = listOf(version).plus(packets.flatMap { it.versions })

    override val value
        get() = when (type) {
            OperatorType.Sum -> packets.sumOf { it.value }
            OperatorType.Product -> packets.fold(1L) { p, v -> p * v.value }
            OperatorType.Minimum -> packets.minOf { it.value }
            OperatorType.Maximum -> packets.maxOf { it.value }
            OperatorType.GreaterThan -> if (packets[0].value > packets[1].value) 1L else 0L
            OperatorType.LessThan -> if (packets[0].value < packets[1].value) 1L else 0L
            OperatorType.EqualTo -> if (packets[0].value == packets[1].value) 1L else 0L
        }
}

private class Reader(data: String) {
    private var buffer = data

    fun next(): Packet {
        val version = readBits(3).toInt(2)

        return when (val typeId = readBits(3).toInt(2)) {
            0 -> Operator(version, OperatorType.Sum, readOperatorPackets())
            1 -> Operator(version, OperatorType.Product, readOperatorPackets())
            2 -> Operator(version, OperatorType.Minimum, readOperatorPackets())
            3 -> Operator(version, OperatorType.Maximum, readOperatorPackets())
            4 -> LiteralValue(version, readLiteralValue())
            5 -> Operator(version, OperatorType.GreaterThan, readOperatorPackets())
            6 -> Operator(version, OperatorType.LessThan, readOperatorPackets())
            7 -> Operator(version, OperatorType.EqualTo, readOperatorPackets())
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

    private fun readOperatorPackets(): List<Packet> = when (readBits(1)) {
        "0" -> buildList {
            val length = readBits(15).toLong(2)
            val bufferStopSize = buffer.length - length
            while (buffer.length > bufferStopSize) {
                add(next())
            }
        }
        "1" -> buildList {
            val packets = readBits(11).toLong(2)
            for (i in 1..packets) {
                add(next())
            }
        }
        else -> throw IllegalArgumentException()
    }

    private fun readBits(n: Int) = buffer.take(n).also { buffer = buffer.drop(n) }
}
