"""Advent of Code 2023 - Day 13."""

import sys
from typing import Optional, TextIO

from aoc_2023.parsers import parse_blocks


class Pattern:
    def __init__(self, text: str):
        self._grid = list(list(s) for s in text.splitlines())

    @property
    def height(self):
        return len(self._grid)

    @property
    def width(self):
        return len(self._grid[0])

    @property
    def rows(self) -> list[str]:
        return list("".join(row) for row in self._grid)

    @property
    def columns(self) -> list[str]:
        return list("".join(x) for x in zip(*self._grid))

    def flip(self, position: tuple[int, int]):
        x, y = position
        self._grid[y][x] = "#" if self._grid[y][x] == "." else "."


def find_reflection_point(pattern: Pattern, skip: tuple[Optional[int], Optional[int]] = (None, None)):
    skip_col, skip_row = skip

    def find(data: list[str], skip_i: Optional[int] = None):
        for i in range(len(data) - 1):
            if data[i] == data[i + 1] and i + 1 != skip_i:
                left, right = i, i + 1
                while left >= 0 and right < len(data) and data[left] == data[right]:
                    left -= 1
                    right += 1
                if left < 0 or right == len(data):
                    return i + 1
        return None

    return find(pattern.columns, skip_col), find(pattern.rows, skip_row)


def find_with_smudge(pattern: Pattern):
    original = find_reflection_point(pattern)
    for x in range(pattern.width):
        for y in range(pattern.height):
            pattern.flip((x, y))
            point = find_reflection_point(pattern, skip=original)
            if point != (None, None):
                return point
            pattern.flip((x, y))
    return None, None


def total(results: list[tuple[Optional[int], Optional[int]]]):
    cols = 0
    rows = 0
    for c, r in results:
        if c:
            cols += c
        if r:
            rows += r
    return cols + 100 * rows


def part_one(file: TextIO) -> int:
    patterns = list(map(Pattern, parse_blocks(file.read())))
    return total(list(map(find_reflection_point, patterns)))


def part_two(file: TextIO) -> int:
    patterns = list(map(Pattern, parse_blocks(file.read())))
    return total(list(map(find_with_smudge, patterns)))


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
