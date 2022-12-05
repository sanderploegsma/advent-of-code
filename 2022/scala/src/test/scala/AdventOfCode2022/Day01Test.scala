package AdventOfCode2022

import org.scalatest.flatspec.AnyFlatSpec

class Day01Test extends AnyFlatSpec {
  private val example = io.Source.fromResource("Day01.txt").mkString

  "Part one" should "provide the correct answer for example input" in {
    assert(Day01.partOne(example) == 24000)
  }

  "Part two" should "provide the correct answer for example input" in {
    assert(Day01.partTwo(example) == 45000)
  }
}