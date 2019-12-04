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
	str := strconv.Itoa(password)

	if len(str) != 6 {
		return false
	}

	prev := 0
	double := false
	for i := range str {
		cur, _ := strconv.Atoi(str[i : i+1])
		if cur < prev {
			return false
		}
		if cur == prev {
			double = true
		}
		prev = cur
	}

	return double
}

// PasswordMeetsCriteria2 checks the given password for the following criteria:
// - Must have exactly 6 digits
// - Must have a group of exactly two adjacent digits that are the same
// - Going from left to right, the digits never decrease
func PasswordMeetsCriteria2(password int) bool {
	str := strconv.Itoa(password)

	if len(str) != 6 {
		return false
	}

	prev := 0
	repeat := 0
	double := false
	for i := range str {
		cur, _ := strconv.Atoi(str[i : i+1])
		if cur < prev {
			return false
		}

		if cur > prev {
			if repeat == 2 {
				double = true
			}
			repeat = 1
		}

		if cur == prev {
			repeat++
		}

		if i == len(str)-1 && repeat == 2 {
			return true
		}

		prev = cur
	}

	return double
}
