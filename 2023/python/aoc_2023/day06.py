"""Advent of Code 2023 - Day 6."""

import math

example_one = [(7, 9), (15, 40), (30, 200)]
input_one = [(45, 305), (97, 1062), (72, 1110), (95, 1695)]


def solve_quadratic(a: float, b: float, c: float) -> tuple[float, float]:
    """Solve the quadratic formula ax^2 + bx + c = 0."""
    d = b ** 2 - 4 * a * c
    return (-b - d ** 0.5) / (2 * a), (-b + d ** 0.5) / (2 * a)


def winning_charge_times(race: tuple[int, int]) -> range:
    """List the possible charging times that would win the given race."""
    time, record = race
    bounds = solve_quadratic(1, -time, record)
    return range(int(math.ceil(min(bounds))), int(math.ceil(max(bounds))))


print("Part one:", math.prod(len(winning_charge_times(race)) for race in input_one))

example_two = 71530, 940200
input_two = 45977295, 305106211101695

print("Part two:", len(winning_charge_times(input_two)))
