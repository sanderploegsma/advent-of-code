"""Advent of Code 2023 - Day 5."""

from typing import Callable

from aoc_2023.input import Input


class Translation:
    def __init__(self, translate: Callable[[int], int], translate_reverse: Callable[[int], int]):
        self._translate = translate
        self._translate_reverse = translate_reverse

    def translate(self, value: int) -> int:
        return self._translate(value)

    def translate_reverse(self, value: int) -> int:
        return self._translate_reverse(value)


class Translations:
    def __init__(self, translations: list[Translation]):
        self._translations = translations

    def translate(self, value: int) -> int:
        values = [value]
        for t in self._translations:
            values.append(t.translate(values[-1]))
        return values[-1]

    def translate_reverse(self, value: int) -> int:
        values = [value]
        for t in reversed(self._translations):
            values.append(t.translate_reverse(values[-1]))
        return values[-1]


def parse_translation(block: str) -> Translation:
    mapping = []
    for line in block.splitlines()[1:]:
        start_dst, start_src, length = [int(x) for x in line.split(" ")]
        mapping.append(((start_dst, start_dst + length - 1), (start_src, start_src + length - 1)))

    def translate(value: int) -> int:
        for (a, _), (c, d) in mapping:
            if c <= value <= d:
                return a + (value - c)

        return value

    def translate_reverse(value: int) -> int:
        for (a, b), (c, _) in mapping:
            if a <= value <= b:
                return c + (value - a)

        return value

    return Translation(translate, translate_reverse)


seeds, maps = Input("05.txt").text.split("\n\n", maxsplit=1)
seeds = [int(x) for x in seeds.replace("seeds: ", "").split(" ")]
translations = Translations([parse_translation(block) for block in maps.split("\n\n")])

print("Part one:", min([translations.translate(seed) for seed in seeds]))

seed_ranges = []
for i in range(0, len(seeds), 2):
    seed_ranges.append((seeds[i], seeds[i] + seeds[i + 1] - 1))

loc = 1
found = False
while not found:
    seed = translations.translate_reverse(loc)
    for seed_start, seed_end in seed_ranges:
        if seed_start <= seed <= seed_end:
            print("Part two:", loc)
            found = True
    loc += 1
