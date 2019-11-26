package main

import (
	"io/ioutil"
	"log"
	"strings"
)

func main() {
	input, err := ioutil.ReadFile("input.txt")
	if err != nil {
		log.Fatalf("unable to read file: %v", err)
	}
	lines := strings.Split(string(input), "\n")
	log.Printf("checksum: %d\n", Checksum(lines))
}

// checksum calculates the checksum over the given list of `boxIds`.
func Checksum(boxIds []string) int {
	var x, y int

	for _, id := range boxIds {
		if hasNOfAnyLetter(id, 2) {
			x++
		}

		if hasNOfAnyLetter(id, 3) {
			y++
		}
	}

	return x * y
}

// hasNOfAnyLetter checks the given `boxId` for a character that occurs exactly `n` times.
func hasNOfAnyLetter(boxId string, n int) bool {
	var occurrences = make(map[rune]int)

	// Count occurrences of each character in the boxId
	for _, c := range boxId {
		occurrences[c]++
	}

	// Check the occurrences for a value that matches n
	for _, i := range occurrences {
		if i == n {
			return true
		}
	}

	return false
}
