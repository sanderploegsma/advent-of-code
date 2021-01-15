package main

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestPartOne(t *testing.T) {
	assert.Equal(t, 2, PartOne([]string{"12"}))
	assert.Equal(t, 2, PartOne([]string{"14"}))
	assert.Equal(t, 654, PartOne([]string{"1969"}))
	assert.Equal(t, 33583, PartOne([]string{"100756"}))
}

func TestPartTwo(t *testing.T) {
	assert.Equal(t, 2, PartTwo([]string{"14"}))
	assert.Equal(t, 966, PartTwo([]string{"1969"}))
	assert.Equal(t, 50346, PartTwo([]string{"100756"}))
}
