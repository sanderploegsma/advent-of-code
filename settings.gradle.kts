rootProject.name = "advent-of-code"

include("aoc-common")
project(":aoc-common").projectDir = file("common/kotlin")

include("aoc-2021")
project(":aoc-2021").projectDir = file("2021/kotlin")

include("aoc-2022")
project(":aoc-2022").projectDir = file("2022/kotlin")