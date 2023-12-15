"""Advent of Code 2023, Day 15: Lens Library."""

import io
from typing import TextIO

import pytest

from aoc_2023.day15 import part_one, part_two

EXAMPLE = """\
rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7
"""


@pytest.fixture()
def example() -> TextIO:
    return io.StringIO(EXAMPLE)


def test_part_one(example: TextIO) -> None:
    """Test that checks if the solution for part one works on the example input."""
    assert part_one(example) == 1320


def test_part_two(example: TextIO) -> None:
    """Test that checks if the solution for part two works on the example input."""
    assert part_two(example) == 145
