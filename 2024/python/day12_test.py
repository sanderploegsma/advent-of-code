"""
Advent of Code 2024, Day 12: Garden Groups.

See: https://adventofcode.com/2024/day/12
"""

import io

import pytest

from day12 import part_one, part_two

EXAMPLE_1 = """\
AAAA
BBCD
BBCC
EEEC
"""

EXAMPLE_2 = """\
OOOOO
OXOXO
OOOOO
OXOXO
OOOOO
"""

EXAMPLE_3 = """\
RRRRIICCFF
RRRRIICCCF
VVRRRCCFFF
VVRCCCJFFF
VVVVCJJCFE
VVIVCCJJEE
VVIIICJJEE
MIIIIIJJEE
MIIISIJEEE
MMMISSJEEE
"""


@pytest.mark.parametrize(
    ("example", "answer"),
    [
        (EXAMPLE_1, 140),
        (EXAMPLE_2, 772),
        (EXAMPLE_3, 1930),
    ],
)
def test_part_one(example: str, answer: int) -> None:
    """
    Test that checks if the solution for part one works on the example input.
    """
    assert part_one(io.StringIO(example)) == answer


@pytest.mark.parametrize(
    ("example", "answer"),
    [
        (EXAMPLE_1, 80),
        (EXAMPLE_3, 1206),
    ],
)
def test_part_two(example: str, answer: int) -> None:
    """
    Test that checks if the solution for part two works on the example input.
    """
    assert part_two(io.StringIO(example)) == answer
