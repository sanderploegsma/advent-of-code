"""Commonly used parsing functions."""

from typing import Iterable

from aoc_2023.navigation import XY


def parse_blocks(data: str) -> list[str]:
    return data.split("\n\n")


def parse_ints(data: Iterable[str]) -> list[int]:
    return list(map(int, data))


def parse_int_lists(data: Iterable[str]) -> list[list[int]]:
    return list(list(map(int, line.strip().split())) for line in data)


def parse_coordinates(data: Iterable[str], symbol: str = "#") -> set[XY]:
    grid = set()
    for y, row in enumerate(data):
        for x, cell in enumerate(row):
            if cell == symbol:
                grid.add(XY(x, y))

    return grid


def parse_grid(data: Iterable[str]) -> dict[XY, str]:
    grid = {}
    for y, row in enumerate(data):
        for x, cell in enumerate(row):
            grid[(XY(x, y))] = cell

    return grid
