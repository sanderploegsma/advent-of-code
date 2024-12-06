"""
Advent of Code 2024, Day 6: Guard Gallivant.

See: https://adventofcode.com/2024/day/6
"""

import sys
from typing import TextIO

from grid import Grid, NORTH, rotate_cw


def traverse(grid: Grid, start: complex) -> tuple[set[complex], bool]:
    """
    Traverse the grid from the given starting position.
    Returns when the end of the map is reached or a loop occurs.

    :param grid: Grid to traverse
    :param start: Position to start traversing from
    :return: Tuple containing the positions visited and a value
             indicating whether a loop has occurred.
    """
    direction = NORTH
    position = start
    path = set()
    while position in grid and (position, direction) not in path:
        path.add((position, direction))
        if grid.get(position + direction) == "#":
            direction = rotate_cw(direction)
        else:
            position += direction

    return {p for p, _ in path}, (position, direction) in path


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    grid = Grid.from_ascii_grid(file.read())
    start = next(p for p in grid if grid[p] == "^")
    path, _ = traverse(grid, start)
    return len(path)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    grid = Grid.from_ascii_grid(file.read())
    start = next(p for p in grid if grid[p] == "^")
    path, _ = traverse(grid, start)

    return sum(traverse(grid | {p: "#"}, start)[1] for p in path if p != start)


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
