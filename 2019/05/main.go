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

	go RunProgram(instructions, in, out)

	in <- input
	close(in)

	return out
}

func RunProgram(instructions []int, input <-chan int, output chan<- int) {
	defer close(output)
	buf := make([]int, len(instructions))
	copy(buf, instructions)

	i := 0
	for i <= len(buf)-1 {
		opcode, modes := ParseOpcode(buf[i])

		switch opcode {
		case 1:
			p1 := ReadParameter(buf, i+1, modes[0])
			p2 := ReadParameter(buf, i+2, modes[1])
			p3 := buf[i+3]
			buf[p3] = p1 + p2
			i += 4
		case 2:
			p1 := ReadParameter(buf, i+1, modes[0])
			p2 := ReadParameter(buf, i+2, modes[1])
			p3 := buf[i+3]
			buf[p3] = p1 * p2
			i += 4
		case 3:
			p1 := buf[i+1]
			buf[p1] = <-input
			i += 2
		case 4:
			p1 := ReadParameter(buf, i+1, modes[0])
			output <- p1
			i += 2
		case 5:
			p1 := ReadParameter(buf, i+1, modes[0])
			p2 := ReadParameter(buf, i+2, modes[1])
			if p1 != 0 {
				i = p2
			} else {
				i += 3
			}
		case 6:
			p1 := ReadParameter(buf, i+1, modes[0])
			p2 := ReadParameter(buf, i+2, modes[1])
			if p1 == 0 {
				i = p2
			} else {
				i += 3
			}
		case 7:
			p1 := ReadParameter(buf, i+1, modes[0])
			p2 := ReadParameter(buf, i+2, modes[1])
			p3 := buf[i+3]
			if p1 < p2 {
				buf[p3] = 1
			} else {
				buf[p3] = 0
			}
			i += 4
		case 8:
			p1 := ReadParameter(buf, i+1, modes[0])
			p2 := ReadParameter(buf, i+2, modes[1])
			p3 := buf[i+3]
			if p1 == p2 {
				buf[p3] = 1
			} else {
				buf[p3] = 0
			}
			i += 4
		case 99:
			return
		default:
			fmt.Printf("Unknown opcode %d, stopping!", opcode)
			return
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
