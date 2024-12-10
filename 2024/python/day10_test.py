"""
Advent of Code 2024, Day 10: Hoof It.

See: https://adventofcode.com/2024/day/10
"""

import io

import pytest

from day10 import part_one, part_two

EXAMPLE = """\
89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732
"""


@pytest.fixture()
def example() -> str:
    return EXAMPLE


def test_part_one(example: str) -> None:
    """
    Test that checks if the solution for part one works on the example input.
    """
    assert part_one(io.StringIO(example)) == 36


def test_part_two(example: str) -> None:
    """
    Test that checks if the solution for part two works on the example input.
    """
    assert part_two(io.StringIO(example)) == 81
