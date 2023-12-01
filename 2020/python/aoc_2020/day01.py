from functools import reduce
from itertools import permutations
from operator import mul

input = open("2020/input/day01.txt").read()
numbers = [int(x) for x in input.splitlines()]


def find_numbers_with_total_sum(total, n):
    candidates = permutations(numbers, n)
    matches = filter(lambda x: sum(x) == total, candidates)
    return next(matches)


print("part one: %d" % reduce(mul, find_numbers_with_total_sum(2020, 2)))
print("part two: %d" % reduce(mul, find_numbers_with_total_sum(2020, 3)))
