"""Advent of Code 2023 - Day 01."""

import io

import pytest

from aoc_2023.day01 import part_one, part_two

EXAMPLE_ONE = """\
1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet
"""

EXAMPLE_TWO = """\
two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen
"""


@pytest.fixture()
def example_one():
    return io.StringIO(EXAMPLE_ONE)


@pytest.fixture()
def example_two():
    return io.StringIO(EXAMPLE_TWO)


def test_part_one(example_one):
    assert part_one(example_one) == 142


def test_part_two(example_two):
    assert part_two(example_two) == 281
