package AdventOfCode2022

import org.scalatest.flatspec.AnyFlatSpec

class Day04Test extends AnyFlatSpec {
  private val example = io.Source.fromResource("Day04.txt").getLines().toSeq

  "Part one" should "provide the correct answer for example input" in {
    assert(Day04.partOne(example) == 2)
  }

  "Part two" should "provide the correct answer for example input" in {
    assert(Day04.partTwo(example) == 4)
  }
}
