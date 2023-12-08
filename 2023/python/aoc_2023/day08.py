"""Advent of Code 2023 - Day 8."""

import math
import re

from aoc_2023.input import Input


def parse_node(line: str):
    m = re.match(r"([A-Z0-9]{3}) = \(([A-Z0-9]{3}), ([A-Z0-9]{3})\)", line)
    return m.group(1), (m.group(2), m.group(3))


instructions, _, *nodes = Input("08.txt").lines
nodes = dict(map(parse_node, nodes))

current = "AAA"
steps = 0
while current != "ZZZ":
    left, right = nodes[current]
    match instructions[steps % len(instructions)]:
        case "L":
            current = left
        case "R":
            current = right
    steps += 1

print("Part one:", steps)

starting_nodes = list(filter(lambda n: n[-1] == "A", nodes.keys()))
distances = dict()

for node in starting_nodes:
    current = node
    i = 0
    while current[-1] != "Z":
        left, right = nodes[current]
        match instructions[i % len(instructions)]:
            case "L":
                current = left
            case "R":
                current = right
        i += 1
    distances[node] = i

print("Part two:", math.lcm(*distances.values()))
