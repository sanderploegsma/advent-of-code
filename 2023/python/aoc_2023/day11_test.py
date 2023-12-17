"""
Advent of Code 2023, Day 11: Cosmic Expansion.
"""

import io

import pytest

from aoc_2023.day11 import part_one, part_two

EXAMPLE = """\
...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....
"""


@pytest.fixture()
def example():
    return io.StringIO(EXAMPLE)


def test_part_one(example):
    assert part_one(example) == 374


@pytest.mark.parametrize("n,expected", [(10, 1030), (100, 8410)])
def test_part_two(example, n, expected):
    assert part_two(example, n) == expected
