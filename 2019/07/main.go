package main

import (
	"fmt"
	"os"
	"sync"
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
	result := PartOne(instructions)
	fmt.Printf("[PART 1] output: %d (took %s)\n", result, time.Since(start))

	start = time.Now()
	result = PartTwo(instructions)
	fmt.Printf("[PART 2] output: %d (took %s)\n", result, time.Since(start))
}

func PartOne(instructions []int) int {
	var wg sync.WaitGroup
	var lock sync.RWMutex
	var max int

	for _, phases := range generateCombinations([]int{0, 1, 2, 3, 4}) {
		in, out := amps(phases, instructions)
		wg.Add(1)

		go func() {
			result := <-out
			lock.Lock()
			if result > max {
				max = result
			}
			lock.Unlock()
			wg.Done()
		}()

		in <- 0
	}

	wg.Wait()
	return max
}

func PartTwo(instructions []int) int {
	var wg sync.WaitGroup
	var lock sync.RWMutex
	var max int

	for _, phases := range generateCombinations([]int{5, 6, 7, 8, 9}) {
		in, out := amps(phases, instructions)
		wg.Add(1)

		go func() {
			var result int
			for result = range out {
				go func() { in <- result }()
			}

			lock.Lock()
			if result > max {
				max = result
			}
			lock.Unlock()
			wg.Done()
		}()

		in <- 0
	}

	wg.Wait()
	return max
}

func generateCombinations(items []int) (c [][]int) {
	var rc func([]int, int)
	rc = func(a []int, k int) {
		if k == len(a) {
			c = append(c, append([]int{}, a...))
		} else {
			for i := k; i < len(items); i++ {
				a[k], a[i] = a[i], a[k]
				rc(a, k+1)
				a[k], a[i] = a[i], a[k]
			}
		}
	}
	rc(items, 0)
	return c
}

func amps(phases, instructions []int) (in, out chan int) {
	channels := make([]chan int, len(phases)+1)
	for i := range channels {
		channels[i] = make(chan int)
	}

	for i, phase := range phases {
		go utils.RunIntCode(channels[i], channels[i+1], instructions)
		channels[i] <- phase
	}

	return channels[0], channels[len(channels)-1]
}
