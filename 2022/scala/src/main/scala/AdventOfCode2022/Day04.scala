package AdventOfCode2022

object Day04 {
  def parse(input: String): (Seq[Int], Seq[Int]) = {
    val Array(first, second) = input.split(',').map { case s"$lo-$hi" => lo.toInt to hi.toInt }
    (first, second)
  }

  def partOne(input: Seq[String]): Int = input
    .map(parse)
    .count(x => x._1.containsSlice(x._2) || x._2.containsSlice(x._1))

  def partTwo(input: Seq[String]): Int = input
    .map(parse)
    .count(x => x._1.intersect(x._2).nonEmpty)

  def main(args: Array[String]): Unit = {
    val input = io.Source.fromResource("Day04.txt").getLines().toSeq
    println(s"Part one: ${partOne(input)}")
    println(s"Part two: ${partTwo(input)}")
  }
}
