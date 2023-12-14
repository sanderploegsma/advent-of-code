"""Advent of Code 2023 - Day 07."""

import collections
import sys
from typing import TextIO


def total(ordered_hands: list[list[str]]) -> int:
    return sum(int(bid) * rank for rank, (_, bid) in enumerate(ordered_hands, start=1))


def part_one(file: TextIO) -> int:
    def sort_hand(hand: list[str]) -> list[int]:
        cards = hand[0]
        card_counts = collections.Counter(cards)
        max_count = max(card_counts.values())
        card_values = list(map("23456789TJQKA".find, cards))

        return [-len(card_counts), max_count, *card_values]

    hands = list(map(lambda s: s.split(), file))
    return total(sorted(hands, key=sort_hand))


def part_two(file: TextIO) -> int:
    def sort_hand(hand: list[str]) -> list[int]:
        cards = hand[0]
        card_counts = collections.Counter(cards.replace("J", "") if cards != "JJJJJ" else cards)
        max_count = max(card_counts.values(), default=0) + len(cards) - card_counts.total()
        card_values = list(map("J23456789TQKA".find, cards))

        return [-len(card_counts), max_count, *card_values]

    hands = list(map(lambda s: s.split(), file))
    return total(sorted(hands, key=sort_hand))


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
