"""
Advent of Code 2024, Day 11: Plutonian Pebbles.

See: https://adventofcode.com/2024/day/11
"""

import io

import pytest

from day11 import part_one

EXAMPLE = "125 17"


@pytest.fixture()
def example() -> str:
    return EXAMPLE


def test_part_one(example: str) -> None:
    """
    Test that checks if the solution for part one works on the example input.
    """
    assert part_one(io.StringIO(example)) == 55312
