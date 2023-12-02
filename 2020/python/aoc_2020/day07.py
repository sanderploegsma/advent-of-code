"""Advent Of Code 2020 Day 7."""

from collections import deque

Rule = tuple[str, dict[str, int]]


def parse_line(line: str) -> Rule:
    def parse_color(bags: str):
        return bags.rsplit(" ", maxsplit=1)[0]

    def parse_child(child: str):
        [quantity, bag] = child.split(" ", maxsplit=1)
        return parse_color(bag), int(quantity)

    def parse_children(children: str) -> dict[str, int]:
        if "no other bags" in children:
            return {}

        return dict(parse_child(child) for child in children.split(", "))

    [parent, children] = line.split(" contain ")
    return parse_color(parent), parse_children(children)


def part_one(rules: list[Rule]):
    """
    Count the number of bags that can contain the "shiny gold" bag.
    """
    bags = set()
    queue = deque(["shiny gold"])

    while len(queue) > 0:
        current_color = queue.pop()
        colors = [color for color, children in rules if current_color in children]

        for color in colors:
            if color not in bags:
                bags.add(color)
                queue.append(color)

    return len(bags)


def part_two(rules: list[Rule]):
    """
    Count the total number of bags contained by the "shiny gold" bag.
    """

    def count_children(color: str):
        children = next(children for c, children in rules if c == color)
        return sum([q + q * count_children(c) for c, q in children.items()])

    return count_children("shiny gold")


with open("../input/07.txt", encoding="utf-8") as file:
    input = [parse_line(line) for line in file.readlines()]
    print("Part one:", part_one(input))
    print("Part two:", part_two(input))
