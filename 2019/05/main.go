package main

import (
	"fmt"
	"strconv"

	"github.com/sanderploegsma/advent-of-code/2019/reader"
)

var numberOfParameters = map[int]int{1: 3, 2: 3, 3: 1, 4: 1, 99: 0}

func main() {
	input, _ := reader.ReadDelim("input.txt", ",")
	instructions := make([]int, len(input))
	for i := range input {
		instructions[i], _ = strconv.Atoi(input[i])
	}

	in := make(chan int)
	out := make(chan int)

	go RunProgram(instructions, in, out)

	in <- 1
	close(in)

	for o := range out {
		fmt.Printf("Output: %d\n", o)
	}
}

func RunProgram(instructions []int, input <-chan int, output chan<- int) {
	defer close(output)
	buf := make([]int, len(instructions))
	copy(buf, instructions)

	i := 0
	for i <= len(buf)-1 {
		opcode, modes := ParseOpcode(buf[i])

		if opcode == 99 {
			break
		}

		if opcode == 1 {
			x := ReadParameter(buf, i+1, modes[0])
			y := ReadParameter(buf, i+2, modes[1])
			pos := buf[i+3]
			buf[pos] = x + y
			i += 4
		}

		if opcode == 2 {
			x := ReadParameter(buf, i+1, modes[0])
			y := ReadParameter(buf, i+2, modes[1])
			pos := buf[i+3]
			buf[pos] = x * y
			i += 4
		}

		if opcode == 3 {
			pos := buf[i+1]
			buf[pos] = <-input
			i += 2
		}

		if opcode == 4 {
			p := ReadParameter(buf, i+1, modes[0])
			output <- p
			i += 2
		}
	}
}

// ReadParameter reads the parameter at the given position in the buffer with the given mode.
// When mode is 0, the parameter will be read using position mode.
// When mode is 1, the parameter will be read using immediate mode.
func ReadParameter(buf []int, pos int, mode int) int {
	if mode == 0 {
		return buf[buf[pos]]
	}

	return buf[pos]
}

// ParseOpcode will convert the given input to an opcode and a set of parameter modes.
// The returned modes are in the same order as their respective parameters.

// Examples:
// 1 -> (1, [0, 0, 0]) (opcode 1 has three parameters, mode defaults to 0)
// 00001 -> (1, [0, 0, 0])
// 10002 -> (2, [0, 0, 1])
func ParseOpcode(input int) (opcode int, modes []int) {
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
