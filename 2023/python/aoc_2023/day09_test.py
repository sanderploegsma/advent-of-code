"""Advent of Code 2023 - Day 09."""

import io

import pytest

from aoc_2023.day09 import part_one, part_two

EXAMPLE = """\
0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45
"""


@pytest.fixture()
def example():
    return io.StringIO(EXAMPLE)


def test_part_one(example):
    assert part_one(example) == 114


def test_part_two(example):
    assert part_two(example) == 2
