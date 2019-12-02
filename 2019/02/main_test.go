package main

import (
	"fmt"
	"github.com/stretchr/testify/assert"
	"testing"
)

func TestPartOne(t *testing.T) {
	tests := []struct {
		input    []int
		expected []int
	}{
		{
			input:    []int{1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50},
			expected: []int{3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50},
		},
		{
			input:    []int{1, 0, 0, 0, 99},
			expected: []int{2, 0, 0, 0, 99},
		},
		{
			input:    []int{2, 3, 0, 3, 99},
			expected: []int{2, 3, 0, 6, 99},
		},
		{
			input:    []int{2, 4, 4, 5, 99, 0},
			expected: []int{2, 4, 4, 5, 99, 9801},
		},
		{
			input:    []int{1, 1, 1, 4, 99, 5, 6, 0, 99},
			expected: []int{30, 1, 1, 4, 2, 5, 6, 0, 99},
		},
	}

	for _, test := range tests {
		t.Run(fmt.Sprintf("program %v yields output %v", test.input, test.expected), func(t *testing.T) {
			assert.EqualValues(t, test.expected, PartOne(test.input))
		})
	}
}
