package main

import (
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
	lines := strings.Split(string(input), "\n")
	log.Printf("[PART ONE] fuel requirements: %d", PartOne(lines))
	log.Printf("[PART TWO] fuel requirements: %d", PartTwo(lines))
}

func PartOne(input []string) int {
	fuel := 0
	for _, module := range input {
		mass, _ := strconv.Atoi(module)
		fuel += mass/3 - 2
	}

	return fuel
}

func PartTwo(input []string) int {
	fuel := 0
	for _, module := range input {
		mass, _ := strconv.Atoi(module)
		fuel += CalculateFuel(mass)
	}

	return fuel
}

// CalculateFuel calculates the total fuel required for the given mass recursively until it reaches zero.
func CalculateFuel(mass int) int {
	fuel := mass/3 - 2
	if fuel < 0 {
		return 0
	}

	return fuel + CalculateFuel(fuel)
}
