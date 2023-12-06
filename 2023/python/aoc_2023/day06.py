"""Advent of Code 2023 - Day 6."""

import math

example_one = [(7, 9), (15, 40), (30, 200)]
input_one = [(45, 305), (97, 1062), (72, 1110), (95, 1695)]


def distance(t_total: int, t_charging: int) -> int:
    """Calculate the distance traveled for the given time spent charging."""
    t_traveling = t_total - t_charging
    return t_traveling * t_charging


def winning_charge_times(race: tuple[int, int]) -> list[int]:
    """List the possible charging times that would win the given race."""
    time, record = race
    return list(filter(lambda d: d > record, list(distance(time, t) for t in range(time + 1))))


print("Part one:", math.prod(len(winning_charge_times(race)) for race in input_one))

example_two = 71530, 940200
input_two = 45977295, 305106211101695

print("Part two:", len(winning_charge_times(input_two)))
