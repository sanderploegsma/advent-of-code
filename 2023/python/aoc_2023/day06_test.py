"""Advent of Code 2023 - Day 06."""

import pytest

from aoc_2023.day06 import part_one, part_two


@pytest.fixture()
def example_one():
    return [(7, 9), (15, 40), (30, 200)]


@pytest.fixture()
def example_two():
    return 71530, 940200


def test_part_one(example_one):
    assert part_one(example_one) == 288


def test_part_two(example_two):
    assert part_two(example_two) == 71503
