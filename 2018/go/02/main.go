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

	match, ok := MatchBoxIDs(lines)
	if ok {
		log.Printf("match: %s\n", match)
	} else {
		log.Println("no match")
	}
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

// MatchBoxIDs looks for a pair of box IDs in the given list that differ by exactly one character at the same position. It returns the boxID with that one character removed, if found.
func MatchBoxIDs(boxIDs []string) (string, bool) {
	if len(boxIDs) == 0 {
		return "", false
	}

	length := len(boxIDs[0])
	for pos := 0; pos < length; pos++ {
		altered := make([]string, len(boxIDs))

		for i, boxID := range boxIDs {
			altered[i] = removeCharacterAt(boxID, pos)
		}

		if found, ok := findDuplicate(altered); ok {
			return found, true
		}
	}

	return "", false
}

// findDuplicate looks for a pair of strings in the given list that are equal. It returns the duplicate string if found.
func findDuplicate(input []string) (string, bool) {
	var visited = make(map[string]bool)
	for _, item := range input {
		if visited[item] {
			return item, true
		}
		visited[item] = true
	}
	return "", false
}

// removeCharacterAt takes an input string and removes the character at the given position. The result of the operation is returned
func removeCharacterAt(input string, position int) string {
	length := len(input)

	if position == 0 {
		return input[1:]
	}

	if position == len(input)-1 {
		return input[0 : length-1]
	}

	return input[0:position] + input[position+1:]
}
