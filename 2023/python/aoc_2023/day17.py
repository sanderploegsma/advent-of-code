"""
Advent of Code 2023, Day 17: Clumsy Crucible.
"""

import sys
from queue import PriorityQueue
from typing import TextIO

from aoc_2023.navigation import XY, bounding_box, DOWN, RIGHT
from aoc_2023.parsers import parse_grid


def minimize_heat_loss(grid: dict[XY, str], min_path_length: int, max_path_length: int) -> int:
    """
    Finds the path through the grid that minimizes the total heat loss.

    :param grid: The grid to navigate
    :param min_path_length: The minimum distance to travel in one direction before being able to turn.
    :param max_path_length: The maximum distance to travel in one direction before having to turn.
    :return: The heat loss incurred by following the path
    """
    top_left, bottom_right = bounding_box(grid)

    visited = set()
    todo = PriorityQueue()
    todo.put((0, top_left, RIGHT, 1))
    todo.put((0, top_left, DOWN, 1))

    while todo.not_empty:
        cost, position, direction, length = todo.get()

        if (position, direction, length) in visited:
            continue

        visited.add((position, direction, length))

        if position == bottom_right:
            return cost

        position += direction

        if position not in grid:
            continue

        cost += int(grid[position])

        if length < max_path_length:
            todo.put((cost, position, direction, length + 1))

        if length >= min_path_length:
            todo.put((cost, position, direction.swapped, 1))
            todo.put((cost, position, -direction.swapped, 1))

    return -1


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    grid = parse_grid(line.strip() for line in file)
    return minimize_heat_loss(grid, min_path_length=1, max_path_length=3)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    grid = parse_grid(line.strip() for line in file)
    return minimize_heat_loss(grid, min_path_length=4, max_path_length=10)


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
