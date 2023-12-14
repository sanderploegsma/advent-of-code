"""Advent of Code 2023 - Day 1."""

import string
import sys
from typing import TextIO

DIGIT_VALUES = {
    **{d: int(d) for d in string.digits},
    "one": 1,
    "two": 2,
    "three": 3,
    "four": 4,
    "five": 5,
    "six": 6,
    "seven": 7,
    "eight": 8,
    "nine": 9,
}


def part_one(file: TextIO) -> int:
    def parse(line: str) -> int:
        digits = [int(c) for c in line if c in string.digits]
        return 10 * digits[0] + digits[-1]

    return sum(map(parse, file))


def part_two(file: TextIO) -> int:
    def parse(line: str) -> int:
        first, first_idx = -1, len(line)
        last, last_idx = -1, -1

        for digit, value in DIGIT_VALUES.items():
            if -1 < line.find(digit) < first_idx:
                first_idx = line.find(digit)
                first = value

            if line.rfind(digit) > last_idx:
                last_idx = line.rfind(digit)
                last = value

        return 10 * first + last

    return sum(map(parse, file))


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
