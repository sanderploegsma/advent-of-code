"""
Advent of Code 2023, Day 11: Cosmic Expansion.
"""

import sys
from itertools import combinations
from typing import TextIO

from aoc_2023.grid import Grid


def sum_distances(grid: Grid, offset: int) -> int:
    columns = []
    column_offset = 0
    for x in range(grid.width):
        if not any(p.real == x for p in grid.keys()):
            column_offset += offset - 1
        columns.append(column_offset)

    rows = []
    row_offset = 0
    for y in range(grid.height):
        if not any(p.imag == y for p in grid.keys()):
            row_offset += offset - 1
        rows.append(row_offset)

    expanded = {(p.real + columns[int(p.real)], p.imag + rows[int(p.imag)]) for p in grid.keys()}
    return sum(abs(x1 - x2) + abs(y1 - y2) for (x1, y1), (x2, y2) in combinations(expanded, 2))


def part_one(file: TextIO) -> int:
    grid = Grid.from_ascii_grid(file)
    return sum_distances(grid, 2)


def part_two(file: TextIO, n: int) -> int:
    grid = Grid.from_ascii_grid(file)
    return sum_distances(grid, n)


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file, 1000000))


if __name__ == "__main__":
    main()
