package utils

// Abs calculates the absolute value for the given integer
func Abs(i int) int {
	if i >= 0 {
		return i
	}
	return -1 * i
}

// GCD calculates the Greatest Common Divisor for the given integers
func GCD(a, b int) int {
	if b == 0 {
		return a
	}
	return GCD(b, a%b)
}

// LCM calculates the Least Common Multiplier for the given integers
func LCM(a, b int, integers ...int) int {
	result := a * b / GCD(a, b)

	for i := 0; i < len(integers); i++ {
		result = LCM(result, integers[i])
	}

	return result
}

// Max returns the maximum of the given integers
func Max(a, b int) int {
	if a >= b {
		return a
	}
	return b
}

// Min returns the minimum of the given integers
func Min(a, b int) int {
	if a <= b {
		return a
	}
	return b
}
