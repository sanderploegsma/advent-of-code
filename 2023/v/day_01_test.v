module day_01

import os

const input_path = 'day_01.txt'

const example_one = '
1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet
'

fn test_part_one_example() {
	input := example_one.trim_space().split_into_lines()
	assert part_one(input) == 142
}

fn test_part_one() ! {
	input := os.read_lines(input_path)!
	assert part_one(input) == 55712
}

const example_two = '
two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen
'

fn test_part_two_example() {
	input := example_two.trim_space().split_into_lines()
	assert part_two(input) == 281
}

fn test_part_two() ! {
	input := os.read_lines(input_path)!
	assert part_two(input) == 55413
}
