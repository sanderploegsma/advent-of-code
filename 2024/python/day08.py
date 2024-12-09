"""
Advent of Code 2024, Day 8: Resonant Collinearity.

See: https://adventofcode.com/2024/day/8
"""

import sys
import itertools
from typing import TextIO

from grid import Grid


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    grid = Grid.from_ascii_grid(file.read())
    antinodes = set()
    for a, b in itertools.combinations((p for p in grid if grid[p] != "."), 2):
        if grid[a] != grid[b]:
            continue

        nodes = {(a - b) * -1 + b, (b - a) * -1 + a}
        antinodes |= {node for node in nodes if grid.is_in_bounds(node)}

    return len(antinodes)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    grid = Grid.from_ascii_grid(file.read())
    antinodes = set()
    for a, b in itertools.combinations((p for p in grid if grid[p] != "."), 2):
        if grid[a] != grid[b]:
            continue

        delta = b - a
        p = a
        while grid.is_in_bounds(p):
            antinodes.add(p)
            p += delta

        p = a
        while grid.is_in_bounds(p):
            antinodes.add(p)
            p -= delta

    print(Grid({p: "#" for p in antinodes}).draw())

    return len(antinodes)


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
