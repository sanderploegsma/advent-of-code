package main

import (
	"fmt"
	"os"
	"time"

	"github.com/sanderploegsma/advent-of-code/2019/go/utils"
)

func main() {
	instructions, err := utils.ReadIntCode("input.txt")
	if err != nil {
		fmt.Printf("failed to read input: %v\n", err)
		os.Exit(1)
	}

	start := time.Now()
	for o := range RunWithInput(instructions, 1) {
		fmt.Printf("[PART 1] Output: %d\n", o)
	}
	fmt.Printf("[PART 1] took %s\n", time.Since(start))

	start = time.Now()
	for o := range RunWithInput(instructions, 5) {
		fmt.Printf("[PART 2] Output: %d\n", o)
	}
	fmt.Printf("[PART 2] took %s\n", time.Since(start))
}

func RunWithInput(instructions []int, input int) chan int {
	in := make(chan int)
	out := make(chan int)

	go utils.RunIntCode(in, out, instructions)

	in <- input
	close(in)

	return out
}
