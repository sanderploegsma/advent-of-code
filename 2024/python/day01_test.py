"""
Advent of Code 2024, Day 1: Historian Hysteria.

See: https://adventofcode.com/2024/day/1
"""

import io

import pytest

from day01 import part_one, part_two

EXAMPLE = """\
3   4
4   3
2   5
1   3
3   9
3   3
"""


@pytest.fixture()
def example() -> str:
    return EXAMPLE


def test_part_one(example: str) -> None:
    """
    Test that checks if the solution for part one works on the example input.
    """
    assert part_one(io.StringIO(example)) == 11


def test_part_two(example: str) -> None:
    """
    Test that checks if the solution for part two works on the example input.
    """
    assert part_two(io.StringIO(example)) == 31
