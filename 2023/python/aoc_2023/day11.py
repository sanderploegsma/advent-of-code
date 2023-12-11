"""Advent of Code 2023 - Day 11."""

from itertools import combinations

from aoc_2023.input import Input
from aoc_2023.navigation import XY, manhattan_distance, bounding_box

coordinates = list(k for k, v in Input("11.txt").grid.items() if v == "#")
top_left, bottom_right = bounding_box(coordinates)


def sum_distances(offset: int) -> int:
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


print("Part one:", sum_distances(2))
print("Part two:", sum_distances(1000000))
