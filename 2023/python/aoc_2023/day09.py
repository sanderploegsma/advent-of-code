"""Advent of Code 2023 - Day 09."""

import sys
from operator import itemgetter
from typing import TextIO

from aoc_2023.parsers import parse_int_lists


def extrapolate(series: list[int]) -> tuple[int, int]:
    if all(n == 0 for n in series):
        return 0, 0

    diffs = list(series[i] - series[i - 1] for i in range(1, len(series)))
    diff_left, diff_right = extrapolate(diffs)
    return series[0] - diff_left, series[-1] + diff_right


def part_one(file: TextIO) -> int:
    histories = parse_int_lists(file)
    return sum(map(itemgetter(1), map(extrapolate, histories)))


def part_two(file: TextIO) -> int:
    histories = parse_int_lists(file)
    return sum(map(itemgetter(0), map(extrapolate, histories)))


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
