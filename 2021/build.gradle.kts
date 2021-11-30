@file:Suppress("SpellCheckingInspection")

import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

val junitVersion by extra {
    buildscript.configurations["classpath"]
        .resolvedConfiguration.firstLevelModuleDependencies
        .find { it.moduleName == "junit-jupiter-api" }?.moduleVersion
}

plugins {
    kotlin("jvm") version "1.6.0"
}

group = "nl.sanderp.aoc"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()
}

dependencies {
    @Suppress("GradlePackageUpdate")
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

tasks.withType<KotlinCompile> {
    kotlinOptions.jvmTarget = "1.8"
}