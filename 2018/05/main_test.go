package main

import "testing"

func TestExample(t *testing.T) {
	input := "dabAcCaCBAcCcaDA"
	expected := "dabCBAcaDA"
	actual := ProcessPolymer(input)

	if actual != expected {
		t.Fatalf("expected '%s' but got '%s'", expected, actual)
	}
}
