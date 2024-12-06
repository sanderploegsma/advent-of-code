"""
Advent of Code 2024, Day 5: Print Queue.

See: https://adventofcode.com/2024/day/5
"""

import functools
import sys
from typing import TextIO


def parse(file: TextIO):
    rules_input, updates_input = file.read().split("\n\n")

    rules = [
        tuple(map(int, line.split("|", maxsplit=1)))
        for line in rules_input.splitlines()
    ]
    updates = [list(map(int, line.split(","))) for line in updates_input.splitlines()]
    return rules, updates


def is_ordered(rules: list[tuple[int, int]], update: list[int]) -> bool:
    for a, b in rules:
        if a not in update or b not in update:
            continue

        if update.index(a) > update.index(b):
            return False

    return True


def compare(rules: list[tuple[int, int]], a: int, b: int) -> int:
    for left, right in rules:
        if a == left and b == right:
            return -1

        if a == right and b == left:
            return 1

    return 0


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    rules, updates = parse(file)
    is_valid = functools.partial(is_ordered, rules)
    valid_updates = filter(is_valid, updates)
    return sum(update[len(update) // 2] for update in valid_updates)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    rules, updates = parse(file)
    is_valid = functools.partial(is_ordered, rules)
    reorder_update = functools.cmp_to_key(functools.partial(compare, rules))
    invalid_updates = list(filter(lambda u: not is_valid(u), updates))

    return sum(
        sorted(update, key=reorder_update)[len(update) // 2]
        for update in invalid_updates
    )


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
