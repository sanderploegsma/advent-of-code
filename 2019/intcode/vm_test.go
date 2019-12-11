package intcode

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestExamplesDay1(t *testing.T) {
	tests := []struct {
		input    []int
		expected map[int]int
	}{
		{
			input:    []int{1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50},
			expected: map[int]int{0: 3500, 1: 9, 2: 10, 3: 70, 4: 2, 5: 3, 6: 11, 7: 0, 8: 99, 9: 30, 10: 40, 11: 50},
		},
		{
			input:    []int{1, 0, 0, 0, 99},
			expected: map[int]int{0: 2, 1: 0, 2: 0, 3: 0, 4: 99},
		},
		{
			input:    []int{2, 3, 0, 3, 99},
			expected: map[int]int{0: 2, 1: 3, 2: 0, 3: 6, 4: 99},
		},
		{
			input:    []int{2, 4, 4, 5, 99, 0},
			expected: map[int]int{0: 2, 1: 4, 2: 4, 3: 5, 4: 99, 5: 9801},
		},
		{
			input:    []int{1, 1, 1, 4, 99, 5, 6, 0, 99},
			expected: map[int]int{0: 30, 1: 1, 2: 1, 3: 4, 4: 2, 5: 5, 6: 6, 7: 0, 8: 99},
		},
	}

	for _, test := range tests {
		c := NewVM(make(chan int), make(chan int), test.input)
		c.Run()
		assert.EqualValues(t, test.expected, c.memory)
	}
}

func TestExamplesDay9(t *testing.T) {
	tests := []struct {
		instructions []int
		input        []int
		expected     []int
	}{
		{
			instructions: []int{109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99},
			input:        []int{},
			expected:     []int{109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99},
		},
		{
			instructions: []int{104, 1125899906842624, 99},
			input:        []int{},
			expected:     []int{1125899906842624},
		},
	}

	for i := range tests {
		in := make(chan int, len(tests[i].input))
		out := make(chan int)
		c := NewVM(in, out, tests[i].instructions)

		go c.Run()

		for _, x := range tests[i].input {
			in <- x
		}
		close(in)

		actual := make([]int, 0)
		for o := range out {
			actual = append(actual, o)
		}

		assert.EqualValues(t, tests[i].expected, actual)
	}
}
