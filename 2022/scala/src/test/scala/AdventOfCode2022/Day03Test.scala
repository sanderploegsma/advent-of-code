package AdventOfCode2022

import org.scalatest.flatspec.AnyFlatSpec

class Day03Test extends AnyFlatSpec {
  private val example = io.Source.fromResource("Day03.txt").getLines().toSeq

  "Part one" should "provide the correct answer for example input" in {
    assert(Day03.partOne(example) == 157)
  }

  "Part two" should "provide the correct answer for example input" in {
    assert(Day03.partTwo(example) == 70)
  }
}
