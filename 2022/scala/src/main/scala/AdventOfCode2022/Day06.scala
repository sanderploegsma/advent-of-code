package AdventOfCode2022

object Day06 {
  private def findMarker(input: String, markerSize: Int): Int =
    val startOfMarker = input
      .sliding(markerSize)
      .indexWhere(_.toSet.size == markerSize)

    startOfMarker + markerSize

  def partOne(input: String): Int = findMarker(input, 4)

  def partTwo(input: String): Int = findMarker(input, 14)

  def main(args: Array[String]): Unit = {
    val input = io.Source.fromResource("Day06.txt").mkString
    println(s"Part one: ${partOne(input)}")
    println(s"Part two: ${partTwo(input)}")
  }
}
