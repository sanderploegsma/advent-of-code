"""Advent of Code 2023, Day 16: The Floor Will Be Lava."""

import io
from typing import TextIO

import pytest

from aoc_2023.day16 import part_one, part_two

EXAMPLE = """\
.|...\\....
|.-.\\.....
.....|-...
........|.
..........
.........\\
..../.\\\\..
.-.-/..|..
.|....-|.\\
..//.|....
"""


@pytest.fixture()
def example() -> TextIO:
    return io.StringIO(EXAMPLE)


def test_part_one(example: TextIO) -> None:
    """Test that checks if the solution for part one works on the example input."""
    assert part_one(example) == 46


def test_part_two(example: TextIO) -> None:
    """Test that checks if the solution for part two works on the example input."""
    assert part_two(example) == 51
