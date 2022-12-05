package AdventOfCode2022

object Day02 {
  def partOne(input: Seq[String]): Int = {
    val scores = Seq("B X", "C Y", "A Z", "A X", "B Y", "C Z", "C X", "A Y", "B Z").zipWithIndex.toMap.mapValues(_ + 1)
    input.map(scores).sum
  }

  def partTwo(input: Seq[String]): Int = {
    val scores = Seq("B X", "C X", "A X", "A Y", "B Y", "C Y", "C Z", "A Z", "B Z").zipWithIndex.toMap.mapValues(_ + 1)
    input.map(scores).sum
  }

  def main(args: Array[String]): Unit = {
    val input = io.Source.fromResource("Day02.txt").getLines().toSeq
    println(s"Part one: ${partOne(input)}")
    println(s"Part two: ${partTwo(input)}")
  }
}
