"""
Utilities to work with 2D grids.
"""

NORTH = complex(0, -1)
NORTH_EAST = complex(1, -1)
EAST = complex(1, 0)
SOUTH_EAST = complex(1, 1)
SOUTH = complex(0, 1)
SOUTH_WEST = complex(-1, 1)
WEST = complex(-1, 0)
NORTH_WEST = complex(-1, -1)

NEIGHBORS_4 = NORTH, EAST, SOUTH, WEST
NEIGHBORS_8 = NEIGHBORS_4 + (NORTH_EAST, SOUTH_EAST, SOUTH_WEST, NORTH_WEST)


def rotate_cw(direction: complex) -> complex:
    return direction * 1j


def rotate_ccw(direction: complex) -> complex:
    return direction * -1j


class Grid(dict):
    @property
    def width(self):
        return int(max(p.real for p in self.keys()) + 1)

    @property
    def height(self):
        return int(max(p.imag for p in self.keys()) + 1)

    @property
    def corners(self):
        return (
            complex(0, 0),
            complex(self.width - 1, 0),
            complex(self.width - 1, self.height - 1),
            complex(0, self.height - 1),
        )

    def get_neighbors(
        self,
        point: complex,
        neighbors: tuple[complex, ...] = NEIGHBORS_4,
    ):
        for n in neighbors:
            other = point + n
            if other in self:
                yield other

    def draw(self, whitespace: str = "."):
        return "\n".join(
            [
                "".join(
                    [self.get(complex(x, y), whitespace) for x in range(self.width)]
                )
                for y in range(self.height)
            ]
        )

    @classmethod
    def from_int_grid(cls, grid: str):
        points = {
            complex(x, y): int(c)
            for y, line in enumerate(grid.strip().splitlines())
            for x, c in enumerate(line)
        }
        return cls(points)

    @classmethod
    def from_ascii_grid(cls, grid: str, ignore_chars: str = ""):
        points = {
            complex(x, y): c
            for y, line in enumerate(grid.strip().splitlines())
            for x, c in enumerate(line)
            if c not in ignore_chars
        }
        return cls(points)
