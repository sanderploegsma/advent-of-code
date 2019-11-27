package main

import "testing"

func TestExample(t *testing.T) {
	input := []string{
		"#1 @ 1,3: 4x4",
		"#2 @ 3,1: 4x4",
		"#3 @ 5,5: 2x2",
	}

	expected := 4
	actual, err := FindOverlaps(input)

	if err != nil {
		t.Fatalf("expected no errors but got %v", err)
	}

	if actual != expected {
		t.Fatalf("expected %d but got %d", expected, actual)
	}
}
