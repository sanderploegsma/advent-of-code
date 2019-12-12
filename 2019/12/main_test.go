package main

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestPartOne(t *testing.T) {
	input := [][]int{
		{-1, 0, 2, 0, 0, 0},
		{2, -10, -7, 0, 0, 0},
		{4, -8, 8, 0, 0, 0},
		{3, 5, -1, 0, 0, 0},
	}

	expected := [][]int{
		{2, -1, 1, 3, -1, -1},
		{3, -7, -4, 1, 3, 3},
		{1, -7, 5, -3, 1, -3},
		{2, 2, 0, -1, -3, 1},
	}

	assert.EqualValues(t, expected, Simulate(input))
}

func TestPartTwo(t *testing.T) {
	input := [][]int{
		{-1, 0, 2, 0, 0, 0},
		{2, -10, -7, 0, 0, 0},
		{4, -8, 8, 0, 0, 0},
		{3, 5, -1, 0, 0, 0},
	}

	assert.Equal(t, 2772, CalculateCycle(input))
}
