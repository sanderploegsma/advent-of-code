"""Advent of Code 2023 - Day 5."""

from dataclasses import dataclass
from aoc_2023.input import Input


@dataclass
class Lookup:
    seed: int
    soil: int
    fert: int
    water: int
    light: int
    temp: int
    humid: int
    loc: int


Map = list[tuple[tuple[int, int], tuple[int, int]]]


def parse_map(block: str) -> Map:
    result: Map = []
    for line in block.splitlines()[1:]:
        start_dst, start_src, length = [int(x) for x in line.split(" ")]
        result.append(((start_src, start_src + length - 1), (start_dst, start_dst + length - 1)))
    return result


seeds, maps = Input("05.txt").text.split("\n\n", maxsplit=1)

seeds = [int(x) for x in seeds.replace("seeds: ", "").split(" ")]
seed_to_soil, soil_to_fert, fert_to_water, water_to_light, light_to_temp, temp_to_humid, humid_to_loc = [
    parse_map(block) for block in maps.split("\n\n")]


def lookup_seed(seed: int) -> Lookup:
    def lookup(value: int, mapping: Map) -> int:
        for (src_start, src_end), (dest_start, dest_end) in mapping:
            if src_start <= value <= src_end:
                return dest_start + (value - src_start)

        return value

    soil = lookup(seed, seed_to_soil)
    fert = lookup(soil, soil_to_fert)
    water = lookup(fert, fert_to_water)
    light = lookup(water, water_to_light)
    temp = lookup(light, light_to_temp)
    humid = lookup(temp, temp_to_humid)
    loc = lookup(humid, humid_to_loc)
    return Lookup(seed, soil, fert, water, light, temp, humid, loc)


lookups = [lookup_seed(seed) for seed in seeds]

print("Part one:", min(lookups, key=lambda l: l.loc))

seed_ranges = []
for i in range(0, len(seeds), 2):
    seed_ranges.append((seeds[i], seeds[i] + seeds[i + 1] - 1))


def lookup_loc(loc: int) -> Lookup:
    def lookup_reverse(value: int, mapping: Map) -> int:
        for (src_start, src_end), (dest_start, dest_end) in mapping:
            if dest_start <= value <= dest_end:
                return src_start + (value - dest_start)

        return value

    humid = lookup_reverse(loc, humid_to_loc)
    temp = lookup_reverse(humid, temp_to_humid)
    light = lookup_reverse(temp, light_to_temp)
    water = lookup_reverse(light, water_to_light)
    fert = lookup_reverse(water, fert_to_water)
    soil = lookup_reverse(fert, soil_to_fert)
    seed = lookup_reverse(soil, seed_to_soil)
    return Lookup(seed, soil, fert, water, light, temp, humid, loc)


loc = 1
found = False
while not found:
    lookup = lookup_loc(loc)
    for seed_start, seed_end in seed_ranges:
        if seed_start <= lookup.seed <= seed_end:
            print("Part two:", loc)
            found = True
    loc += 1
