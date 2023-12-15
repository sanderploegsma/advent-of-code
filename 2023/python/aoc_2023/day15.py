"""Advent of Code 2023, Day 15: Lens Library."""

import sys
from typing import TextIO


def hash(step: str):
    value = 0
    for c in step:
        value += ord(c)
        value *= 17
        value %= 256
    return value


def index_of(items, predicate):
    return next((idx for idx, item in enumerate(items) if predicate(item)), -1)


def part_one(file: TextIO) -> int:
    """Solve part one of the puzzle."""
    steps = file.read().strip().split(",")
    return sum(map(hash, steps))


def part_two(file: TextIO) -> int:
    """Solve part two of the puzzle."""
    steps = file.read().strip().split(",")
    boxes = {i: [] for i in range(256)}
    for step in steps:
        if "-" in step:
            label = step[:-1]
            i = hash(label)
            if (idx := index_of(boxes[i], lambda item: label in item)) >= 0:
                boxes[i].pop(idx)
        if "=" in step:
            label = step[:-2]
            i = hash(label)
            if (idx := index_of(boxes[i], lambda item: label in item)) >= 0:
                boxes[i].pop(idx)
                boxes[i].insert(idx, step)
            else:
                boxes[i].append(step)

    def box_value(box: int) -> int:
        return (box + 1) * sum((i + 1) * int(item[-1]) for i, item in enumerate(boxes[box]))

    return sum(map(box_value, boxes.keys()))


def main():
    """The main entrypoint for the script."""
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
