"""Advent of Code 2023 - Day 9."""

from operator import itemgetter

from aoc_2023.input import Input


def extrapolate(series: list[int]) -> tuple[int, int]:
    if all(n == 0 for n in series):
        return 0, 0

    diffs = list(series[i] - series[i - 1] for i in range(1, len(series)))
    diff_left, diff_right = extrapolate(diffs)
    return series[0] - diff_left, series[-1] + diff_right


histories = list(list(map(int, line.split())) for line in Input("09.txt").lines)
extrapolated = list(map(extrapolate, histories))

print("Part one:", sum(map(itemgetter(1), extrapolated)))
print("Part two:", sum(map(itemgetter(0), extrapolated)))
