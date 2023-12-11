"""Advent of Code 2023 - Day 11."""

from itertools import combinations
from operator import itemgetter
from typing import Generator

from aoc_2023.input import Input
from aoc_2023.navigation import XY, manhattan_distance


def expand(rows: list[str], n: int) -> list[list[tuple[int, int, str]]]:
    result = list(list((1, 1, c) for c in row) for row in rows)

    for y, row in enumerate(rows):
        if all(c == "." for c in row):
            result[y] = list((w, n, c) for w, _, c in result[y])

    for x in range(len(rows[0])):
        col = list(row[x] for row in rows)
        if all(c == "." for c in col):
            for y, _ in enumerate(result):
                result[y][x] = (n, result[y][x][1], result[y][x][2])

    return result


def parse(grid: list[list[tuple[int, int, str]]]) -> Generator[XY, None, None]:
    y = 0
    for row in grid:
        x = 0
        for w, h, c in row:
            if c == "#":
                yield XY(x, y)
            x += w
        y += row[0][1]


coordinates = list(parse(expand(Input("11.txt").lines, 2)))
distances = list((p1, p2, manhattan_distance(p1, p2)) for p1, p2 in combinations(coordinates, 2))
print("Part one:", sum(map(itemgetter(2), distances)))

coordinates2 = list(parse(expand(Input("11.txt").lines, 1000000)))
distances2 = list((p1, p2, manhattan_distance(p1, p2)) for p1, p2 in combinations(coordinates2, 2))
print("Part two:", sum(map(itemgetter(2), distances2)))
