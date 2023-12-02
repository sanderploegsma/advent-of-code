"""Advent of Code 2020 - Day 09."""

from itertools import combinations


def part_one(input: list[int]):
    preamble, values = input[:25], input[25:]

    while len(values) > 0:
        value = values.pop(0)
        if any(a != b and a + b == value for a, b in combinations(preamble, 2)):
            preamble.pop(0)
            preamble.append(value)
        else:
            return value


def part_two(invalid: int, input: list[int]):
    values = []
    i = 0

    while sum(values) < invalid and i < len(input):
        values.append(input[i])
        i += 1

    if sum(values) == invalid:
        return min(values) + max(values)

    return part_two(invalid, input[1:])


with open("../input/09.txt", encoding="utf-8") as file:
    input = [int(line) for line in file.readlines()]

invalid = part_one(input)
print("Part one:", invalid)
print("Part two:", part_two(invalid, input))
