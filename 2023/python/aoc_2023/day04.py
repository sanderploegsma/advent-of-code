"""Advent of Code 2023 - Day 4."""
import re
from collections import deque

from aoc_2023.input import Input

Card = tuple[int, list[int], list[int]]


def parse(line: str) -> Card:
    id, winners, mine = line.replace(":", "|").split("|")
    return (
        int(id.removeprefix("Card ")),
        [int(n) for n in re.findall(r"\d+", winners)],
        [int(n) for n in re.findall(r"\d+", mine)],
    )


def points(card: Card) -> int:
    winners = list(filter(lambda n: n in card[1], card[2]))
    return 0 if len(winners) == 0 else 1 << (len(winners) - 1)


cards = [parse(line) for line in Input("04.txt").lines]

print("Part one:", sum(points(card) for card in cards))

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

print("Part two:", len(processed))
