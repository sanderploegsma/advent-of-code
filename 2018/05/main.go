package main

import (
	"io/ioutil"
	"log"
	"unicode"
)

func main() {
	input, err := ioutil.ReadFile("input.txt")
	if err != nil {
		log.Fatalf("unable to read file: %v", err)
	}
	processed := ProcessPolymer(string(input))
	log.Printf("result: %s (length: %d)", processed, len(processed))
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
