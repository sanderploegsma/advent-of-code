"""
Advent of Code 2023, Day 19: Aplenty.
"""

import sys
from functools import partial
from typing import TextIO


def parse_rule(rule: str):
    name, conditions = rule.split("{")
    return name, conditions.strip()[:-1].split(",")


def parse_part(part: str) -> dict[str, int]:
    kvs = part.strip()[1:-1]
    return eval(f"dict({kvs})", {"__builtins__": None, "dict": dict})


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    rules, parts = file.read().split("\n\n")
    rules = {name: rule for line in rules.splitlines() for name, rule in [parse_rule(line.strip())]}
    parts = list(map(parse_part, parts.splitlines()))

    def evaluate(rule: str, part: dict[str, int]) -> bool:
        if rule == "A":
            return True
        if rule == "R":
            return False

        for condition in rules[rule]:
            if ":" in condition:
                condition, next_rule = condition.split(":")
                if eval(condition, {"__builtins__": None, **part}):
                    return evaluate(next_rule, part)
            else:
                return evaluate(condition, part)

    return sum(sum(part.values()) for part in filter(partial(evaluate, "in"), parts))


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    return -1


def main():
    """
    The main entrypoint for the script.
    """
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
