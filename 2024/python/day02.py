"""
Advent of Code 2024, Day 2: Red-Nosed Reports.

See: https://adventofcode.com/2024/day/2
"""

import sys
from itertools import pairwise
from typing import TextIO


def parse(file: TextIO):
    for line in file.readlines():
        yield list(map(int, line.split()))


def is_valid(levels: list[int]):
    return (sorted(levels) == levels or sorted(levels, reverse=True) == levels) and all(
        1 <= abs(a - b) <= 3 for a, b in pairwise(levels)
    )


def problem_dampener(levels: list[int]):
    for i in range(len(levels)):
        yield levels[:i] + levels[i + 1 :]


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    valid = list(levels for levels in parse(file) if is_valid(levels))
    return len(valid)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    valid = list(
        levels
        for levels in parse(file)
        if any(is_valid(variant) for variant in problem_dampener(levels))
    )
    return len(valid)


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
