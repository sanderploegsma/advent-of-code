"""Advent of Code 2023 - Day 12."""

import functools
import itertools

from aoc_2023.input import Input


@functools.cache
def count_options(pattern: str, groups: tuple) -> int:
    if len(pattern) == 0:
        return 1 if len(groups) == 0 else 0

    if pattern[0] == ".":
        return count_options(pattern[1:], groups)

    if pattern[0] == "?":
        return count_options("." + pattern[1:], groups) + count_options("#" + pattern[1:], groups)

    # We found a broken spring
    if len(groups) == 0:
        return 0

    want = groups[0]
    have = 0
    for _ in itertools.takewhile(lambda s: s in "#?", pattern):
        have += 1

    if have < want:  # not enough broken springs
        return 0
    if pattern[want:want + 1] == "#":  # too many broken springs
        return 0

    # Exactly enough broken springs. This means that the next character MUST be a ., even if it's a ?
    return count_options(pattern[want + 1:], groups[1:])


def solve_record(record: str, n: int) -> int:
    pattern, groups = record.split()
    return count_options("?".join([pattern] * n), tuple([*map(int, groups.split(","))] * n))


records = Input("12.txt").lines
print("Part one:", sum(solve_record(r, 1) for r in records))
print("Part two:", sum(solve_record(r, 5) for r in records))
