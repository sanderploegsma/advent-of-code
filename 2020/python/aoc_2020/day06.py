"""Advent of Code 2020 - day 06."""

import os
from typing import List


def union(group: List[List[str]]) -> List[str]:
    result = []
    for answers in group:
        result = set(result) | set(answers)

    return result


def intersect(group: List[List[str]]) -> List[str]:
    result = union(group)

    for answers in group:
        result = set(result) & set(answers)

    return result


with open('2020/input/day06.txt', encoding='utf8') as file:
    groups = [group.splitlines()
              for group in file.read().split(os.linesep + os.linesep)]

    print(f"part one: {sum(len(union(group)) for group in groups)}")
    print(f"part two: {sum(len(intersect(group)) for group in groups)}")
