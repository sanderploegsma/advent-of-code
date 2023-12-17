"""
Advent of Code 2023, Day 10: Pipe Maze.
"""

import io

import pytest

from aoc_2023.day10 import part_one, part_two

EXAMPLE_ONE = """\
-L|F7
7S-7|
L|7||
-L-J|
L|-JF
"""

EXAMPLE_TWO = """\
7-F7-
.FJ|7
SJLL7
|F--J
LJ.LJ
"""

EXAMPLE_THREE = """\
...........
.S-------7.
.|F-----7|.
.||.....||.
.||.....||.
.|L-7.F-J|.
.|..|.|..|.
.L--J.L--J.
...........
"""

EXAMPLE_FOUR = """\
.F----7F7F7F7F-7....
.|F--7||||||||FJ....
.||.FJ||||||||L7....
FJL7L7LJLJ||LJ.L-7..
L--J.L7...LJS7F-7L7.
....F-J..F7FJ|L7L7L7
....L7.F7||L7|.L7L7|
.....|FJLJ|FJ|F7|.LJ
....FJL-7.||.||||...
....L---J.LJ.LJLJ...
"""


@pytest.fixture()
def example_one():
    return io.StringIO(EXAMPLE_ONE)


@pytest.fixture()
def example_two():
    return io.StringIO(EXAMPLE_TWO)


@pytest.fixture()
def example_three():
    return io.StringIO(EXAMPLE_THREE)


@pytest.fixture()
def example_four():
    return io.StringIO(EXAMPLE_FOUR)


def test_part_one(example_one, example_two):
    assert part_one(example_one) == 4
    assert part_one(example_two) == 8


def test_part_two(example_three, example_four):
    assert part_two(example_three) == 4
    assert part_two(example_four) == 8
