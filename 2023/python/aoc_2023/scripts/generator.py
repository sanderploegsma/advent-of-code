#!/usr/bin/env python

"""
Generator for Advent of Code 2023 solutions.
"""

import os
import sys

from jinja2 import Environment

SCRIPT_DIR = os.path.join(os.path.dirname(__file__))
TEMPLATE_DIR = os.path.join(SCRIPT_DIR, "templates")
OUTPUT_DIR = os.path.join(SCRIPT_DIR, "..")


def generate(**kwargs) -> None:
    env = Environment(autoescape=False, optimized=False)

    def generate_file(template_name: str, output_name: str):
        with open(os.path.join(TEMPLATE_DIR, template_name), "r", encoding="utf-8") as f:
            template = env.from_string(f.read())

        with open(os.path.join(OUTPUT_DIR, output_name), "x", encoding="utf-8") as f:
            template.stream(**kwargs).dump(f)

    day = kwargs["day"]
    generate_file("input.txt.j2", f"day{day}.txt")
    generate_file("solution.py.j2", f"day{day}.py")
    generate_file("test.py.j2", f"day{day}_test.py")


def main():
    if len(sys.argv) < 3:
        print("Usage: generate <day> <title of puzzle>")
        exit(1)

    generate(day=f"{int(sys.argv[1]):02}", title=sys.argv[2])


if __name__ == "__main__":
    main()
