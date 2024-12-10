"""
Advent of Code 2024, Day 10: Hoof It.

See: https://adventofcode.com/2024/day/10
"""

import sys
import queue
from typing import TextIO

from grid import Grid


def part_one(file: TextIO) -> int:
    """
    For each trailhead, find the number of 9-height positions that can be
    reached.
    """
    grid = Grid.from_int_grid(file.read())

    def score(start: complex):
        found = set()
        q = queue.SimpleQueue()
        q.put(start)

        while not q.empty():
            p = q.get()
            v = grid[p]
            if v == 9:
                found.add(p)
                continue

            for p_ in grid.get_neighbors(p):
                if grid[p_] == v + 1:
                    q.put(p_)

        return len(found)

    return sum(score(p) for p in grid if grid[p] == 0)


def part_two(file: TextIO) -> int:
    """
    For each trailhead, find the number of paths that lead to a 9-height
    position.
    """
    grid = Grid.from_int_grid(file.read())

    def score(p: complex):
        v = grid[p]
        if v == 9:
            return 1

        candidates = set(p_ for p_ in grid.get_neighbors(p) if grid[p_] == v + 1)
        if len(candidates) == 0:
            return 0

        return sum(map(score, candidates))

    return sum(score(p) for p in grid if grid[p] == 0)


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
