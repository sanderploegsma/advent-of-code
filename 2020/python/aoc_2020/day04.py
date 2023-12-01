"""
Advent of Code 2020 - day 04.
"""

import os
import re
from typing import Dict, Union


def parse_int(text: str) -> Union[int, None]:
    try:
        return int(text)
    except ValueError:
        return None


class Passport:
    def __init__(self, fields: Dict[str, str]) -> None:
        self.fields = fields

    @property
    def has_required_fields(self) -> bool:
        """Checks whether all required fields are present"""
        required = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"]
        for key in required:
            if key not in self.fields:
                return False

        return True

    @property
    def has_valid_fields(self) -> bool:
        """Checks whether the required fields are valid according to the validation rules"""
        return (
            self.has_required_fields
            and self.has_valid_birth_year
            and self.has_valid_issue_year
            and self.has_valid_expiration_year
            and self.has_valid_height
            and self.has_valid_hair_color
            and self.has_valid_eye_color
            and self.has_valid_passport_id
        )

    @property
    def has_valid_birth_year(self) -> bool:
        """Birth Year should be at least 1920 and at most 2002"""
        return parse_int(self.fields["byr"]) in range(1920, 2003)

    @property
    def has_valid_issue_year(self) -> bool:
        """Issue Year should be at least 2010 and at most 2020"""
        return parse_int(self.fields["iyr"]) in range(2010, 2021)

    @property
    def has_valid_expiration_year(self) -> bool:
        """Expiration Year should be at least 2020 and at most 2030"""
        return parse_int(self.fields["eyr"]) in range(2020, 2031)

    @property
    def has_valid_height(self) -> bool:
        """Height should be at least 150cm and at most 193cm; or at least 59in and at most 76in"""
        height = self.fields["hgt"]
        if height.endswith("cm"):
            return parse_int(height.strip("cm")) in range(150, 194)

        if height.endswith("in"):
            return parse_int(height.strip("in")) in range(59, 77)

        return False

    @property
    def has_valid_hair_color(self) -> bool:
        """Hair Color should contain a valid hexadecimal number"""
        if re.match("^#[0-9a-f]{6}$", self.fields["hcl"]):
            return True
        return False

    @property
    def has_valid_eye_color(self) -> bool:
        """Eye color should be one of the defined values"""
        return self.fields["ecl"] in ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"]

    @property
    def has_valid_passport_id(self) -> bool:
        """Passport ID should be a nine-digit number, including leading zeroes"""
        return len(self.fields["pid"]) == 9 and self.fields["pid"].isdigit()


def parse_passport(data: str) -> Passport:
    pairs = [pair.split(":") for pair in data.split()]
    fields = dict(pairs)
    return Passport(fields)


with open("2020/input/day04.txt", encoding="utf8") as file:
    passport_data = file.read().split(os.linesep + os.linesep)
    passports = [parse_passport(lines) for lines in passport_data]

print(f"part one: {len([p for p in passports if p.has_required_fields])}")
print(f"part two: {len([p for p in passports if p.has_valid_fields])}")
