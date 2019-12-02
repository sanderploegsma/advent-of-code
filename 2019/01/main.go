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
	log.Printf("fuel requirements: %d", CalculateRequiredFuel(lines))
}

func CalculateRequiredFuel(input []string) int {
	fuel := 0
	for _, module := range input {
		mass, _ := strconv.Atoi(module)
		fuel += mass/3 - 2
	}

	return fuel
}
