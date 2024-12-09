"""
Advent of Code 2024, Day 8: Resonant Collinearity.

See: https://adventofcode.com/2024/day/8
"""

import io

import pytest

from day08 import part_one, part_two

EXAMPLE = """\
............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............
"""


@pytest.fixture()
def example() -> str:
    return EXAMPLE


def test_part_one(example: str) -> None:
    """
    Test that checks if the solution for part one works on the example input.
    """
    assert part_one(io.StringIO(example)) == 14


def test_part_two(example: str) -> None:
    """
    Test that checks if the solution for part two works on the example input.
    """
    assert part_two(io.StringIO(example)) == 34
