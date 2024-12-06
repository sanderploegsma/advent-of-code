import pytest
from grid import rotate_cw, rotate_ccw, NORTH, EAST, SOUTH, WEST


@pytest.mark.parametrize(
    ("direction", "expected"),
    [
        (NORTH, EAST),
        (EAST, SOUTH),
        (SOUTH, WEST),
        (WEST, NORTH),
    ],
)
def test_rotate_cw(direction: complex, expected: complex):
    assert rotate_cw(direction) == expected


@pytest.mark.parametrize(
    ("direction", "expected"),
    [
        (NORTH, WEST),
        (EAST, NORTH),
        (SOUTH, EAST),
        (WEST, SOUTH),
    ],
)
def test_rotate_ccw(direction: complex, expected: complex):
    assert rotate_ccw(direction) == expected
