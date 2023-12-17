"""
Advent of Code 2023, Day 10: Pipe Maze.
"""

import sys
from itertools import pairwise
from typing import TextIO

from aoc_2023.grid import Grid, NORTH, EAST, SOUTH, WEST

START_DIRECTIONS = {
    NORTH: "|F7",
    SOUTH: "|JL",
    EAST: "-J7",
    WEST: "-FL",
}

DIRECTIONS = {
    "-": {WEST: WEST, EAST: EAST},
    "|": {NORTH: NORTH, SOUTH: SOUTH},
    "L": {WEST: NORTH, SOUTH: EAST},
    "J": {EAST: NORTH, SOUTH: WEST},
    "7": {EAST: SOUTH, NORTH: WEST},
    "F": {NORTH: EAST, WEST: SOUTH},
}


def find_loop(grid: Grid) -> list[complex]:
    start = next(coord for coord, cell in grid.items() if cell == "S")
    direction = next(d for d, s in START_DIRECTIONS.items() if grid.get(start + d, "?") in s)

    position = start + direction
    loop = [start]
    while position != start:
        loop.append(position)
        symbol = grid[position]
        direction = DIRECTIONS[symbol][direction]
        position = position + direction

    return loop


def part_one(file: TextIO) -> int:
    grid = Grid.from_ascii_grid(file)
    loop = find_loop(grid)
    return len(loop) // 2


def part_two(file: TextIO) -> int:
    grid = Grid.from_ascii_grid(file)
    loop = find_loop(grid)
    area = abs(sum(p1.real * p2.imag - p2.real * p1.imag for p1, p2 in pairwise([*loop, loop[0]]))) // 2
    return area - len(loop) // 2 + 1


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
