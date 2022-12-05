package AdventOfCode2022

import scala.collection.MapView

object Day03 {
  private lazy val priorities: Map[Char, Int] = (('a' to 'z') ++ ('A' to 'Z')).zipWithIndex.toMap.transform((_, v) => v + 1)

  def partOne(input: Seq[String]): Int = input
    .map(s => s.substring(0, s.length / 2).intersect(s.substring(s.length / 2)))
    .filter(_.nonEmpty)
    .map(_.head)
    .map(priorities)
    .sum

  def partTwo(input: Seq[String]): Int = input
    .grouped(3)
    .map { case Seq(a, b, c) => a.intersect(b).intersect(c) }
    .filter(_.nonEmpty)
    .map(_.head)
    .map(priorities)
    .sum

  def main(args: Array[String]): Unit = {
    val input = io.Source.fromResource("Day03.txt").getLines().toSeq
    println(s"Part one: ${partOne(input)}")
    println(s"Part two: ${partTwo(input)}")
  }
}
