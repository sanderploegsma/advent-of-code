"""Advent of Code 2023 - Day 02."""

import io

import pytest

from aoc_2023.day02 import part_one, part_two

EXAMPLE = """\
Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
"""


@pytest.fixture()
def example():
    return io.StringIO(EXAMPLE)


def test_part_one(example):
    assert part_one(example) == 8


def test_part_two(example):
    assert part_two(example) == 2286
