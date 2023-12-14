"""Advent of Code 2023 - Day 08."""

import io

import pytest

from aoc_2023.day08 import part_one, part_two

EXAMPLE_ONE = """\
LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)
"""

EXAMPLE_TWO = """\
LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)
"""


@pytest.fixture()
def example_one():
    return io.StringIO(EXAMPLE_ONE)


@pytest.fixture()
def example_two():
    return io.StringIO(EXAMPLE_TWO)


def test_part_one(example_one):
    assert part_one(example_one) == 6


def test_part_two(example_two):
    assert part_two(example_two) == 6
