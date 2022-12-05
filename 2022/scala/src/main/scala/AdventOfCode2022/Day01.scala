package AdventOfCode2022

object Day01 {
  def parse(input: String): Seq[Int] = input
    .split(System.lineSeparator() + System.lineSeparator())
    .map(_.split(System.lineSeparator()).map(_.toInt).sum)

  def partOne(input: String): Int = parse(input).max

  def partTwo(input: String): Int = parse(input).sorted.takeRight(3).sum

  def main(args: Array[String]): Unit = {
    val input = io.Source.fromResource("Day01.txt").mkString.trim
    println(s"Part one: ${partOne(input)}")
    println(s"Part two: ${partTwo(input)}")
  }
}
