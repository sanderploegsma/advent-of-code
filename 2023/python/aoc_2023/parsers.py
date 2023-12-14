from typing import Callable, Optional

from aoc_2023.navigation import XY


def parse_grid(text: str, cell_filter: Optional[Callable[[str], bool]] = None) -> set[XY]:
    grid = set()
    for y, row in enumerate(text.splitlines()):
        for x, cell in enumerate(row):
            if cell_filter and cell_filter(cell):
                grid.add(XY(x, y))

    return grid
