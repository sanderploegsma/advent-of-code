plugins {
    kotlin("jvm") version "1.9.21"
}

val junitVersion by extra {
    buildscript.configurations["classpath"]
        .resolvedConfiguration.firstLevelModuleDependencies
        .find { it.moduleName == "junit-jupiter-api" }?.moduleVersion
}

group = "nl.sanderp.aoc"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()
}

dependencies {
    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-core:1.4.2")

    testImplementation(kotlin("test"))

    testImplementation("org.junit.jupiter", "junit-jupiter-params", junitVersion)
    testImplementation("org.amshove.kluent", "kluent", "1.68")
}

tasks.test {
    useJUnitPlatform()
    testLogging {
        events("passed", "skipped", "failed")
    }
}
