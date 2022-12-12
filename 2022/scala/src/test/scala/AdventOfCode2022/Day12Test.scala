package AdventOfCode2022

import org.scalatest.flatspec.AnyFlatSpec

class Day12Test extends AnyFlatSpec {
  private val example = io.Source.fromResource("Day12.txt").getLines().toSeq

  "Part one" should "provide the correct answer for example input" in {
    assert(Day12.partOne(example) == 31)
  }

  "Part two" should "provide the correct answer for example input" in {
    assert(Day12.partTwo(example) == 29)
  }
}
