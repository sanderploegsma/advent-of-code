import functools
import os

from aoc_2023.navigation import XY


class Input:
    def __init__(self, name: str):
        with open(os.path.join(os.path.dirname(__file__), f"../input/{name}"), 'r', encoding="utf-8") as f:
            self._text = f.read()

    @property
    def text(self) -> str:
        return self._text

    @functools.cached_property
    def lines(self) -> list[str]:
        return list(line.rstrip() for line in self.text.splitlines())

    @functools.cached_property
    def ints(self) -> list[int]:
        return list(map(int, self.lines))

    @functools.cached_property
    def grid(self) -> dict[XY, str]:
        result = {}
        for y, row in enumerate(self.lines):
            for x, cell in enumerate(row):
                result[XY(x, y)] = cell

        return result
