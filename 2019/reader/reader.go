package reader

import (
	"fmt"
	"io/ioutil"
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
	return string(c), nil
}
