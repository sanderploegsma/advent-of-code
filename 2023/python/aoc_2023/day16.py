"""
Advent of Code 2023, Day 16: The Floor Will Be Lava.
"""

import sys
from collections import deque, defaultdict
from typing import TextIO

Tile = tuple[int, int]
Direction = tuple[int, int]

UP = 0, -1
DOWN = 0, 1
LEFT = -1, 0
RIGHT = 1, 0


def energize(grid: list[str], start: Tile, start_dir: Direction) -> int:
    """
    Energize the given grid by following all beams through it,
    starting with the given starting position and direction.

    :param grid: Grid containing mirrors and splitters.
    :param start: Starting tile of the first beam.
    :param start_dir: Starting direction of the first beam.
    :return: The number of energized tiles in the grid.
    """

    def in_bounds(_tile: Tile):
        return 0 <= _tile[0] < len(grid[0]) and 0 <= _tile[1] < len(grid)

    energized = set()
    seen = defaultdict(set)
    beams = deque([(start, start_dir)])
    while beams:
        tile, direction = beams.popleft()

        if not in_bounds(tile) or tile in seen[direction]:
            continue

        energized.add(tile)
        seen[direction].add(tile)

        x, y = tile
        dx, dy = direction

        def move(next_dir: Direction):
            _dx, _dy = next_dir
            return (x + _dx, y + _dy), next_dir

        match grid[y][x]:
            case "-" if dx == 0:
                beams.append(move(LEFT))
                beams.append(move(RIGHT))
            case "|" if dy == 0:
                beams.append(move(UP))
                beams.append(move(DOWN))
            case "/":
                beams.append(move((-dy, -dx)))
            case "\\":
                beams.append(move((dy, dx)))
            case _:
                beams.append(move((dx, dy)))

    return len(energized)


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    grid = list(line.strip() for line in file)
    return energize(grid, (0, 0), RIGHT)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    grid = list(line.strip() for line in file)
    max_x = len(grid[0]) - 1
    max_y = len(grid)
    start_options = []
    for x in range(max_x + 1):
        start_options.append(((x, 0), DOWN))
        start_options.append(((x, max_y), UP))

    for y in range(max_y + 1):
        start_options.append(((0, y), RIGHT))
        start_options.append(((max_x, y), LEFT))

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
