package main

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestPartOne(t *testing.T) {
	input := []Moon{
		{Position: XYZ{-1, 0, 2}},
		{Position: XYZ{2, -10, -7}},
		{Position: XYZ{4, -8, 8}},
		{Position: XYZ{3, 5, -1}},
	}

	expected := []Moon{
		{Position: XYZ{X: 2, Y: -1, Z: 1}, Velocity: XYZ{X: 3, Y: -1, Z: -1}},
		{Position: XYZ{X: 3, Y: -7, Z: -4}, Velocity: XYZ{X: 1, Y: 3, Z: 3}},
		{Position: XYZ{X: 1, Y: -7, Z: 5}, Velocity: XYZ{X: -3, Y: 1, Z: -3}},
		{Position: XYZ{X: 2, Y: 2, Z: 0}, Velocity: XYZ{X: -1, Y: -3, Z: 1}},
	}

	assert.EqualValues(t, expected, Simulate(input))
}

func TestPartTwo(t *testing.T) {
	input := []Moon{
		{Position: XYZ{-1, 0, 2}},
		{Position: XYZ{2, -10, -7}},
		{Position: XYZ{4, -8, 8}},
		{Position: XYZ{3, 5, -1}},
	}

	assert.Equal(t, 2772, RepeatUntilPreviousState(input))
}
