#!/usr/bin/env python

"""Generator for Advent of Code 2023 solutions."""

import os
import sys

from jinja2 import Environment

SCRIPT_DIR = os.path.join(os.path.dirname(__file__))
TEMPLATE_DIR = os.path.join(SCRIPT_DIR, "templates")
OUTPUT_DIR = os.path.join(SCRIPT_DIR, "..")


def generate(day: str) -> None:
    env = Environment(autoescape=False, optimized=False)

    def generate_file(template_name: str, output_name: str):
        with open(os.path.join(TEMPLATE_DIR, template_name), "r", encoding="utf-8") as f:
            template = env.from_string(f.read())

        with open(os.path.join(OUTPUT_DIR, output_name), "x", encoding="utf-8") as f:
            template.stream(day=day).dump(f)

    generate_file("input.txt.j2", f"day{day}.txt")
    generate_file("solution.py.j2", f"day{day}.py")
    generate_file("test.py.j2", f"day{day}_test.py")


def main():
    if len(sys.argv) < 2:
        print("Please supply a day number")
        exit(1)

    day = int(sys.argv[1])
    day_padded = f"{day:02}"

    generate(day_padded)


if __name__ == "__main__":
    main()
