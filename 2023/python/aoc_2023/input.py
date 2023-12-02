import functools
import os


class Input:
    def __init__(self, name: str):
        with open(os.path.join(os.path.dirname(__file__), f"../input/{name}"), 'r', encoding="utf-8") as f:
            self._text = f.read()

    @property
    def text(self) -> str:
        return self._text

    @functools.cached_property
    def lines(self) -> list[str]:
        return [line.rstrip() for line in self._text.splitlines()]

    @functools.cached_property
    def ints(self) -> list[int]:
        return [int(line.rstrip()) for line in self._text.splitlines()]
