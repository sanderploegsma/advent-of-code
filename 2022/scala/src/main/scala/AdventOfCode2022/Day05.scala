package AdventOfCode2022

object Day05 {
  private case class Instruction(quantity: Int, from: Int, to: Int)

  private def parseState(input: String): Seq[String] =
    input
      .split(System.lineSeparator())
      .map { x => (1 to x.length by 4).map(x).toArray }
      .transpose
      .map(_.mkString.trim)

  private def parseInstructions(input: String): Seq[Instruction] =
    input
      .split(System.lineSeparator())
      .map { case s"move $quantity from $from to $to" => Instruction(quantity.toInt, from.toInt - 1, to.toInt - 1) }

  private def parse(input: String): (Seq[String], Seq[Instruction]) =
    val Array(state, instructions) = input.split(System.lineSeparator() + System.lineSeparator())
    (parseState(state), parseInstructions(instructions))

  private def move(state: Seq[String], instructions: Seq[Instruction], operation: String => String): Seq[String] =
    instructions.foldLeft(state) { case (s, Instruction(quantity, from, to)) =>
      val (head, tail) = s(from).splitAt(quantity)
      s.updated(from, tail).updated(to, operation(head) + s(to))
    }

  def partOne(input: String): String =
    val (state, instructions) = parse(input)
    move(state, instructions, _.reverse).map(_.head).mkString

  def partTwo(input: String): String =
    val (state, instructions) = parse(input)
    move(state, instructions, identity).map(_.head).mkString

  def main(args: Array[String]): Unit = {
    val input = io.Source.fromResource("Day05.txt").mkString
    println(s"Part one: ${partOne(input)}")
    println(s"Part two: ${partTwo(input)}")
  }
}
