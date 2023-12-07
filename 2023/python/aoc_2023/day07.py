"""Advent of Code 2023 - Day 7."""

import collections
from aoc_2023.input import Input

card_order = "23456789TJQKA"
card_order_2 = "J23456789TQKA"


def sort_hand(hand: list[str]) -> list[int]:
    cards = hand[0]
    card_counts = collections.Counter(cards)
    max_count = max(card_counts.values())
    card_values = list(map(card_order.find, cards))

    return [-len(card_counts), max_count, *card_values]


def sort_hand_with_jokers(hand: list[str]) -> list[int]:
    cards = hand[0]
    card_counts = collections.Counter(cards.replace("J", "") if cards != "JJJJJ" else cards)
    max_count = max(card_counts.values(), default=0) + len(cards) - card_counts.total()
    card_values = list(map(card_order_2.find, cards))

    return [-len(card_counts), max_count, *card_values]


def total(ordered_hands: list[list[str]]) -> int:
    return sum(int(bid) * rank for rank, (_, bid) in enumerate(ordered_hands, start=1))


hands = list(map(lambda s: s.split(), Input("07.txt").lines))

print("Part one:", total(sorted(hands, key=sort_hand)))
print("Part two:", total(sorted(hands, key=sort_hand_with_jokers)))
