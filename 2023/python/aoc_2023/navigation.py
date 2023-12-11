from dataclasses import dataclass
from typing import Iterable, Iterator


@dataclass(frozen=True)
class XY:
    """Represents a point on a 2-dimensional grid."""

    x: int
    y: int

    def __add__(self, other):
        return XY(self.x + other.x, self.y + other.y)

    def __sub__(self, other):
        return XY(self.x - other.x, self.y - other.y)

    def __iter__(self) -> Iterator[int]:
        yield from [self.x, self.y]


UP = XY(0, -1)
DOWN = XY(0, 1)
LEFT = XY(-1, 0)
RIGHT = XY(1, 0)


def manhattan_distance(p1: XY, p2: XY) -> int:
    """
    Calculates the manhattan distance between two points on a 2-dimensional grid.
    """
    return abs(p2.x - p1.x) + abs(p2.y - p1.y)


def bounding_box(coordinates: Iterable[XY]) -> tuple[XY, XY]:
    min_x = min(c.x for c in coordinates)
    max_x = max(c.x for c in coordinates)
    min_y = min(c.y for c in coordinates)
    max_y = max(c.y for c in coordinates)
    return XY(min_x, min_y), XY(max_x, max_y)
