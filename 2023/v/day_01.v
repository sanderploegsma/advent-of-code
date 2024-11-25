module day_01

import arrays
import os

const input_path = 'day_01.txt'

@[params]
struct Config {
	input ?string
}

fn part_one(config Config) !int {
	input := config.input or { os.read_file(input_path)! }

	mut result := 0

	for line in input.split_into_lines() {
		digits := line.runes()
			.map(it.str())
			.filter(it.is_int())
			.map(it.int())

		result += digits.first() * 10 + digits.last()
	}

	return result
}

fn part_two(config Config) !int {
	input := config.input or { os.read_file(input_path)! }

	mapping := {
		'1':     1
		'one':   1
		'2':     2
		'two':   2
		'3':     3
		'three': 3
		'4':     4
		'four':  4
		'5':     5
		'five':  5
		'6':     6
		'six':   6
		'7':     7
		'seven': 7
		'8':     8
		'eight': 8
		'9':     9
		'nine':  9
	}

	mut result := 0

	for line in input.split_into_lines() {
		mut left_idx := line.len
		mut left_value := 0
		mut right_idx := -1
		mut right_value := 0

		for key, value in mapping {
			if idx := line.index(key) {
				if idx < left_idx {
					left_idx = idx
					left_value = value
				}
			}

			if idx := line.last_index(key) {
				if idx > right_idx {
					right_idx = idx
					right_value = value
				}
			}
		}

		result += left_value * 10 + right_value
	}

	return result
}
