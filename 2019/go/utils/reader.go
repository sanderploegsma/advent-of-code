package utils

import (
	"fmt"
	"io/ioutil"
	"strconv"
	"strings"
)

// ReadFile reads the given file and returns the content split by a newline
func ReadLines(file string) ([]string, error) {
	return ReadDelim(file, "\n")
}

// ReadFile reads the given file and returns the content split by the given delimiter
func ReadDelim(file, delim string) ([]string, error) {
	c, err := ReadFile(file)
	if err != nil {
		return nil, err
	}
	return strings.Split(c, delim), nil
}

// ReadFile reads the given file and returns the contents as a string
func ReadFile(file string) (string, error) {
	c, err := ioutil.ReadFile(file)
	if err != nil {
		return "", fmt.Errorf("failed to read file %s: %v", file, err)
	}
	return strings.TrimSpace(string(c)), nil
}

// ReadIntCode reads the given file and parses the contents as IntCode instructions
func ReadIntCode(file string) ([]int, error) {
	input, err := ReadDelim(file, ",")
	if err != nil {
		return nil, fmt.Errorf("failed to read file %s: %w", file, err)
	}

	instructions := make([]int, len(input))
	for i := range input {
		instructions[i], err = strconv.Atoi(strings.TrimSpace(input[i]))
		if err != nil {
			return nil, fmt.Errorf("unable to parse '%s' as int: %w", input[i], err)
		}
	}

	return instructions, nil
}
