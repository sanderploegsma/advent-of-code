package AdventOfCode2022

import org.scalatest.flatspec.AnyFlatSpec

class Day02Test extends AnyFlatSpec {
  private val example = io.Source.fromResource("Day02.txt").getLines().toSeq

  "Part one" should "provide the correct answer for example input" in {
    assert(Day02.partOne(example) == 15)
  }

  "Part two" should "provide the correct answer for example input" in {
    assert(Day02.partTwo(example) == 12)
  }
}