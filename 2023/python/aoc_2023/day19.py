"""
Advent of Code 2023, Day 19: Aplenty.
"""

import math
import operator
import re
import sys
from functools import partial
from typing import Callable, TextIO, TypeAlias

RULE_PATTERN = re.compile(r"(\w+){((?:.*,)*)(\w+)}")
CONDITION_PATTERN = re.compile(r"([xmas])([<>])(\d+):(\w+)")
PART_ITEM_PATTERN = re.compile(r"([xmas])=(\d+)")
OPERATORS = {">": operator.gt, "<": operator.lt}

Predicate: TypeAlias = tuple[str, Callable[[int, int], bool], int]
Condition: TypeAlias = tuple[Predicate | None, str]
Rule: TypeAlias = tuple[str, list[Condition]]


def parse_rules(rules: str):
    def parse_conditions(conditions: str) -> list[Condition]:
        return list(
            ((key, OPERATORS[op], int(val)), next_rule)
            for key, op, val, next_rule in CONDITION_PATTERN.findall(conditions)
        )

    def parse_rule(rule: str) -> Rule:
        name, conditions, default = RULE_PATTERN.match(rule).groups()
        return name, parse_conditions(conditions) + [(None, default)]

    return {
        rule: conditions
        for rule, conditions in list(map(parse_rule, rules.splitlines()))
    }


def parse_parts(parts: str) -> list[dict[str, int]]:
    def parse_part(part: str):
        return {key: int(value) for key, value in PART_ITEM_PATTERN.findall(part)}

    return list(map(parse_part, parts.splitlines()))


def invert_predicate(predicate: Predicate) -> Predicate:
    key, op, value = predicate
    if op == operator.lt:
        return key, operator.gt, value - 1
    return key, operator.lt, value + 1


def part_one(file: TextIO) -> int:
    """
    Solve part one of the puzzle.
    """
    rules, parts = file.read().split("\n\n")
    rules = parse_rules(rules)
    parts = parse_parts(parts)

    def evaluate(rule: str, part: dict[str, int]) -> bool:
        if rule == "A":
            return True
        if rule == "R":
            return False

        for predicate, next_rule in rules[rule]:
            if predicate is None:
                return evaluate(next_rule, part)

            key, op, value = predicate
            if op(part[key], value):
                return evaluate(next_rule, part)

    return sum(sum(part.values()) for part in filter(partial(evaluate, "in"), parts))


def part_two(file: TextIO) -> int:
    """
    Solve part two of the puzzle.
    """
    rules, parts = file.read().split("\n\n")
    rules = parse_rules(rules)

    def evaluate_predicates(predicates: list[Predicate]) -> dict[str, tuple[int, int]]:
        valid_values = {str(key): (1, 4000) for key in "xmas"}
        for key, op, value in predicates:
            lo, hi = valid_values[key]
            match op:
                case operator.lt:
                    valid_values[key] = lo, min(hi, value - 1)
                case operator.gt:
                    valid_values[key] = max(lo, value + 1), hi
        return valid_values

    accepted: list[dict[str, tuple[int, int]]] = []

    def evaluate(current: list[Predicate], conditions: list[Condition]):
        predicate, next_rule = next(iter(conditions))

        if predicate is None:
            match next_rule:
                case "A":
                    accepted.append(evaluate_predicates(current))
                case _ if next_rule in rules:
                    evaluate(current, rules[next_rule])
            return

        match next_rule:
            case "R":
                evaluate(current + [invert_predicate(predicate)], conditions[1:])
            case "A":
                accepted.append(evaluate_predicates(current + [predicate]))
                evaluate(current + [invert_predicate(predicate)], conditions[1:])
            case _ if next_rule in rules:
                evaluate(current + [predicate], rules[next_rule])
                evaluate(current + [invert_predicate(predicate)], conditions[1:])

    evaluate([], rules["in"])

    return sum(
        math.prod(hi - lo + 1 for lo, hi in interval.values()) for interval in accepted
    )


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
