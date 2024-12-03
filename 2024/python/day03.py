"""
Advent of Code 2024, Day 3: Mull It Over.

See: https://adventofcode.com/2024/day/3
"""

import re
import sys
from typing import TextIO

pattern_to_find = re.compile(r"mul\((\d+),(\d+)\)")
pattern_to_remove = re.compile(r"don't\(\)[\s\S]*?do\(\)")


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    instructions = file.read()
    matches = pattern_to_find.findall(instructions)
    return sum(int(a) * int(b) for a, b in matches)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    instructions = pattern_to_remove.sub("", file.read())
    matches = pattern_to_find.findall(instructions)
    return sum(int(a) * int(b) for a, b in matches)


def main():
    """
    The main entrypoint for the script.
    """
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
