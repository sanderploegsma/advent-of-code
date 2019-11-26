package main

import "testing"

func TestExample(t *testing.T) {
	input := []string{
		"abcdef",
		"bababc",
		"abbcde",
		"abcccd",
		"aabcdd",
		"abcdee",
		"ababab",
	}

	expected := 12
	actual := Checksum(input)

	if actual != expected {
		t.Fatalf("expected %d but got %d", expected, actual)
	}
}
