module day_01

import os

const input_path = "../input/01.txt"

@[params]
struct Config {
    input ?string
}

fn part_one(config Config) !int {
    input := config.input or {
        os.read_file(input_path)!
    }

	return input.len
}

fn part_two(config Config) !int {
	input := config.input or {
        os.read_file(input_path)!
    }
    
	return input.len
}
