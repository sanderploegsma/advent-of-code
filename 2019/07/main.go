package main

import (
	"fmt"
	"strconv"
	"time"

	"github.com/sanderploegsma/advent-of-code/2019/reader"
)

var numberOfParameters = map[int]int{1: 3, 2: 3, 3: 1, 4: 1, 5: 2, 6: 2, 7: 3, 8: 3, 99: 0}

func main() {
	input, _ := reader.ReadDelim("input.txt", ",")
	instructions := make([]int, len(input))
	for i := range input {
		instructions[i], _ = strconv.Atoi(input[i])
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
	a := &amp{Input: make(chan int), Output: make(chan int)}
	b := &amp{Input: a.Output, Output: make(chan int)}
	c := &amp{Input: b.Output, Output: make(chan int)}
	d := &amp{Input: c.Output, Output: make(chan int)}
	e := &amp{Input: d.Output, Output: make(chan int)}

	go a.runProgram(instructions)
	go b.runProgram(instructions)
	go c.runProgram(instructions)
	go d.runProgram(instructions)
	go e.runProgram(instructions)

	a.Input <- phases[0]
	b.Input <- phases[1]
	c.Input <- phases[2]
	d.Input <- phases[3]
	e.Input <- phases[4]

	a.Input <- 0

	for {
		o := <-e.Output
		if !loop || a.Finished {
			return o
		}
		a.Input <- o
	}
}

type amp struct {
	Input    chan int
	Output   chan int
	Finished bool
}

func (a *amp) runProgram(instructions []int) {
	defer close(a.Output)
	buf := make([]int, len(instructions))
	copy(buf, instructions)

	i := 0
	for i <= len(buf)-1 {
		opcode, modes := parseOpcode(buf[i])

		switch opcode {
		case 1:
			p1 := readParameter(buf, i+1, modes[0])
			p2 := readParameter(buf, i+2, modes[1])
			p3 := buf[i+3]
			buf[p3] = p1 + p2
			i += 4
		case 2:
			p1 := readParameter(buf, i+1, modes[0])
			p2 := readParameter(buf, i+2, modes[1])
			p3 := buf[i+3]
			buf[p3] = p1 * p2
			i += 4
		case 3:
			p1 := buf[i+1]
			buf[p1] = <-a.Input
			i += 2
		case 4:
			p1 := readParameter(buf, i+1, modes[0])
			a.Output <- p1
			i += 2
		case 5:
			p1 := readParameter(buf, i+1, modes[0])
			p2 := readParameter(buf, i+2, modes[1])
			if p1 != 0 {
				i = p2
			} else {
				i += 3
			}
		case 6:
			p1 := readParameter(buf, i+1, modes[0])
			p2 := readParameter(buf, i+2, modes[1])
			if p1 == 0 {
				i = p2
			} else {
				i += 3
			}
		case 7:
			p1 := readParameter(buf, i+1, modes[0])
			p2 := readParameter(buf, i+2, modes[1])
			p3 := buf[i+3]
			if p1 < p2 {
				buf[p3] = 1
			} else {
				buf[p3] = 0
			}
			i += 4
		case 8:
			p1 := readParameter(buf, i+1, modes[0])
			p2 := readParameter(buf, i+2, modes[1])
			p3 := buf[i+3]
			if p1 == p2 {
				buf[p3] = 1
			} else {
				buf[p3] = 0
			}
			i += 4
		case 99:
			a.Finished = true
			return
		default:
			fmt.Printf("Unknown opcode %d (original %d), stopping!\n", opcode, buf[i])
			a.Finished = true
			return
		}
	}
}

func readParameter(buf []int, pos int, mode int) int {
	if mode == 0 {
		return buf[buf[pos]]
	}

	return buf[pos]
}

func parseOpcode(input int) (opcode int, modes []int) {
	opcode = input
	str := strconv.Itoa(input)

	if len(str) >= 2 {
		opcode, _ = strconv.Atoi(str[len(str)-2:])
		for i := len(str) - 3; i >= 0; i-- {
			mode, _ := strconv.Atoi(string(str[i]))
			modes = append(modes, mode)
		}
	}

	for i := len(modes); i < numberOfParameters[opcode]; i++ {
		modes = append(modes, 0)
	}

	return opcode, modes
}
