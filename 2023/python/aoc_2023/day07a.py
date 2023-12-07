"""Advent of Code 2023 - Day 7."""

import collections
import functools
from aoc_2023.input import Input

card_order = "23456789TJQKA"

def compare_hands(hand1, hand2):
    cards1 = collections.Counter(hand1[0])
    cards2 = collections.Counter(hand2[0])

    if len(cards1.keys()) != len(cards2.keys()):
        return -1 * (len(cards1.keys()) - len(cards2.keys()))

    if max(cards1.values()) != max(cards2.values()):
        return max(cards1.values()) - max(cards2.values())

    for c1, c2 in zip(hand1[0], hand2[0]):
        if c1 == c2:
            continue

        i1, i2 = map(card_order.find, (c1, c2))
        return i1 - i2

    return 0


hands = map(lambda s: s.split(), Input("07.txt").lines)
ordered = sorted(hands, key=functools.cmp_to_key(compare_hands))

print(ordered)

total = 0
for i, (_, bid) in enumerate(ordered):
    total += int(bid) * (i + 1)

print("Part one:", total)
