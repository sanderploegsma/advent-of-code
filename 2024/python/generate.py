#!/usr/bin/env python

"""
Generator for Advent of Code 2024 solutions.
"""

import argparse
import os

from jinja2 import Environment

SCRIPT_DIR = os.path.join(os.path.dirname(__file__))
TEMPLATE_DIR = os.path.join(SCRIPT_DIR, "templates")
OUTPUT_DIR = SCRIPT_DIR


def generate(day: int, title: str):
    env = Environment(autoescape=False, optimized=False)

    def generate_file(template_name: str, output_name: str):
        with open(
            os.path.join(TEMPLATE_DIR, template_name), "r", encoding="utf-8"
        ) as f:
            template = env.from_string(f.read())

        with open(os.path.join(OUTPUT_DIR, output_name), "x", encoding="utf-8") as f:
            template.stream(day=day, title=title).dump(f)

    generate_file("input.txt.j2", f"day{day:02}.txt")
    generate_file("solution.py.j2", f"day{day:02}.py")
    generate_file("test.py.j2", f"day{day:02}_test.py")


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("day", type=int)
    parser.add_argument("title", type=str)
    args = parser.parse_args()

    generate(day=args.day, title=args.title)


if __name__ == "__main__":
    main()
