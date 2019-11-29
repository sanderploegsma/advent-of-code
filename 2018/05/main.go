package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"math"
	"regexp"
	"sort"
	"strings"
	"unicode"
)

func main() {
	input, err := ioutil.ReadFile("input.txt")
	if err != nil {
		log.Fatalf("unable to read file: %v", err)
	}
	processed := ProcessPolymer(string(input))
	log.Printf("[PART 1] result: %s (length: %d)\n", processed, len(processed))

	processed = ProcessPolymerAdvanced(string(input))
	log.Printf("[PART 2] result: %s (length: %d)\n", processed, len(processed))
}

// ProcessPolymer takes an input and removes pairs like 'aA' or 'Bb' until no further reactions occur. The resulting polymer is returned.
func ProcessPolymer(input string) string {
	didReact := true
	result := input

	for didReact == true {
		var previous rune
		for i, current := range result {
			// If the previous character matches the current only when we both convert them to lowercase, remove both previous and current from result and start from the beginning
			if i > 0 && current != previous && unicode.ToLower(current) == unicode.ToLower(previous) {
				result = result[0:i-1] + result[i+1:]
				break
			}

			// If we reached the end of result, no reactions are possible
			if i == len(result)-1 {
				didReact = false
			}

			// Save the current character to compare with the next
			previous = current
		}
	}

	return result
}

// ProcessPolymerAdvanced takes an input polymer and looks for the shortest remaining polymer by performing the same reaction on pairs like 'aA' or 'Bb', but this time by removing one unit pair from the polymer first.
func ProcessPolymerAdvanced(input string) string {
	results := make([]string, 0)
	for _, c := range getCharsInString(input) {
		r := regexp.MustCompile(fmt.Sprintf("(?i)%c", c))
		reduced := r.ReplaceAllString(input, "")
		results = append(results, ProcessPolymer(reduced))
	}

	min := math.MaxInt32
	minVal := ""
	for _, r := range results {
		if len(r) < min {
			min = len(r)
			minVal = r
		}
	}
	return minVal
}

// getCharsInString returns the set of unique characters in the given string
func getCharsInString(input string) []rune {
	// Create a list of all characters in the string
	var chars []rune
	for _, c := range strings.ToLower(input) {
		chars = append(chars, c)
	}

	// Sort all characters
	sort.Slice(chars, func(i, j int) bool {
		return chars[i] < chars[j]
	})

	// Create a list of unique characters
	result := make([]rune, 0)
	for _, c := range chars {
		if len(result) == 0 || result[len(result)-1] != c {
			result = append(result, c)
		}
	}
	return result
}
