package main

import (
	"fmt"
	"testing"
)

func TestExampleOne(t *testing.T) {
	tests := []struct {
		line1    string
		line2    string
		expected int
	}{
		{
			line1:    "R75,D30,R83,U83,L12,D49,R71,U7,L72",
			line2:    "U62,R66,U55,R34,D71,R55,D58,R83",
			expected: 159,
		},
		{
			line1:    "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
			line2:    "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7",
			expected: 135,
		},
	}

	for i, test := range tests {
		t.Run(fmt.Sprintf("example %d", i+1), func(t *testing.T) {
			actual := DistanceToClosestIntersection(test.line1, test.line2)
			if actual != test.expected {
				t.Errorf("expected %d but got %d", test.expected, actual)
			}
		})
	}
}

func TestExampleTwo(t *testing.T) {
	tests := []struct {
		line1    string
		line2    string
		expected int
	}{
		{
			line1:    "R75,D30,R83,U83,L12,D49,R71,U7,L72",
			line2:    "U62,R66,U55,R34,D71,R55,D58,R83",
			expected: 610,
		},
		{
			line1:    "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
			line2:    "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7",
			expected: 410,
		},
	}

	for i, test := range tests {
		t.Run(fmt.Sprintf("example %d", i+1), func(t *testing.T) {
			actual := FewestStepsToIntersection(test.line1, test.line2)
			if actual != test.expected {
				t.Errorf("expected %d but got %d", test.expected, actual)
			}
		})
	}
}
