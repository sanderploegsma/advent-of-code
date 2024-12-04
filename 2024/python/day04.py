"""
Advent of Code 2024, Day 4: Ceres Search.

See: https://adventofcode.com/2024/day/4
"""

import sys
from typing import TextIO

from grid import Grid, NEIGHBORS_8, NORTH_EAST, NORTH_WEST, SOUTH_EAST, SOUTH_WEST


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    grid = Grid.from_ascii_grid(file.readlines())
    count = 0
    for p, value in grid.items():
        if value != "X":
            continue

        for d in NEIGHBORS_8:
            if (
                grid.get(p + d),
                grid.get(p + 2 * d),
                grid.get(p + 3 * d),
            ) == ("M", "A", "S"):
                count += 1

    return count


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    grid = Grid.from_ascii_grid(file.readlines())
    count = 0
    for p, value in grid.items():
        if value != "A":
            continue

        if sorted(
            [
                grid.get(p + NORTH_EAST, "Z"),
                grid.get(p + SOUTH_WEST, "Z"),
            ]
        ) != ["M", "S"]:
            continue

        if sorted(
            [
                grid.get(p + SOUTH_EAST, "Z"),
                grid.get(p + NORTH_WEST, "Z"),
            ]
        ) != ["M", "S"]:
            continue

        count += 1

    return count


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
