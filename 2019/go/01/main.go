package main

import (
	"log"
	"strconv"

	"github.com/sanderploegsma/advent-of-code/2019/utils"
)

func main() {
	lines, _ := utils.ReadLines("input.txt")
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
