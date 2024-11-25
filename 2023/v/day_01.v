module day_01

fn part_one(input []string) int {
	mut result := 0

	for line in input {
		digits := line.runes()
			.map(it.str())
			.filter(it.is_int())
			.map(it.int())

		result += digits.first() * 10 + digits.last()
	}

	return result
}

fn part_two(input []string) int {
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

	for line in input {
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
