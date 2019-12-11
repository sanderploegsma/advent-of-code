package main

import (
	"fmt"
	"os"
	"time"

	"github.com/sanderploegsma/advent-of-code/2019/intcode"
)

func main() {
	instructions, err := intcode.ReadInstructions("input.txt")
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

func PartOne(instructions []int) (out int) {
	for _, phases := range generateCombinations([]int{0, 1, 2, 3, 4}) {
		output := RunWithPhases(instructions, phases, false)
		if output > out {
			out = output
		}
	}
	return out
}

func PartTwo(instructions []int) (out int) {
	for _, phases := range generateCombinations([]int{5, 6, 7, 8, 9}) {
		output := RunWithPhases(instructions, phases, true)
		if output > out {
			out = output
		}
	}
	return out
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

func RunWithPhases(instructions []int, phases []int, loop bool) (out int) {
	a := intcode.NewComputer(make(chan int), make(chan int), instructions)
	b := intcode.NewComputer(a.Out, make(chan int), instructions)
	c := intcode.NewComputer(b.Out, make(chan int), instructions)
	d := intcode.NewComputer(c.Out, make(chan int), instructions)
	e := intcode.NewComputer(d.Out, make(chan int), instructions)

	go b.Run()
	go a.Run()
	go d.Run()
	go c.Run()
	go e.Run()

	a.In <- int(phases[0])
	b.In <- int(phases[1])
	c.In <- int(phases[2])
	d.In <- int(phases[3])
	e.In <- int(phases[4])

	a.In <- 0

	for {
		o := <-e.Out
		if !loop || a.Finished {
			return int(o)
		}
		a.In <- o
	}
}
