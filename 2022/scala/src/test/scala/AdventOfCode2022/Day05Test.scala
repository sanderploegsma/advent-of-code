package AdventOfCode2022

import org.scalatest.flatspec.AnyFlatSpec

class Day05Test extends AnyFlatSpec {
  private val example = io.Source.fromResource("Day05.txt").mkString

  "Part one" should "provide the correct answer for example input" in {
    assert(Day05.partOne(example) == "CMZ")
  }

  "Part two" should "provide the correct answer for example input" in {
    assert(Day05.partTwo(example) == "MCD")
  }
}