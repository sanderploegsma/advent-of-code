"""
Advent of Code 2024, Day 9: Disk Fragmenter.

See: https://adventofcode.com/2024/day/9
"""

import io

import pytest

from day09 import part_one, part_two

EXAMPLE = "2333133121414131402"


@pytest.fixture()
def example() -> str:
    return EXAMPLE


def test_part_one(example: str) -> None:
    """
    Test that checks if the solution for part one works on the example input.
    """
    assert part_one(io.StringIO(example)) == 1928


def test_part_two(example: str) -> None:
    """
    Test that checks if the solution for part two works on the example input.
    """
    assert part_two(io.StringIO(example)) == 2858
