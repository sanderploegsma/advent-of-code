"""
Advent of Code 2023, Day 14: Parabolic Reflector Dish.
"""

import io

import pytest

from aoc_2023.day14 import part_one, part_two

EXAMPLE = """\
O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....
"""


@pytest.fixture()
def example():
    return io.StringIO(EXAMPLE)


def test_part_one(example):
    assert part_one(example) == 136


def test_part_two(example):
    assert part_two(example) == 64
