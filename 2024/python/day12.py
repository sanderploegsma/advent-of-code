"""
Advent of Code 2024, Day 12: Garden Groups.

See: https://adventofcode.com/2024/day/12
"""

import sys
from typing import TextIO
from queue import SimpleQueue

from grid import Grid, NORTH, EAST, SOUTH, WEST


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    grid = Grid.from_ascii_grid(file.read())
    regions, perimeters = [], {}
    queue = SimpleQueue()
    for p in grid:
        if p in perimeters:
            continue

        region = []
        queue.put(p)
        while not queue.empty():
            p_ = queue.get()
            if p_ in perimeters:
                continue

            region.append(p_)
            perimeters[p_] = 4
            for n in grid.get_neighbors(p_):
                if grid[n] == grid[p_]:
                    perimeters[p_] -= 1
                    queue.put(n)

        regions.append(region)

    return sum(len(r) * sum(perimeters[p] for p in r) for r in regions)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    grid = Grid.from_ascii_grid(file.read())
    regions, corners = [], {}
    queue = SimpleQueue()
    for p in grid:
        if p in corners:
            continue

        region = set()
        queue.put(p)
        while not queue.empty():
            p_ = queue.get()
            if p_ in corners:
                continue

            region.add(p_)
            v = grid[p_]
            corners[p_] = 0
            for d1, d2 in [(NORTH, EAST), (NORTH, WEST), (SOUTH, EAST), (SOUTH, WEST)]:
                # "Outer" corners
                if grid.get(p_ + d1, None) != v and grid.get(p_ + d2, None) != v:
                    corners[p_] += 1

                # "Inner" corners
                if (
                    grid.get(p_ + d1, None) == v
                    and grid.get(p_ + d2, None) == v
                    and grid.get(p_ + d1 + d2, None) != v
                ):
                    corners[p_] += 1

            for n in grid.get_neighbors(p_):
                if grid[n] == grid[p_]:
                    queue.put(n)

        regions.append(region)

    return sum(len(r) * sum(corners[p] for p in r) for r in regions)


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
