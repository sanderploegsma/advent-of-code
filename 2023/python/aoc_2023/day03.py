"""Advent of Code 2023 - Day 3."""

import math
import re
import string

from aoc_2023.input import Input

rows = Input("03.txt").lines
num_rows, num_cols = len(rows), len(rows[0])


def get_neighbours(
    row_nr: int, col_nr_start: int, col_nr_end: int
) -> list[tuple[int, int]]:
    left = max(0, col_nr_start - 1)
    right = min(num_cols - 1, col_nr_end + 1)

    result: list[tuple[int, int]] = []

    if row_nr > 0:
        for col_nr in range(left, right + 1):
            result.append((row_nr - 1, col_nr))

    if col_nr_start > 0:
        result.append((row_nr, col_nr_start - 1))

    if col_nr_end < num_cols - 1:
        result.append((row_nr, col_nr_end + 1))

    if row_nr < num_rows - 1:
        for col_nr in range(left, right + 1):
            result.append((row_nr + 1, col_nr))

    return result


def is_symbol(s: str) -> bool:
    return s not in string.digits and s != "."


def is_gear(s: str) -> bool:
    return s == "*"


part_numbers = []
part_numbers_per_gear: dict[tuple[int, int], list[int]] = dict()

for row_nr, row in enumerate(rows):
    for m in re.finditer("(\\d+)", row):
        number = int(m[0])
        neighbours = get_neighbours(row_nr, m.start(), m.end() - 1)

        if any(is_symbol(rows[r][c]) for r, c in neighbours):
            part_numbers.append(number)

        for cell in neighbours:
            r, c = cell
            if is_gear(rows[r][c]):
                if cell not in part_numbers_per_gear:
                    part_numbers_per_gear[cell] = [number]
                else:
                    part_numbers_per_gear[cell].append(number)

print("Part one:", sum(part_numbers))

print(
    "Part two:",
    sum(math.prod(xs) for xs in part_numbers_per_gear.values() if len(xs) == 2),
)
