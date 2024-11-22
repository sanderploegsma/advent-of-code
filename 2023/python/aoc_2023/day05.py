"""Advent of Code 2023 - Day 05."""

import sys
from functools import reduce
from operator import itemgetter
from typing import Callable, TextIO

F = Callable[[int], int]


def parse_transformation(block: str) -> list[F]:
    mapping = []
    for line in block.splitlines()[1:]:
        start_dst, start_src, length = [int(x) for x in line.split(" ")]
        mapping.append(
            ((start_dst, start_dst + length - 1), (start_src, start_src + length - 1))
        )

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


def parse(file: TextIO, getter: itemgetter) -> tuple[list[int], list[F]]:
    seeds, maps = file.read().split("\n\n", maxsplit=1)
    seeds = list(map(int, seeds.replace("seeds: ", "").split(" ")))
    transformations = list(map(parse_transformation, maps.split("\n\n")))
    transformations = list(map(getter, transformations))

    return seeds, transformations


def part_one(file: TextIO) -> int:
    seeds, transformations = parse(file, itemgetter(0))

    def seed_to_loc(seed: int) -> int:
        return reduce(lambda x, f: f(x), transformations, seed)

    return min(map(seed_to_loc, seeds))


def part_two(file: TextIO) -> int:
    seeds, transformations = parse(file, itemgetter(1))

    def loc_to_seed(loc: int) -> int:
        return reduce(lambda x, f: f(x), reversed(transformations), loc)

    seed_ranges = []
    for i in range(0, len(seeds), 2):
        seed_ranges.append((seeds[i], seeds[i] + seeds[i + 1] - 1))

    loc = 1
    while True:
        seed = loc_to_seed(loc)
        for seed_start, seed_end in seed_ranges:
            if seed_start <= seed <= seed_end:
                return loc
        loc += 1


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
