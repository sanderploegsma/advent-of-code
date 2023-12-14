"""Advent of Code 2023 - Day 03."""

import math
import re
import string
import sys
from typing import TextIO


def is_symbol(s: str) -> bool:
    return s not in string.digits and s != "."


def is_gear(s: str) -> bool:
    return s == "*"


def solve(rows: list[str]) -> tuple[list[int], list[tuple[int, int]]]:
    def get_neighbours(row_nr: int, col_nr_start: int, col_nr_end: int) -> list[tuple[int, int]]:
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

    part_numbers = []
    part_numbers_per_gear: dict[tuple[int, int], list[int]] = {}

    num_rows, num_cols = len(rows), len(rows[0])

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

    return part_numbers, list((xs[0], xs[1]) for xs in part_numbers_per_gear.values() if len(xs) == 2)


def part_one(file: TextIO) -> int:
    rows = list(line.strip() for line in file)
    part_numbers, _ = solve(rows)
    return sum(part_numbers)


def part_two(file: TextIO) -> int:
    rows = list(line.strip() for line in file)
    _, part_numbers = solve(rows)
    return sum(map(math.prod, part_numbers))


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
