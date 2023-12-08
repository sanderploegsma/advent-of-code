"""Advent of Code 2023 - Day 8."""

import math
import re
from typing import Callable

from aoc_2023.input import Input


def parse_node(line: str):
    m = re.match(r"([A-Z0-9]{3}) = \(([A-Z0-9]{3}), ([A-Z0-9]{3})\)", line)
    return m.group(1), (m.group(2), m.group(3))


instructions, _, *nodes = Input("08.txt").lines
nodes = dict(map(parse_node, nodes))


def find_distance(start: str, is_end: Callable[[str], bool]):
    current = start
    i = 0
    while not is_end(current):
        left, right = nodes[current]
        match instructions[i % len(instructions)]:
            case "L":
                current = left
            case "R":
                current = right
        i += 1
    return i


print("Part one:", find_distance("AAA", lambda n: n == "ZZZ"))

starting_nodes = list(filter(lambda n: n[-1] == "A", nodes.keys()))
distances = dict()

for node in starting_nodes:
    distances[node] = find_distance(node, lambda n: n[-1] == "Z")

print("Part two:", math.lcm(*distances.values()))
