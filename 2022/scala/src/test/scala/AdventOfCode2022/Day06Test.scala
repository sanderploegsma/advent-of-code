package AdventOfCode2022

import org.scalatest.flatspec.AnyFlatSpec

class Day06Test extends AnyFlatSpec {
  val example = "mjqjpqmgbljsphdztnvjfqwrcgsmlb"

  "Part one" should "provide the correct answer for example input" in {
    assert(Day06.partOne(example) == 7)
  }

  "Part two" should "provide the correct answer for example input" in {
    assert(Day06.partTwo(example) == 19)
  }
}
