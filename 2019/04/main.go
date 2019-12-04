package main

import (
	"fmt"
	"strconv"
	"time"
)

func main() {
	start := time.Now()
	one, two := CheckPasswords(254032, 789860)
	end := time.Since(start)
	fmt.Printf("[PART ONE] passwords that meet criteria: %d\n", one)
	fmt.Printf("[PART TWO] passwords that meet criteria: %d\n", two)
	fmt.Printf("took %s\n", end)
}

// CheckPasswords checks for each password in the given range of passwords whether they meet both sets of criteria.
// For both criteria, it returns the number of passwords that meet them.
func CheckPasswords(start int, end int) (a int, b int) {
	for i := start; i < end; i++ {
		if PasswordMeetsCriteria(i) {
			a++
		}

		if PasswordMeetsCriteria2(i) {
			b++
		}
	}
	return a, b
}

// PasswordMeetsCriteria checks the given password for the following criteria:
// - Must have exactly 6 digits
// - Must have two adjacent digits that are the same
// - Going from left to right, the digits never decrease
func PasswordMeetsCriteria(password int) bool {
	digits := toDigits(password)

	if len(digits) != 6 {
		return false
	}

	if !isAscending(digits) {
		return false
	}

	return anyMatch(countDigits(digits), func(k, v int) bool {
		return v >= 2
	})
}

// PasswordMeetsCriteria2 checks the given password for the following criteria:
// - Must have exactly 6 digits
// - Must have a group of exactly two adjacent digits that are the same
// - Going from left to right, the digits never decrease
func PasswordMeetsCriteria2(password int) bool {
	digits := toDigits(password)

	if len(digits) != 6 {
		return false
	}

	if !isAscending(digits) {
		return false
	}

	return anyMatch(countDigits(digits), func(k, v int) bool {
		return v == 2
	})
}

// toDigits converts the given password to a list of digits.
// Example: 12345 => [1, 2, 3, 4, 5]
func toDigits(password int) []int {
	str := strconv.Itoa(password)
	digits := make([]int, len(str))

	for i := range str {
		digits[i], _ = strconv.Atoi(string(str[i]))
	}

	return digits
}

// isAscending checks if the given list of digits never descends.
func isAscending(digits []int) bool {
	for i := 1; i < len(digits); i++ {
		if digits[i-1] > digits[i] {
			return false
		}
	}

	return true
}

// countDigits counts occurrences of each digit the given list, and returns a map of digits and their respective counts.
func countDigits(digits []int) map[int]int {
	count := make(map[int]int)

	for _, d := range digits {
		count[d]++
	}

	return count
}

// anyMatch checks each key-value pair of the given map and returns true if any of them match the given predicate. If none match, it returns false.
func anyMatch(count map[int]int, pred func(k, v int) bool) bool {
	for k, v := range count {
		if pred(k, v) {
			return true
		}
	}
	return false
}
