package AdventOfCode2022

import scala.annotation.tailrec

object Day12 {
  private val StartMarker: Char = 'S'
  private val EndMarker: Char = 'E'

  private case class Point(x: Int, y: Int) {
    def neighbours: Seq[Point] = Seq(Point(x + 1, y), Point(x - 1, y), Point(x, y + 1), Point(x, y - 1))
  }

  private def parse(input: Seq[String]): Map[Point, Char] =
    val points = for {
      y <- input.indices
      x <- input.head.indices
    } yield (Point(x, y), input(y)(x))

    points.toMap

  private def minDistance(to: Char, heights: Map[Point, Char]): Int = {
    def height(p: Point): Int = heights(p) match {
      case StartMarker => 0
      case EndMarker => 'z' - 'a'
      case h => h - 'a'
    }

    @tailrec
    def traverse(queue: List[Point], state: Map[Point, Int]): Map[Point, Int] = queue match
      case Nil => state
      case p :: tail =>
        val neighbours = p.neighbours
          .filter(heights.contains)
          .filter(!state.contains(_))
          .filter { n => height(p) - height(n) <= 1 }

        traverse(tail ++ neighbours, state ++ neighbours.map((_, state(p) + 1)).toMap)

    val start = heights.map(_.swap)(EndMarker)
    val distances = traverse(List(start), Map((start, 0)))
    distances.filter { x => heights(x._1) == to }.values.min
  }

  def partOne(input: Seq[String]): Int = minDistance(StartMarker, parse(input))

  def partTwo(input: Seq[String]): Int = minDistance('a', parse(input))

  def main(args: Array[String]): Unit = {
    val input = io.Source.fromResource("Day12.txt").getLines().toSeq
    println(s"Part one: ${partOne(input)}")
    println(s"Part two: ${partTwo(input)}")
  }
}
