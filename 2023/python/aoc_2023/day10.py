"""Advent of Code 2023 - Day 10."""

import sys
from itertools import pairwise
from typing import TextIO

from aoc_2023.navigation import UP, DOWN, LEFT, RIGHT, XY
from aoc_2023.parsers import parse_grid

START_DIRECTIONS = {
    UP: "|F7",
    DOWN: "|JL",
    RIGHT: "-J7",
    LEFT: "-FL",
}

DIRECTIONS = {
    "-": {LEFT: LEFT, RIGHT: RIGHT},
    "|": {UP: UP, DOWN: DOWN},
    "L": {LEFT: UP, DOWN: RIGHT},
    "J": {RIGHT: UP, DOWN: LEFT},
    "7": {RIGHT: DOWN, UP: LEFT},
    "F": {UP: RIGHT, LEFT: DOWN},
}


def find_loop(grid: dict[XY, str]) -> list[XY]:
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
    grid = parse_grid(file)
    loop = find_loop(grid)
    return len(loop) // 2


def part_two(file: TextIO) -> int:
    grid = parse_grid(file)
    loop = find_loop(grid)
    area = abs(sum(x1 * y2 - x2 * y1 for (x1, y1), (x2, y2) in pairwise([*loop, loop[0]]))) // 2
    return area - len(loop) // 2 + 1


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
