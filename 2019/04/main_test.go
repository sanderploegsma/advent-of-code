package main

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestPartOne(t *testing.T) {
	assert.True(t, PasswordMeetsCriteria(111111), "%d should meet criteria", 111111)
	assert.False(t, PasswordMeetsCriteria(223450), "%d should not meet criteria", 223450)
	assert.False(t, PasswordMeetsCriteria(123789), "%d should not meet criteria", 123789)
}

func TestPartTwo(t *testing.T) {
	assert.True(t, PasswordMeetsCriteria2(112233), "%d should meet criteria", 112233)
	assert.False(t, PasswordMeetsCriteria2(123444), "%d should not meet criteria", 123444)
	assert.True(t, PasswordMeetsCriteria2(111122), "%d should meet criteria", 111122)
}
