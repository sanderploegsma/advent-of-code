package main

import "testing"

import "fmt"

type testCase struct {
	changes  []int
	expected int
}

func TestPart1Examples(t *testing.T) {
	cases := []testCase{
		{
			changes:  []int{1, -2, 3, 1},
			expected: 3,
		},
		{
			changes:  []int{1, 1, 1},
			expected: 3,
		},
		{
			changes:  []int{1, 1, -2},
			expected: 0,
		},
		{
			changes:  []int{-1, -2, -3},
			expected: -6,
		},
	}

	for _, c := range cases {
		t.Run(fmt.Sprint(c.changes), func(t *testing.T) {
			actual := frequency(c.changes)
			if actual != c.expected {
				t.Fatalf("expected %d but got %d", c.expected, actual)
			}
		})
	}
}

func TestPart2Example(t *testing.T) {
	cases := []testCase{
		{
			changes:  []int{1, -1},
			expected: 0,
		},
		{
			changes:  []int{3, 3, 4, -2, -4},
			expected: 10,
		},
		{
			changes:  []int{-6, 3, 8, 5, -6},
			expected: 5,
		},
		{
			changes:  []int{7, 7, -2, -7, -4},
			expected: 14,
		},
	}

	for _, c := range cases {
		t.Run(fmt.Sprint(c.changes), func(t *testing.T) {
			actual := firstFrequencyReachedTwice(c.changes)
			if actual != c.expected {
				t.Fatalf("expected %d but got %d", c.expected, actual)
			}
		})
	}
}
