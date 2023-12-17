"""
Advent of Code 2023, Day 16: The Floor Will Be Lava.
"""

import sys
from collections import deque, defaultdict
from typing import TextIO

from aoc_2023.grid import Grid, NORTH, EAST, SOUTH, WEST


def energize(grid: Grid, start: complex, start_dir: complex) -> int:
    """
    Energize the given grid by following all beams through it,
    starting with the given starting position and direction.

    :param grid: Grid containing mirrors and splitters.
    :param start: Starting tile of the first beam.
    :param start_dir: Starting direction of the first beam.
    :return: The number of energized tiles in the grid.
    """

    def in_bounds(_tile: complex):
        return 0 <= _tile.real < grid.width and 0 <= _tile.imag < grid.height

    energized = set()
    seen = defaultdict(set)
    beams = deque([(start, start_dir)])
    while beams:
        tile, direction = beams.popleft()

        if tile not in grid or tile in seen[direction]:
            continue

        energized.add(tile)
        seen[direction].add(tile)

        def move(next_dir: complex):
            return tile + next_dir, next_dir

        match grid[tile]:
            case "-" if direction.real == 0:
                beams.append(move(EAST))
                beams.append(move(WEST))
            case "|" if direction.imag == 0:
                beams.append(move(NORTH))
                beams.append(move(SOUTH))
            case "/":
                beams.append(move(complex(-direction.imag, -direction.real)))
            case "\\":
                beams.append(move(complex(direction.imag, direction.real)))
            case _:
                beams.append(move(direction))

    return len(energized)


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    grid = Grid.from_ascii_grid((line.strip() for line in file), ignore_chars="")
    return energize(grid, complex(0, 0), EAST)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    grid = Grid.from_ascii_grid((line.strip() for line in file), ignore_chars="")
    start_options = []
    for x in range(grid.width):
        start_options.append((complex(x, 0), SOUTH))
        start_options.append((complex(x, grid.height - 1), NORTH))

    for y in range(grid.height):
        start_options.append((complex(0, y), EAST))
        start_options.append((complex(grid.width - 1, y), WEST))

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
