"""Advent of Code 2023 - Day 10."""

from itertools import pairwise
from aoc_2023.input import Input

UP = (0, -1)
DOWN = (0, 1)
LEFT = (-1, 0)
RIGHT = (1, 0)

start_directions = {
    UP: "|F7",
    DOWN: "|JL",
    RIGHT: "-J7",
    LEFT: "-FL",
}

directions = {
    "-": {LEFT: LEFT, RIGHT: RIGHT},
    "|": {UP: UP, DOWN: DOWN},
    "L": {LEFT: UP, DOWN: RIGHT},
    "J": {RIGHT: UP, DOWN: LEFT},
    "7": {RIGHT: DOWN, UP: LEFT},
    "F": {UP: RIGHT, LEFT: DOWN},
}


def move(a: tuple[int, int], b: tuple[int, int]) -> tuple[int, int]:
    ax, ay = a
    bx, by = b
    return ax + bx, ay + by


grid = Input("10.txt").grid
start = next(coord for coord, cell in grid.items() if cell == "S")
direction = next(d for d, s in start_directions.items() if grid.get(move(start, d), "?") in s)

position = move(start, direction)
loop = [start]
while position != start:
    loop.append(position)
    symbol = grid[position]
    direction = directions[symbol][direction]
    position = move(position, direction)

print("Part one:", len(loop) // 2)

area = abs(sum(x1 * y2 - x2 * y1 for (x1, y1), (x2, y2) in pairwise([*loop, start]))) // 2

print("Part two:", area - len(loop) // 2 + 1)
