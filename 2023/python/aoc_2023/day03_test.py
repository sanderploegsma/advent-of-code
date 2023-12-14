"""Advent of Code 2023 - Day 03."""

import io

import pytest

from aoc_2023.day03 import part_one, part_two

EXAMPLE = """\
467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..
"""


@pytest.fixture()
def example():
    return io.StringIO(EXAMPLE)


def test_part_one(example):
    assert part_one(example) == 4361


def test_part_two(example):
    assert part_two(example) == 467835
