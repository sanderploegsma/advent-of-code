"""
Advent of Code 2023, Day 18: Lavaduct Lagoon.
"""

import sys
from itertools import pairwise
from typing import TextIO

from aoc_2023.grid import NORTH, EAST, SOUTH, WEST


def calculate_area(directions: list[complex]):
    """
    Calculate the area covered by digging a trench following the given directions.

    :param directions: List of directions to dig.
    :return: The area of the trench in cubic meters.
    """
    trench = [0]
    for direction in directions:
        trench.append(trench[-1] + direction)

    area_edges = sum(abs(d.real + d.imag) for d in directions)
    shoelace_area = (
        abs(sum(p1.real * p2.imag - p2.real * p1.imag for p1, p2 in pairwise(trench)))
        // 2
    )
    area_interior = shoelace_area - area_edges // 2 + 1
    return int(area_interior + area_edges)


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """

    def parse(line: str) -> complex:
        directions = {"U": NORTH, "D": SOUTH, "L": WEST, "R": EAST}

        direction, steps, _ = line.split()
        return directions[direction] * int(steps)

    return calculate_area(list(map(parse, file)))


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """

    def parse(line: str) -> complex:
        directions = {"0": EAST, "1": SOUTH, "2": WEST, "3": NORTH}

        line = line.strip()
        steps, direction = line[-7:-2], line[-2]
        return directions[direction] * int("0x" + steps, base=16)

    return calculate_area(list(map(parse, file)))


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
