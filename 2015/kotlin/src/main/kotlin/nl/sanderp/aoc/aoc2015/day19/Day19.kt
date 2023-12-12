package nl.sanderp.aoc.aoc2015.day19

import nl.sanderp.aoc.common.readResource

fun parse(input: List<String>): Pair<List<Pair<String, String>>, String> {
    val replacements = input.takeWhile { it.isNotBlank() }
        .map { it.split(" => ").let { (a, b) -> a to b } }

    val molecule = input.drop(replacements.size + 1).first()

    return replacements to molecule
}

fun candidates(rules: List<Pair<String, String>>, molecule: String) =
    rules.flatMap { (find, replace) ->
        Regex(find).findAll(molecule).map { match ->
            molecule.replaceRange(match.range, replace)
        }
    }.toSet()

fun stepCount(rules: List<Pair<String, String>>, molecule: String): Int {
    val reversedRules = rules.map { (a, b) -> b to a }
    var steps = 0
    var target = molecule

    while (target != "e") {
        for ((find, replace) in reversedRules) {
            if (target.contains(find)) {
                target = target.replaceFirst(find, replace)
                steps++
            }
        }
    }

    return steps
}

fun main() {
    val (replacements, molecule) = readResource("19.txt").lines().let(::parse)

    println("Part one: ${candidates(replacements, molecule).size}")
    println("Part two: ${stepCount(replacements, molecule)}")
}
