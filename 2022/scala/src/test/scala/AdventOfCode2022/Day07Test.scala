package AdventOfCode2022

import org.scalatest.flatspec.AnyFlatSpec

class Day07Test extends AnyFlatSpec {
  private val example = io.Source.fromResource("Day07.txt").getLines().toSeq

  "Part one" should "provide the correct answer for example input" in {
    assert(Day07.partOne(example) == 95437)
  }

  "Part two" should "provide the correct answer for example input" in {
    assert(Day07.partTwo(example) == 24933642)
  }
}
