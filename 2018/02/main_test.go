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

func TestExampleTwo(t *testing.T) {
	input := []string{
		"abcde",
		"fghij",
		"klmno",
		"pqrst",
		"fguij",
		"axcye",
		"wvxyz",
	}

	expected := "fgij"
	actual, ok := MatchBoxIDs(input)

	if !ok {
		t.Fatal("expected a match but got none")
	}

	if actual != expected {
		t.Fatalf("expected '%s' but got '%s'", expected, actual)
	}
}
