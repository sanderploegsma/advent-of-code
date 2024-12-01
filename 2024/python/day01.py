"""
Advent of Code 2024, Day 1: Historian Hysteria.

See: https://adventofcode.com/2024/day/1
"""

import sys
from collections import defaultdict
from typing import TextIO


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    left, right = [], []
    for line in file.readlines():
        a, b = line.split(maxsplit=1)
        left.append(int(a))
        right.append(int(b))

    left, right = sorted(left), sorted(right)

    return sum(map(lambda n: abs(n[0] - n[1]), zip(left, right)))


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    left, right = [], defaultdict(lambda: 0)

    for line in file.readlines():
        a, b = line.split(maxsplit=1)
        left.append(int(a))
        right[int(b)] += 1

    return sum(map(lambda n: n * right[n], left))


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
