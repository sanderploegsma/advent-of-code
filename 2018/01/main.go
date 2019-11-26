package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"strconv"
	"strings"
)

func main() {
	input, err := ioutil.ReadFile("input.txt")
	if err != nil {
		log.Fatalf("unable to read file: %v", err)
	}

	changes, err := parseInput(input)
	if err != nil {
		log.Fatalf("unable to parse input: %v", err)
	}

	log.Printf("final frequency: %d", frequency(changes))
	log.Printf("first frequency reached twice: %d", firstFrequencyReachedTwice(changes))
}

// parseInput tries to parse an input of the following form into a list of changes:
//
// +1
// -2
// +3
//
// The resulting list contains the changes as integers, with the sign denoting either an addition or subtraction.
func parseInput(input []byte) ([]int, error) {
	lines := strings.Split(string(input), "\n")
	result := make([]int, len(lines))

	for i, line := range lines {
		change, err := strconv.Atoi(line[1:])
		if err != nil {
			return nil, fmt.Errorf("failed to parse line %d: %v", i, err)
		}
		if strings.HasPrefix(line, "+") {
			result[i] = change
		} else {
			result[i] = change * -1
		}
	}

	return result, nil
}

// frequency calculates the resulting frequency after applying the given list of changes to an initial frequency of 0.
func frequency(changes []int) int {
	var frequency = 0
	for _, c := range changes {
		frequency += c
	}
	return frequency
}

// firstFrequencyReachedTwice applies the given list of changes until a frequency is reached twice.
func firstFrequencyReachedTwice(changes []int) int {
	var frequencies = make(map[int]bool)
	var frequency = 0
	var i = 0

	for {
		// mark the current frequency as reached
		frequencies[frequency] = true

		// calculate the next frequency
		frequency += changes[i]

		// if the frequency was previously reached, return it
		if frequencies[frequency] {
			return frequency
		}

		// Advance i to the next position of the list of changes, looping around
		if i+1 < len(changes) {
			i++
		} else {
			i = 0
		}
	}
}
