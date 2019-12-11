package intcode

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestReadInstructions(t *testing.T) {
	expected := []int{204, -1, 104, 1125899906842624, 99}
	actual, err := ReadInstructions("input_test.txt")
	assert.NoError(t, err)
	assert.Equal(t, expected, actual)
}
