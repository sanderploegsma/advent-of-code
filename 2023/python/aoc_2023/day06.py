"""Advent of Code 2023 - Day 06."""

import math

INPUT_ONE = [(45, 305), (97, 1062), (72, 1110), (95, 1695)]
INPUT_TWO = 45977295, 305106211101695


def solve_quadratic(a: float, b: float, c: float) -> tuple[float, float]:
    """Solve the quadratic formula ax^2 + bx + c = 0."""
    d = b**2 - 4 * a * c
    return (-b - d**0.5) / (2 * a), (-b + d**0.5) / (2 * a)


def winning_charge_times(race: tuple[int, int]) -> range:
    """List the possible charging times that would win the given race."""
    time, record = race
    bounds = solve_quadratic(1, -time, record)
    return range(int(math.ceil(min(bounds))), int(math.ceil(max(bounds))))


def part_one(races: list[tuple[int, int]]) -> int:
    return math.prod(len(winning_charge_times(race)) for race in races)


def part_two(race: tuple[int, int]) -> int:
    return len(winning_charge_times(race))


def main():
    print("Part one:", part_one(INPUT_ONE))
    print("Part two:", part_two(INPUT_TWO))


if __name__ == "__main__":
    main()
