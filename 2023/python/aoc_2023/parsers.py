"""Commonly used parsing functions."""

from typing import Iterable


def parse_blocks(data: str) -> list[str]:
    return data.split("\n\n")


def parse_ints(data: Iterable[str]) -> list[int]:
    return list(map(int, data))


def parse_int_lists(data: Iterable[str]) -> list[list[int]]:
    return list(list(map(int, line.strip().split())) for line in data)
