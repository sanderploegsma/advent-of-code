"""Advent of Code 2023 - Day 04."""

import re
import sys
from collections import deque
from typing import TextIO

Card = tuple[int, list[int], list[int]]


def parse(line: str) -> Card:
    id, winners, mine = line.replace(":", "|").split("|")
    return (
        int(id.removeprefix("Card ")),
        [int(n) for n in re.findall(r"\d+", winners)],
        [int(n) for n in re.findall(r"\d+", mine)],
    )


def part_one(file: TextIO) -> int:
    def points(card: Card) -> int:
        winners = list(filter(lambda n: n in card[1], card[2]))
        return 0 if len(winners) == 0 else 1 << (len(winners) - 1)

    return sum(map(points, map(parse, file)))


def part_two(file: TextIO) -> int:
    cards = list(map(parse, file))
    processed = []
    queue = deque(cards)

    while len(queue) > 0:
        card = queue.popleft()
        matching_cards = list(filter(lambda n: n in card[1], card[2]))
        for i in range(len(matching_cards)):
            idx = card[0] + i
            if idx < len(cards):
                queue.append(cards[idx])
        processed.append(card[0])

    return len(processed)


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
