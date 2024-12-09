"""
Advent of Code 2024, Day 9: Disk Fragmenter.

See: https://adventofcode.com/2024/day/9
"""

import sys
from typing import TextIO


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    mem = []
    for i, d in enumerate(file.read()):
        blocks = int(d)
        file_id = i // 2 if i % 2 == 0 else None
        mem += [file_id] * blocks

    left_idx, right_idx = 0, len(mem) - 1
    # Search for first free block
    while mem[left_idx] is not None:
        left_idx += 1

    # Search for last value block
    while mem[right_idx] is None:
        right_idx -= 1

    while left_idx < right_idx:
        # Swap blocks
        mem[left_idx] = mem[right_idx]
        mem[right_idx] = None

        # Search for first free block
        while mem[left_idx] is not None:
            left_idx += 1

        # Search for last value block
        while mem[right_idx] is None:
            right_idx -= 1

    return sum(i * d for i, d in enumerate(mem) if d is not None)


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    mem = []
    for i, d in enumerate(file.read()):
        blocks = int(d)
        file_id = i // 2 if i % 2 == 0 else None
        mem += [file_id] * blocks

    file_end = len(mem) - 1
    file_start = len(mem) - 1
    while mem[file_start - 1] == mem[file_end]:
        file_start -= 1

    while file_end > 0:
        file_size = file_end - file_start + 1

        for free_start in range(file_start):
            if mem[free_start] is not None:
                continue

            free_end = free_start
            while free_end < len(mem) - 1 and mem[free_end + 1] is None:
                free_end += 1

            free_size = free_end - free_start + 1
            if free_size >= file_size:
                mem[free_start : free_start + file_size] = mem[
                    file_start : file_end + 1
                ]
                mem[file_start : file_end + 1] = [None] * file_size
                break

        file_end = file_start - 1
        while file_end >= 0 and mem[file_end] is None:
            file_end -= 1

        file_start = file_end
        while file_start > 0 and mem[file_start - 1] == mem[file_end]:
            file_start -= 1

    return sum(i * d for i, d in enumerate(mem) if d is not None)


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
