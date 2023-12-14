"""Commonly used parsing functions."""

from typing import Callable, Optional, TextIO

from aoc_2023.navigation import XY


def parse_ints(file: TextIO) -> list[int]:
    return list(map(int, file))


def parse_grid(file: TextIO, cell_filter: Optional[Callable[[str], bool]] = None) -> set[XY]:
    grid = set()
    for y, row in enumerate(file):
        for x, cell in enumerate(row):
            if cell_filter or cell_filter(cell):
                grid.add(XY(x, y))

    return grid
