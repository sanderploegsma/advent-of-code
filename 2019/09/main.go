package main

import (
	"fmt"
	"os"
	"time"

	"github.com/sanderploegsma/advent-of-code/2019/utils"
)

func main() {
	instructions, err := utils.ReadIntCode("input.txt")
	if err != nil {
		fmt.Printf("failed to read input: %v\n", err)
		os.Exit(1)
	}

	start := time.Now()
	result := RunWithInput(instructions, 1)
	fmt.Printf("[PART 1] output: %d (took %s)\n", result, time.Since(start))

	start = time.Now()
	result = RunWithInput(instructions, 2)
	fmt.Printf("[PART 2] output: %d (took %s)\n", result, time.Since(start))
}

func RunWithInput(instructions []int, input int) int {
	in := make(chan int, 1)
	out := make(chan int)

	go utils.RunIntCode(in, out, instructions)

	in <- input
	close(in)

	output := make([]int, 0)
	for o := range out {
		output = append(output, o)
	}
	return output[len(output)-1]
}
