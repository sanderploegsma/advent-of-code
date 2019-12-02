package main

import (
	"io/ioutil"
	"log"
	"strconv"
	"strings"
)

func main() {
	input, err := parseInput("input.txt")
	if err != nil {
		log.Fatalf("unable to read file: %v", err)
	}

	// Replace position 1 with the value 12
	input[1] = 12
	// Replace position 2 with the value 2
	input[2] = 2

	log.Printf("[PART ONE] result: %v", PartOne(input))
}

func PartOne(input []int) []int {
	result := make([]int, len(input))
	for i := range input {
		result[i] = input[i]
	}

	index := 0
	for index <= len(result)-1 {
		opcode := result[index]

		if opcode == 99 {
			break
		}

		x := result[result[index+1]]
		y := result[result[index+2]]
		pos := result[index+3]

		if opcode == 1 {
			result[pos] = x + y
		}

		if opcode == 2 {
			result[pos] = x * y
		}

		index += 4
	}

	return result
}

func parseInput(file string) (input []int, err error) {
	text, err := ioutil.ReadFile(file)
	if err != nil {
		return nil, err
	}

	for _, item := range strings.Split(string(text), ",") {
		val, err := strconv.Atoi(item)
		if err != nil {
			return nil, err
		}
		input = append(input, val)
	}

	return input, err
}
