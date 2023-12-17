"""
Advent of Code 2023, Day 16: The Floor Will Be Lava.
"""

import sys
from collections import deque, defaultdict
from typing import TextIO

from aoc_2023.navigation import bounding_box, UP, DOWN, LEFT, RIGHT, XY
from aoc_2023.parsers import parse_grid


def energize(grid: dict[XY, str], start_pos: XY, start_dir: XY) -> int:
    """
    Energize the given grid by following all beams through it,
    starting with the given starting position and direction.

    :param grid: Grid containing mirrors and splitters.
    :param start_pos: Starting position of the first beam.
    :param start_dir: Starting direction of the first beam.
    :return: The number of energized tiles in the grid.
    """
    energized = set()
    seen = defaultdict(set)
    beams = deque([(start_pos, start_dir)])
    while beams:
        position, direction = beams.popleft()

        if position not in grid or position in seen[direction]:
            continue

        energized.add(position)
        seen[direction].add(position)

        def move(next_dir: XY):
            return position + next_dir, next_dir

        match grid[position]:
            case "-" if direction in [UP, DOWN]:
                beams.append(move(LEFT))
                beams.append(move(RIGHT))
            case "|" if direction in [LEFT, RIGHT]:
                beams.append(move(UP))
                beams.append(move(DOWN))
            case "/":
                beams.append(move(-direction.swapped))
            case "\\":
                beams.append(move(direction.swapped))
            case _:
                beams.append(move(direction))

    return len(energized)


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    grid = parse_grid(line.strip() for line in file)
    top_left, _ = bounding_box(grid.keys())
    return energize(grid, top_left, RIGHT)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    grid = parse_grid(line.strip() for line in file)
    (min_x, min_y), (max_x, max_y) = bounding_box(grid.keys())
    start_options = []
    for x in range(min_x, max_x + 1):
        start_options.append((XY(x, min_y), DOWN))
        start_options.append((XY(x, max_y), UP))

    for y in range(min_y, max_y + 1):
        start_options.append((XY(min_x, y), RIGHT))
        start_options.append((XY(max_x, y), LEFT))

    return max(energize(grid, start_pos, start_dir) for start_pos, start_dir in start_options)


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
