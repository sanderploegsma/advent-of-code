package utils

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

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

		go RunIntCode(in, out, tests[i].instructions)

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
