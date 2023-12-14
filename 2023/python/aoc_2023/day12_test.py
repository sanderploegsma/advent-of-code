"""Advent of Code 2023 - Day 12."""

import io

import pytest

from aoc_2023.day12 import part_one, part_two

EXAMPLE = """\
???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1
"""


@pytest.fixture()
def example():
    return io.StringIO(EXAMPLE)


def test_part_one(example):
    assert part_one(example) == 21


def test_part_two(example):
    assert part_two(example) == 525152
