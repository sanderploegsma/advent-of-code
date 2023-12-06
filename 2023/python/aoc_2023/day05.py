"""Advent of Code 2023 - Day 5."""

from functools import reduce
from typing import Callable

from aoc_2023.input import Input

F = Callable[[int], int]


def parse_transformation(block: str) -> list[F]:
    mapping = []
    for line in block.splitlines()[1:]:
        start_dst, start_src, length = [int(x) for x in line.split(" ")]
        mapping.append(((start_dst, start_dst + length - 1), (start_src, start_src + length - 1)))

    def translate(value: int) -> int:
        for (start_dst, _), (start_src, end_src) in mapping:
            if start_src <= value <= end_src:
                return start_dst + (value - start_src)

        return value

    def translate_reverse(value: int) -> int:
        for (start_dst, end_dst), (start_src, _) in mapping:
            if start_dst <= value <= end_dst:
                return start_src + (value - start_dst)

        return value

    return [translate, translate_reverse]


seeds, maps = Input("05.txt").text.split("\n\n", maxsplit=1)
seeds = [int(x) for x in seeds.replace("seeds: ", "").split(" ")]
transformations = [parse_transformation(block) for block in maps.split("\n\n")]
transformations = list(map(list, zip(*transformations)))


def seed_to_loc(seed: int) -> int:
    return reduce(lambda x, f: f(x), transformations[0], seed)


def loc_to_seed(loc: int) -> int:
    return reduce(lambda x, f: f(x), reversed(transformations[1]), loc)


print("Part one:", min([seed_to_loc(seed) for seed in seeds]))

seed_ranges = []
for i in range(0, len(seeds), 2):
    seed_ranges.append((seeds[i], seeds[i] + seeds[i + 1] - 1))

loc = 1
found = False
while not found:
    seed = loc_to_seed(loc)
    for seed_start, seed_end in seed_ranges:
        if seed_start <= seed <= seed_end:
            print("Part two:", loc)
            found = True
    loc += 1
