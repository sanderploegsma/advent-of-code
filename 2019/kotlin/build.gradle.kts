plugins {
    id("advent-of-code.kotlin-conventions")
}

dependencies {
    api(project(":aoc-common"))

    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-core:1.4.2")
}