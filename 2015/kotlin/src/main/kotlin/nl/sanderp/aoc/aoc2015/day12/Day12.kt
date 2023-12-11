package nl.sanderp.aoc.aoc2015.day12

import kotlinx.serialization.json.*
import nl.sanderp.aoc.common.readResource

fun sumNumbers(element: JsonElement) = sumNumbers(element) { false }

fun sumNumbers(element: JsonElement, ignoreObject: (JsonObject) -> Boolean): Int = when {
    element is JsonPrimitive -> element.intOrNull ?: 0
    element is JsonArray -> element.sumOf { sumNumbers(it, ignoreObject) }
    element is JsonObject && !ignoreObject(element) -> element.values.sumOf { sumNumbers(it, ignoreObject) }
    else -> 0
}

fun main() {
    val input = Json.decodeFromString<JsonElement>(readResource("Day12.txt"))
    println("Part one: ${sumNumbers(input)}")
    println("Part two: ${sumNumbers(input) { it.containsValue(JsonPrimitive("red")) }}")
}
