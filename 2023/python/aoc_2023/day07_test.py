"""Advent of Code 2023 - Day 07."""

import io

import pytest

from aoc_2023.day07 import part_one, part_two

EXAMPLE = """\
32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483
"""


@pytest.fixture()
def example():
    return io.StringIO(EXAMPLE)


def test_part_one(example):
    assert part_one(example) == 6440


def test_part_two(example):
    assert part_two(example) == 5905
