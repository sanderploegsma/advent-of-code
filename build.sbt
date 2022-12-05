name := "AdventOfCode"
scalaVersion := "3.2.1"

lazy val aoc2022 = project in file("2022/scala")
lazy val root = (project in file(".")).aggregate(aoc2022)
