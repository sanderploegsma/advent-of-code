"""Advent of Code 2023 - Day 02."""

import sys
from typing import TextIO

RGB = tuple[int, int, int]
Game = tuple[int, list[RGB]]


def parse_rgb(rgb: str) -> RGB:
    r, g, b = 0, 0, 0
    for value in rgb.split(", "):
        n, color = value.split(" ")
        match color:
            case "red":
                r = int(n)
            case "green":
                g = int(n)
            case "blue":
                b = int(n)

    return r, g, b


def parse_game(line: str) -> Game:
    id, subsets = line.split(": ")
    return int(id.split(" ")[-1]), [parse_rgb(x) for x in subsets.split("; ")]


def part_one(file: TextIO) -> int:
    def is_possible(game: Game) -> bool:
        return all(r <= 12 and g <= 13 and b <= 14 for r, g, b in game[1])

    games = list(parse_game(line.strip()) for line in file)
    return sum(game[0] for game in games if is_possible(game))


def part_two(file: TextIO) -> int:
    def power(game: Game) -> int:
        r_max, g_max, b_max = 0, 0, 0
        for r, g, b in game[1]:
            r_max = max(r_max, r)
            g_max = max(g_max, g)
            b_max = max(b_max, b)
        return r_max * g_max * b_max

    games = list(parse_game(line.strip()) for line in file)
    return sum(map(power, games))


def main():
    filename = sys.argv[0].replace(".py", ".txt")

    with open(filename, encoding="utf-8") as file:
        print("Part one:", part_one(file))

    with open(filename, encoding="utf-8") as file:
        print("Part two:", part_two(file))


if __name__ == "__main__":
    main()
