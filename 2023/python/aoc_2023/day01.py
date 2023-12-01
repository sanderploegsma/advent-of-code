"""Advent of Code 2023 - Day 1."""
import re
import string

with open("2023/input/01.txt", encoding="utf-8") as f:
    input = [line.strip() for line in f.readlines()]


def parse_part_one(line: str) -> int:
    digits = [c for c in line if c in string.digits]
    return int(digits[0] + digits[-1])


print("Part one:", sum(parse_part_one(line) for line in input))

digit_values = {
    "one": "1",
    "two": "2",
    "three": "3",
    "four": "4",
    "five": "5",
    "six": "6",
    "seven": "7",
    "eight": "8",
    "nine": "9",
}

for d in string.digits:
    digit_values[d] = d


def parse_part_two(line: str) -> int:
    digits = []
    for digit, value in digit_values.items():
        for idx in [i.start() for i in re.finditer(digit, line)]:
            digits.append((idx, value))

    digits.sort()

    (_, v1), (_, v2) = digits[0], digits[-1]
    return int(v1 + v2)


print("Part two:", sum(parse_part_two(line) for line in input))
