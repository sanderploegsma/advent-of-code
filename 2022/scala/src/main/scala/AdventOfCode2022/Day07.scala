package AdventOfCode2022

import scala.annotation.tailrec

object Day07 {
  private def parse(input: Seq[String]): Map[List[String], Int] =
    val (_, fileSizes) = input.foldLeft((List("/"), Map[List[String], Int]())) { case ((currentDir, sizes), line) =>
      line match {
        case "$ cd /" => (List("/"), sizes)
        case "$ cd .." => (currentDir.tail, sizes)
        case "$ ls" => (currentDir, sizes)
        case s"dir $_" => (currentDir, sizes)
        case s"$$ cd $newDir" => (newDir :: currentDir, sizes)
        case s"$fileSize $fileName" => (currentDir, sizes.updated(fileName :: currentDir, fileSize.toInt))
      }
    }

    fileSizes
      .toSeq
      .flatMap { case (_ :: path, size) => tree(path).map(p => (p, size)) }
      .groupMapReduce(_._1)(_._2)(_ + _)

  private def tree(path: List[String]): List[List[String]] = path match {
    case Nil => List()
    case _ :: tail => path :: tree(tail)
  }

  def partOne(input: Seq[String]): Int =
    parse(input)
      .values
      .filter(_ <= 100000)
      .sum

  def partTwo(input: Seq[String]): Int =
    val sizes = parse(input)
    val requiredSpace = 30000000 - (70000000 - sizes(List("/")))

    sizes
      .values
      .filter(_ >= requiredSpace)
      .min

  def main(args: Array[String]): Unit = {
    val input = io.Source.fromResource("Day07.txt").getLines().toSeq
    println(s"Part one: ${partOne(input)}")
    println(s"Part two: ${partTwo(input)}")
  }
}
