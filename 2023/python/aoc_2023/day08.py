"""Advent of Code 2023 - Day 08."""

import math
import re
import sys
from typing import Callable, TextIO


def parse(file: TextIO) -> tuple[str, dict[str, tuple[str, str]]]:
    def parse_node(line: str):
        m = re.match(r"([A-Z0-9]{3}) = \(([A-Z0-9]{3}), ([A-Z0-9]{3})\)", line)
        return m.group(1), (m.group(2), m.group(3))

    instructions, _, *nodes = list(line.strip() for line in file)
    nodes = dict(map(parse_node, nodes))
    return instructions, nodes


def find_distance(instructions: str, nodes: dict[str, tuple[str, str]], start: str, is_end: Callable[[str], bool]):
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


def part_one(file: TextIO) -> int:
    instructions, nodes = parse(file)
    return find_distance(instructions, nodes, "AAA", lambda n: n == "ZZZ")


def part_two(file: TextIO) -> int:
    instructions, nodes = parse(file)
    starting_nodes = list(filter(lambda n: n[-1] == "A", nodes.keys()))
    distances = list(find_distance(instructions, nodes, node, lambda n: n[-1] == "Z") for node in starting_nodes)

    return math.lcm(*distances)


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
