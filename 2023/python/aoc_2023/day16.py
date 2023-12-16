"""Advent of Code 2023, Day 16: The Floor Will Be Lava."""

import sys
from collections import deque
from typing import TextIO

from aoc_2023.navigation import bounding_box, UP, DOWN, LEFT, RIGHT, XY
from aoc_2023.parsers import parse_grid

DIRECTIONS = {
    ".": {UP: [UP], DOWN: [DOWN], LEFT: [LEFT], RIGHT: [RIGHT]},
    "-": {UP: [LEFT, RIGHT], DOWN: [LEFT, RIGHT], LEFT: [LEFT], RIGHT: [RIGHT]},
    "|": {UP: [UP], DOWN: [DOWN], LEFT: [UP, DOWN], RIGHT: [UP, DOWN]},
    "/": {LEFT: [DOWN], DOWN: [LEFT], RIGHT: [UP], UP: [RIGHT]},
    "\\": {RIGHT: [DOWN], DOWN: [RIGHT], LEFT: [UP], UP: [LEFT]},
}


def energize(grid: dict[XY, str], start_pos: XY, start_dir: XY) -> int:
    (min_x, min_y), (max_x, max_y) = bounding_box(grid.keys())
    energized = set()
    seen = {UP: set(), DOWN: set(), LEFT: set(), RIGHT: set()}
    beams = deque([(start_pos, start_dir)])
    while len(beams) > 0:
        position, direction = beams.popleft()
        next_position = position + direction
        if min_x <= next_position.x <= max_x and min_y <= next_position.y <= max_y:
            energized.add(next_position)
            for next_direction in DIRECTIONS[grid[next_position]][direction]:
                if next_position not in seen[next_direction]:
                    seen[next_direction].add(next_position)
                    beams.append((next_position, next_direction))

    return len(energized)


def part_one(file: TextIO) -> int:
    """Solve part one of the puzzle."""
    grid = parse_grid(line.strip() for line in file)
    top_left, _ = bounding_box(grid.keys())
    return energize(grid, top_left + XY(-1, 0), RIGHT)


def part_two(file: TextIO) -> int:
    """Solve part two of the puzzle."""
    grid = parse_grid(line.strip() for line in file)
    (min_x, min_y), (max_x, max_y) = bounding_box(grid.keys())
    start_options = []
    for x in range(min_x, max_x + 1):
        start_options.append((XY(x, min_y - 1), DOWN))
        start_options.append((XY(x, max_y + 1), UP))

    for y in range(min_y, max_y + 1):
        start_options.append((XY(min_x - 1, y), RIGHT))
        start_options.append((XY(max_x + 1, y), LEFT))

    return max(energize(grid, start_pos, start_dir) for start_pos, start_dir in start_options)


def main():
    """The main entrypoint for the script."""
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
