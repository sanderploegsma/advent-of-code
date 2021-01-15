package main

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestFFT(t *testing.T) {
	assert.Equal(t, []int{4, 8, 2, 2, 6, 1, 5, 8}, FFT([]int{1, 2, 3, 4, 5, 6, 7, 8}, 1))
	assert.Equal(t, []int{3, 4, 0, 4, 0, 4, 3, 8}, FFT([]int{1, 2, 3, 4, 5, 6, 7, 8}, 2))
	assert.Equal(t, []int{0, 3, 4, 1, 5, 5, 1, 8}, FFT([]int{1, 2, 3, 4, 5, 6, 7, 8}, 3))
}

func TestPartOne(t *testing.T) {
	assert.Equal(t, []int{2, 4, 1, 7, 6, 1, 7, 6}, PartOne([]int{8, 0, 8, 7, 1, 2, 2, 4, 5, 8, 5, 9, 1, 4, 5, 4, 6, 6, 1, 9, 0, 8, 3, 2, 1, 8, 6, 4, 5, 5, 9, 5}))
	assert.Equal(t, []int{7, 3, 7, 4, 5, 4, 1, 8}, PartOne([]int{1, 9, 6, 1, 7, 8, 0, 4, 2, 0, 7, 2, 0, 2, 2, 0, 9, 1, 4, 4, 9, 1, 6, 0, 4, 4, 1, 8, 9, 9, 1, 7}))
	assert.Equal(t, []int{5, 2, 4, 3, 2, 1, 3, 3}, PartOne([]int{6, 9, 3, 1, 7, 1, 6, 3, 4, 9, 2, 9, 4, 8, 6, 0, 6, 3, 3, 5, 9, 9, 5, 9, 2, 4, 3, 1, 9, 8, 7, 3}))
}

func TestPartTwo(t *testing.T) {
	assert.Equal(t, []int{8, 4, 4, 6, 2, 0, 2, 6}, PartTwo([]int{0, 3, 0, 3, 6, 7, 3, 2, 5, 7, 7, 2, 1, 2, 9, 4, 4, 0, 6, 3, 4, 9, 1, 5, 6, 5, 4, 7, 4, 6, 6, 4}))
	assert.Equal(t, []int{7, 8, 7, 2, 5, 2, 7, 0}, PartTwo([]int{0, 2, 9, 3, 5, 1, 0, 9, 6, 9, 9, 9, 4, 0, 8, 0, 7, 4, 0, 7, 5, 8, 5, 4, 4, 7, 0, 3, 4, 3, 2, 3}))
	assert.Equal(t, []int{5, 3, 5, 5, 3, 7, 3, 1}, PartTwo([]int{0, 3, 0, 8, 1, 7, 7, 0, 8, 8, 4, 9, 2, 1, 9, 5, 9, 7, 3, 1, 1, 6, 5, 4, 4, 6, 8, 5, 0, 5, 1, 7}))
}
