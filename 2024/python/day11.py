"""
Advent of Code 2024, Day 11: Plutonian Pebbles.

See: https://adventofcode.com/2024/day/11
"""

import sys
from typing import TextIO
from collections import Counter


def blink(stones: Counter):
    next_stones = Counter()
    for stone, count in stones.items():
        if stone == 0:
            next_stones[1] += count
            continue

        stone_str = str(stone)
        if len(stone_str) % 2 == 0:
            left, right = (
                int(stone_str[: len(stone_str) // 2]),
                int(stone_str[len(stone_str) // 2 :]),
            )
            next_stones[left] += count
            next_stones[right] += count
            continue

        next_stones[stone * 2024] += count

    return next_stones


def part_one(file: TextIO) -> int:
    """
    Count the amount of stones after blinking 25 times.
    """
    stones = Counter(map(int, file.read().split()))
    for _ in range(25):
        stones = blink(stones)
    return sum(stones.values())


def part_two(file: TextIO) -> int:
    """
    Count the amount of stones after blinking 75 times.
    """
    stones = Counter(map(int, file.read().split()))
    for _ in range(75):
        stones = blink(stones)
    return sum(stones.values())


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
