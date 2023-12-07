"""Advent of Code 2023 - Day 7."""

import collections
import functools
from aoc_2023.input import Input

card_order = "J23456789TQKA"

def compare_hands(hand1, hand2):
    (cards1, _), (cards2, _) = hand1, hand2
    counts1 = collections.Counter(cards1.replace("J", "") if cards1 != "JJJJJ" else cards1)
    counts2 = collections.Counter(cards2.replace("J", "") if cards2 != "JJJJJ" else cards2)

    if len(counts1.keys()) != len(counts2.keys()):
        return -1 * (len(counts1.keys()) - len(counts2.keys()))

    max_count1 = max(counts1.values(), default=0) + len(cards1) - counts1.total()
    max_count2 = max(counts2.values(), default=0) + len(cards2) - counts2.total()
    if max_count1 != max_count2:
        return max_count1 - max_count2

    for c1, c2 in zip(cards1, cards2):
        if c1 == c2:
            continue

        i1, i2 = map(card_order.find, (c1, c2))
        return i1 - i2

    return 0


hands = map(lambda s: s.split(), Input("07.txt").lines)
ordered = sorted(hands, key=functools.cmp_to_key(compare_hands))

total = 0
for i, (hand, bid) in enumerate(ordered):
    print(hand)
    total += int(bid) * (i + 1)

print("Part two:", total)
