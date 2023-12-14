"""Advent of Code 2023 - Day 14."""

import sys
from functools import reduce
from typing import TextIO

from aoc_2023.navigation import UP, DOWN, LEFT, RIGHT, XY, bounding_box
from aoc_2023.parsers import parse_coordinates

RoundRocks = set[XY]
CubeRocks = set[XY]
DishPlatform = tuple[RoundRocks, CubeRocks]

CYCLES = 1_000_000_000


def tilt(platform: DishPlatform, direction: XY) -> DishPlatform:
    round_rocks, cube_rocks = platform
    (min_x, min_y), (max_x, max_y) = bounding_box(round_rocks | cube_rocks)

    tilted = set(cube_rocks)
    for rock in sorted(round_rocks,
                       key=lambda r: r.x if direction in [LEFT, RIGHT] else r.y,
                       reverse=direction in [DOWN, RIGHT]):
        while (pos := rock + direction) not in tilted and min_x <= pos.x <= max_x and min_y <= pos.y <= max_y:
            rock = pos

        tilted.add(rock)

    return tilted - cube_rocks, cube_rocks


def tilt_cycle(platform: DishPlatform) -> DishPlatform:
    return reduce(tilt, [UP, LEFT, DOWN, RIGHT], platform)


def north_beam_load(platform: DishPlatform) -> int:
    round_rocks, cube_rocks = platform
    _, (_, max_y) = bounding_box(round_rocks | cube_rocks)

    return sum(max_y - y + 1 for _, y in round_rocks)


def part_one(file: TextIO) -> int:
    data = list(line.strip() for line in file)
    platform = parse_coordinates(data, "O"), parse_coordinates(data, "#")
    platform = tilt(platform, UP)
    return north_beam_load(platform)


def part_two(file: TextIO) -> int:
    data = list(line.strip() for line in file)
    platform = parse_coordinates(data, "O"), parse_coordinates(data, "#")

    cache = {}
    for step in range(CYCLES):
        platform = tilt_cycle(platform)

        key = frozenset(platform[0])
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
