"""Advent of Code 2023 - Day 11."""

import sys
from itertools import combinations
from typing import TextIO

from aoc_2023.navigation import XY, manhattan_distance, bounding_box
from aoc_2023.parsers import parse_coordinates


def sum_distances(coordinates: set[XY], offset: int) -> int:
    top_left, bottom_right = bounding_box(coordinates)

    columns = []
    column_offset = 0
    for x in range(top_left.x, bottom_right.x + 1):
        if not any(c.x == x for c in coordinates):
            column_offset += offset - 1
        columns.append(column_offset)

    rows = []
    row_offset = 0
    for y in range(top_left.y, bottom_right.y + 1):
        if not any(c.y == y for c in coordinates):
            row_offset += offset - 1
        rows.append(row_offset)

    expanded = {c + XY(columns[c.x], rows[c.y]) for c in coordinates}
    return sum(manhattan_distance(p1, p2) for p1, p2 in combinations(expanded, 2))


def part_one(file: TextIO) -> int:
    data = list(line.strip() for line in file)
    coordinates = parse_coordinates(data)
    return sum_distances(coordinates, 2)


def part_two(file: TextIO, n: int) -> int:
    data = list(line.strip() for line in file)
    coordinates = parse_coordinates(data)
    return sum_distances(coordinates, n)


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file, 1000000))


if __name__ == "__main__":
    main()
