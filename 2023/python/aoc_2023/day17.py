"""
Advent of Code 2023, Day 17: Clumsy Crucible.
"""

import sys
from queue import PriorityQueue
from typing import TextIO


def minimize_heat_loss(
    grid: list[str], min_path_length: int, max_path_length: int
) -> int:
    """
    Finds the path through the grid that minimizes the total heat loss.

    :param grid: The grid to navigate
    :param min_path_length: The minimum distance to travel in one direction before being able to turn.
    :param max_path_length: The maximum distance to travel in one direction before having to turn.
    :return: The heat loss incurred by following the path
    """
    max_x, max_y = len(grid[0]) - 1, len(grid) - 1

    visited = set()
    todo = PriorityQueue()

    # cost, x, y, dx, dy, segment length
    todo.put((0, 0, 0, 1, 0, 1))
    todo.put((0, 0, 0, 0, 1, 1))

    while todo:
        cost, x, y, dx, dy, c = todo.get()

        if (x, y, dx, dy, c) in visited:
            continue

        visited.add((x, y, dx, dy, c))

        if x == max_x and y == max_y:
            return cost

        x += dx
        y += dy

        if not (0 <= x <= max_x and 0 <= y <= max_y):
            continue

        cost += int(grid[y][x])

        if c < max_path_length:
            todo.put((cost, x, y, dx, dy, c + 1))

        if c >= min_path_length:
            todo.put((cost, x, y, dy, dx, 1))
            todo.put((cost, x, y, -dy, -dx, 1))

    return -1


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    grid = list(line.strip() for line in file)
    return minimize_heat_loss(grid, min_path_length=1, max_path_length=3)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    grid = list(line.strip() for line in file)
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
