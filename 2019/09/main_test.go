package main

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestExampleOne(t *testing.T) {
	tests := []struct {
		instructions []int64
		input        []int64
		expected     []int64
	}{
		{
			instructions: []int64{109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99},
			input:        []int64{},
			expected:     []int64{109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99},
		},
		{
			instructions: []int64{104, 1125899906842624, 99},
			input:        []int64{},
			expected:     []int64{1125899906842624},
		},
	}

	for i := range tests {
		in := make(chan int64, len(tests[i].input))
		out := make(chan int64)

		go runProgram(tests[i].instructions, in, out)

		for _, x := range tests[i].input {
			in <- x
		}
		close(in)

		actual := make([]int64, 0)
		for o := range out {
			t.Logf("output: %d", o)
			actual = append(actual, o)
		}

		assert.EqualValues(t, tests[i].expected, actual)
	}
}
