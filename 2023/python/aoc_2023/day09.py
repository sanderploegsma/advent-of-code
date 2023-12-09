"""Advent of Code 2023 - Day 9."""

from aoc_2023.input import Input


def extrapolate(history: list[int]) -> int:
    if all(n == 0 for n in history):
        return 0

    diffs = list(history[i] - history[i - 1] for i in range(1, len(history)))

    return history[-1] + extrapolate(diffs)


histories = list(list(map(int, line.split())) for line in Input("09.txt").lines)

print("Part one:", sum(map(extrapolate, histories)))
print("Part two:", sum(map(extrapolate, [list(reversed(h)) for h in histories])))
