package nl.sanderp.aoc.common

import java.io.IOException

internal object Resources

/**
 * Reads a file from the resources.
 * @param fileName the name of the file, relative to the resources root
 * @return the contents of the resource
 */
fun readResource(fileName: String) = Resources.javaClass.getResource("/$fileName")?.readText()?.trim()
    ?: throw IOException("File does not exist: $fileName")