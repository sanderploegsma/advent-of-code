"""Advent of Code 2020 - day 05."""

from typing import Tuple


def find_row(seat: str, min_row: int = 0, max_row: int = 127) -> int:
    """Recursively finds the row of the given seat."""
    mid = max_row - int((max_row - min_row) / 2)

    if seat.startswith('F'):
        return find_row(seat[1:], min_row=min_row, max_row=mid)

    if seat.startswith('B'):
        return find_row(seat[1:], min_row=mid, max_row=max_row)

    return min_row


def find_column(seat: str, min_column: int = 0, max_column: int = 7) -> int:
    """Recursively finds the column of the given seat."""
    mid = max_column - int((max_column - min_column) / 2)

    if seat.startswith('L'):
        return find_column(seat[1:], min_column=min_column, max_column=mid)

    if seat.startswith('R'):
        return find_column(seat[1:], min_column=mid, max_column=max_column)

    return min_column


def find_seat_location(seat: str) -> Tuple[int, int]:
    """Finds the row and column number of the given seat."""
    return find_row(seat[:7]), find_column(seat[7:])


with open('2020/input/day05.txt', encoding='utf8') as file:
    seats = [find_seat_location(line) for line in file.read().splitlines()]
    seat_ids = [row * 8 + column for row, column in seats]
    max_seat = max(seat_ids)

    print(f"part one: {max_seat}")

    min_seat = min(seat_ids)
    for seat_id in range(min_seat, max_seat):
        if seat_id not in seat_ids:
            print(f"part two: {seat_id}")
