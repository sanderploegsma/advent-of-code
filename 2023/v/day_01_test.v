module day_01

import os

const example_one = '
1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet
'

fn test_part_one_example() ! {
	assert part_one(input: example_one.trim_space())! == 142
}

fn test_part_one() ! {
	assert part_one()! == 55712
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

fn test_part_two_example() ! {
	assert part_two(input: example_two.trim_space())! == 281
}

fn test_part_two() {
	assert part_two()! == 55413
}
