package main

import (
	"fmt"
	"os"
	"strconv"

	"github.com/sanderploegsma/advent-of-code/2019/utils"
)

func main() {
	input, err := utils.ReadFile("input.txt")
	if err != nil {
		fmt.Printf("failed to read input: %v", err)
		os.Exit(1)
	}

	digits := make([]int, len(input))
	for i := range input {
		digits[i], _ = strconv.Atoi(string(input[i]))
	}

	fmt.Println(PartOne(digits))
	fmt.Println(PartTwo(digits))
}

func PartOne(digits []int) []int {
	res := FFT(digits, 100)
	return res[:8]
}

func PartTwo(digits []int) []int {
	signal := make([]int, len(digits)*10000)
	for i := 0; i < 1000; i++ {
		for j := 0; j < len(digits); j++ {
			signal[i*j] = digits[j]
		}
	}

	offsetStr := ""
	for i := 0; i < 7; i++ {
		offsetStr += strconv.Itoa(digits[i])
	}
	offset, _ := strconv.Atoi(offsetStr)

	res := FFT(signal, 100)
	return res[offset+1 : offset+9]
}

func FFT(input []int, repeat int) []int {
	res := make([]int, len(input))
	copy(res, input)

	patterns := make(map[int][]int)
	for r := 0; r < repeat; r++ {
		temp := make([]int, len(input))
		for i := range input {
			p, ok := patterns[i+1]
			if !ok {
				p = pattern(i+1, len(input))
				patterns[i+1] = p
			}

			v := 0
			for j := range input {
				v += res[j] * p[j]
			}

			temp[i] = utils.Abs(v) % 10
		}
		res = temp
	}
	return res
}

func pattern(repeat, length int) []int {
	p := []int{0, 1, 0, -1}
	res := make([]int, length+1)
	for i := range res {
		res[i] = p[i/repeat%len(p)]
	}
	return res[1:]
}
