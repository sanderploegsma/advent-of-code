package utils

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestReadIntcode(t *testing.T) {
	expected := []int{204, -1, 104, 1125899906842624, 99}
	actual, err := ReadIntCode("input_intcode.txt")
	assert.NoError(t, err)
	assert.Equal(t, expected, actual)
}
