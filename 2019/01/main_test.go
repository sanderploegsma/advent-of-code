package main

import (
	"fmt"
	"testing"
)

func TestPartOne(t *testing.T) {
	tests := []struct {
		mass string
		fuel int
	}{
		{"12", 2},
		{"14", 2},
		{"1969", 654},
		{"100756", 33583},
	}

	for _, test := range tests {
		t.Run(fmt.Sprintf("mass of %s requires %d fuel", test.mass, test.fuel), func(t *testing.T) {
			actual := PartOne([]string{test.mass})
			if actual != test.fuel {
				t.Errorf("expected %d but got %d", test.fuel, actual)
			}
		})
	}
}

func TestPartTwo(t *testing.T) {
	tests := []struct {
		mass string
		fuel int
	}{
		{"14", 2},
		{"1969", 966},
		{"100756", 50346},
	}

	for _, test := range tests {
		t.Run(fmt.Sprintf("mass of %s requires %d fuel", test.mass, test.fuel), func(t *testing.T) {
			actual := PartTwo([]string{test.mass})
			if actual != test.fuel {
				t.Errorf("expected %d but got %d", test.fuel, actual)
			}
		})
	}
}
