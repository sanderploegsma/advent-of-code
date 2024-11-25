module day_01

import os

const input_path = '../input/01.txt'

fn test_part_one_example() {
	input := 'Hello, World!'
	assert part_one(input) == 13
}

fn test_part_one() ! {
	input := os.read_file(input_path)!
	assert part_one(input) == 28
}
