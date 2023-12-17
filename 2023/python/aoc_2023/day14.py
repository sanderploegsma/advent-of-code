"""
Advent of Code 2023, Day 14: Parabolic Reflector Dish.
"""

import sys
from functools import reduce
from typing import TextIO

from aoc_2023.grid import Grid, NORTH, WEST, SOUTH, EAST

ROUND_ROCK = "O"
CUBE_ROCK = "#"

CYCLES = 1_000_000_000


def tilt(grid: Grid, direction: complex) -> Grid:
    tilted = Grid({p: v for p, v in grid.items() if v == CUBE_ROCK})

    for p in sorted((p for p, v in grid.items() if v == ROUND_ROCK),
                    key=lambda pos: pos.real if direction in [EAST, WEST] else pos.imag,
                    reverse=direction in [SOUTH, EAST]):
        while ((next_pos := p + direction) not in tilted and
               0 <= next_pos.real < grid.width and
               0 <= next_pos.imag < grid.height):
            p = next_pos

        tilted[p] = ROUND_ROCK

    return tilted


def tilt_cycle(grid: Grid) -> Grid:
    return reduce(tilt, [NORTH, WEST, SOUTH, EAST], grid)


def north_beam_load(grid: Grid) -> int:
    return int(sum(grid.height - p.imag for p, v in grid.items() if v == ROUND_ROCK))


def part_one(file: TextIO) -> int:
    platform = Grid.from_ascii_grid(file)
    platform = tilt(platform, NORTH)
    return north_beam_load(platform)


def part_two(file: TextIO) -> int:
    platform = Grid.from_ascii_grid(file)

    cache = {}
    for step in range(CYCLES):
        platform = tilt_cycle(platform)

        key = frozenset(p for p, v in platform.items() if v == ROUND_ROCK)
        if key in cache:
            loop_length = step - cache[key]
            steps_remaining = (CYCLES - step - 1) % loop_length
            for _ in range(steps_remaining):
                platform = tilt_cycle(platform)
            return north_beam_load(platform)

        cache[key] = step

    return north_beam_load(platform)


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
