"""Advent of Code 2023 - Day 13."""

import io

import pytest

from aoc_2023.day13 import part_one, part_two

EXAMPLE = """\
#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#
"""


@pytest.fixture()
def example():
    return io.StringIO(EXAMPLE)


def test_part_one(example):
    assert part_one(example) == 405


def test_part_two(example):
    assert part_two(example) == 400
