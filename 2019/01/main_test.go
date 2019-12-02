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
			actual := CalculateRequiredFuel([]string{test.mass})
			if actual != test.fuel {
				t.Errorf("expected %d but got %d", test.fuel, actual)
			}
		})
	}
}
