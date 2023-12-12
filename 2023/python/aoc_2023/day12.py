"""Advent of Code 2023 - Day 12."""

import re
from aoc_2023.input import Input

lines = Input("12.txt").lines


def permutations(row: str) -> list[str]:
    if "?" not in row:
        return [row]

    return permutations(row.replace("?", ".", 1)) + permutations(row.replace("?", "#", 1))


def count_valid_permutations(row: str, groups: str) -> int:
    counts = list(map(int, groups.split(",")))
    pattern = r"^\.*" + r"\.+".join([f"#{{{d}}}" for d in counts]) + r"\.*$"
    valid = [p for p in permutations(row) if re.match(pattern, p)]
    return len(valid)


print("Part one:", sum(count_valid_permutations(*line.split()) for line in lines))


def unfold(row: str, groups: str) -> tuple[str, str]:
    return "?".join([row] * 5), ",".join([groups] * 5)


# print("Part two:", sum(count_valid_permutations(*unfold(*line.split())) for line in lines))
