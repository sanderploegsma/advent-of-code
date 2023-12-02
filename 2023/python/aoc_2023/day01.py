"""Advent of Code 2023 - Day 1."""
import string

from aoc_2023.input import Input

input = Input("01.txt")


def parse_part_one(line: str) -> int:
    digits = [int(c) for c in line if c in string.digits]
    return 10 * digits[0] + digits[-1]


print("Part one:", sum(parse_part_one(line) for line in input.lines))

digit_values = {
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


def parse_part_two(line: str) -> int:
    first, first_idx = -1, len(line)
    last, last_idx = -1, -1

    for digit, value in digit_values.items():
        if -1 < line.find(digit) < first_idx:
            first_idx = line.find(digit)
            first = value

        if line.rfind(digit) > last_idx:
            last_idx = line.rfind(digit)
            last = value

    return 10 * first + last


print("Part two:", sum(parse_part_two(line) for line in input.lines))
