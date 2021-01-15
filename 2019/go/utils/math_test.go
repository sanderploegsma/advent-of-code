package utils

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestAbs(t *testing.T) {
	assert.Equal(t, 1, Abs(1))
	assert.Equal(t, 1, Abs(-1))
	assert.Equal(t, 0, Abs(0))
	assert.Equal(t, 0, Abs(-0))
}

func TestGCD(t *testing.T) {
	assert.Equal(t, 1, GCD(5, 7))
	assert.Equal(t, 5, GCD(10, 15))
	assert.Equal(t, 10, GCD(10, 300))
	assert.Equal(t, 200, GCD(400, 600))
}

func TestLCM(t *testing.T) {
	assert.Equal(t, 30, LCM(10, 15))
	assert.Equal(t, 10, LCM(10, 10, 10))
	assert.Equal(t, 60, LCM(5, 4, 6))
}

func TestMax(t *testing.T) {
	assert.Equal(t, 1, Max(0, 1))
	assert.Equal(t, 1, Max(1, 1))
	assert.Equal(t, 2, Max(2, 1))
	assert.Equal(t, 1, Max(-2, 1))
}

func TestMin(t *testing.T) {
	assert.Equal(t, 0, Min(0, 1))
	assert.Equal(t, 1, Min(1, 1))
	assert.Equal(t, 1, Min(2, 1))
	assert.Equal(t, -2, Min(-2, 1))
}

func TestCompare(t *testing.T) {
	assert.Equal(t, 1, Compare(2, 1))
	assert.Equal(t, -1, Compare(1, 2))
	assert.Equal(t, 0, Compare(1, 1))
}
