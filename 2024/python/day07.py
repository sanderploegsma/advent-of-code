"""
Advent of Code 2024, Day 7: Bridge Repair.

See: https://adventofcode.com/2024/day/7
"""

import sys
from typing import TextIO


def parse(file: TextIO):
    for line in file.read().splitlines():
        value, operands = line.split(": ")
        yield int(value), tuple(map(int, operands.split()))


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """

    def is_valid(value: int, operands: tuple[int, ...], current: int = 0):
        if len(operands) == 0:
            return current == value

        head, *tail = operands
        return is_valid(value, tail, current + head) or is_valid(
            value, tail, current * head
        )

    return sum(value for value, operands in parse(file) if is_valid(value, operands))


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """

    def is_valid(value: int, operands: tuple[int, ...], current: int = 0):
        if len(operands) == 0:
            return current == value

        head, *tail = operands
        return (
            is_valid(value, tail, current + head)
            or is_valid(value, tail, current * head)
            or is_valid(value, tail, int(str(current) + str(head)))
        )

    return sum(value for value, operands in parse(file) if is_valid(value, operands))


def main():
    """
    The main entrypoint for the script.
    """
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
