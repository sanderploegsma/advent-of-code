"""
Advent of Code 2024, Day 3: Mull It Over.

See: https://adventofcode.com/2024/day/3
"""

import io


from day03 import part_one, part_two

EXAMPLE_1 = """\
xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
"""

EXAMPLE_2 = """\
xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
"""


def test_part_one() -> None:
    """
    Test that checks if the solution for part one works on the example input.
    """
    assert part_one(io.StringIO(EXAMPLE_1)) == 161


def test_part_two() -> None:
    """
    Test that checks if the solution for part two works on the example input.
    """
    assert part_two(io.StringIO(EXAMPLE_2)) == 48
