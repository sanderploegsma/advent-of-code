rootProject.name = "advent-of-code"

include("aoc-common")
project(":aoc-common").projectDir = file("common/kotlin")

include("aoc-2015")
project(":aoc-2015").projectDir = file("2015/kotlin")

include("aoc-2019")
project(":aoc-2019").projectDir = file("2019/kotlin")

include("aoc-2021")
project(":aoc-2021").projectDir = file("2021/kotlin")
