from functools import reduce
from operator import mul

input = open("../input/day03.txt").read().splitlines()


def traverse(step_x, step_y):
    """
    Traverses the input using the given step sizes and counts the number of trees on the traversed path.
    """
    x, y, trees = 0, 0, 0

    while y < len(input):
        line = input[y]
        if line[x % len(line)] == "#":
            trees += 1
        x += step_x
        y += step_y

    return trees


print("part one: %d" % traverse(3, 1))
print(
    "part two: %d"
    % reduce(
        mul, [traverse(*step) for step in [(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)]]
    )
)
