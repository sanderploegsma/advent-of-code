package main

import (
	"fmt"
	"strconv"
	"time"

	"github.com/sanderploegsma/advent-of-code/2019/reader"
)

var numberOfParameters = map[int]int{1: 3, 2: 3, 3: 1, 4: 1, 5: 2, 6: 2, 7: 3, 8: 3, 9: 1, 99: 0}

func main() {
	input, _ := reader.ReadDelim("input.txt", ",")
	instructions := make([]int64, len(input))
	for i := range input {
		instructions[i], _ = strconv.ParseInt(input[i], 10, 64)
	}

	start := time.Now()
	result := RunWithInput(instructions, 1)
	fmt.Printf("[PART 1] output: %d (took %s)\n", result, time.Since(start))

	start = time.Now()
	result = RunWithInput(instructions, 2)
	fmt.Printf("[PART 2] output: %d (took %s)\n", result, time.Since(start))
}

func RunWithInput(instructions []int64, input int64) int64 {
	in := make(chan int64, 1)
	out := make(chan int64)
	go runProgram(instructions, in, out)

	in <- input
	close(in)

	output := make([]int64, 0)
	for o := range out {
		output = append(output, o)
	}
	fmt.Println(output)
	return output[len(output)-1]
}

type memory struct {
	m []int64
}

func newMemory(initial []int64) *memory {
	m := &memory{m: make([]int64, len(initial))}
	copy(m.m, initial)
	return m
}

func (m *memory) Len() int {
	return len(m.m)
}

func (m *memory) Get(addr int) int64 {
	m.checkSize(addr)
	return m.m[addr]
}

func (m *memory) Set(addr int, val int64) {
	m.checkSize(addr)
	m.m[addr] = val
}

func (m *memory) checkSize(addr int) {
	if addr >= len(m.m) {
		extra := make([]int64, addr-(len(m.m)-1))
		m.m = append(m.m, extra...)
	}
}

func runProgram(instructions []int64, input, output chan int64) {
	defer close(output)
	m := newMemory(instructions)

	i := 0
	offset := 0
	for i <= m.Len()-1 {
		opcode, modes := parseOpcode(int(m.Get(i)))

		switch opcode {
		case 1:
			p1 := readParameter(m, i+1, offset, modes[0], false)
			p2 := readParameter(m, i+2, offset, modes[1], false)
			p3 := readParameter(m, i+3, offset, modes[2], true)
			// p3 := m.Get(i+3)
			fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), m.Get(i+2), m.Get(i+3), p3, p1+p2, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
			m.Set(int(p3), p1+p2)
			i += 4
		case 2:
			p1 := readParameter(m, i+1, offset, modes[0], false)
			p2 := readParameter(m, i+2, offset, modes[1], false)
			p3 := readParameter(m, i+3, offset, modes[2], true)
			// p3 := m.Get(i+3)
			fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), m.Get(i+2), m.Get(i+3), p3, p1*p2, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
			m.Set(int(p3), p1*p2)
			i += 4
		case 3:
			p1 := readParameter(m, i+1, offset, modes[0], true)
			in := <-input
			fmt.Printf("i=%d [%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), p1, in, opcode, p1, modes[0])
			m.Set(int(p1), in)
			i += 2
		case 4:
			p1 := readParameter(m, i+1, offset, modes[0], false)
			fmt.Printf("i=%d [%d,%d] OUT: %d (code: %d, p1: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), p1, opcode, p1, modes[0])
			output <- p1
			i += 2
		case 5:
			p1 := readParameter(m, i+1, offset, modes[0], false)
			p2 := readParameter(m, i+2, offset, modes[1], false)
			if p1 != 0 {
				fmt.Printf("i=%d [%d,%d,%d] GOTO: %d (code: %d, p1: %d (mode %d), p2: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), m.Get(i+2), p2, opcode, p1, modes[0], p2, modes[1])
				i = int(p2)
			} else {
				fmt.Printf("i=%d [%d,%d,%d] GOTO: %d (code: %d, p1: %d (mode %d), p2: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), m.Get(i+2), i + 3, opcode, p1, modes[0], p2, modes[1])
				i += 3
			}
		case 6:
			p1 := readParameter(m, i+1, offset, modes[0], false)
			p2 := readParameter(m, i+2, offset, modes[1], false)
			if p1 == 0 {
				fmt.Printf("i=%d [%d,%d,%d] GOTO: %d (code: %d, p1: %d (mode %d), p2: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), m.Get(i+2), p2, opcode, p1, modes[0], p2, modes[1])
				i = int(p2)
			} else {
				fmt.Printf("i=%d [%d,%d,%d] GOTO: %d (code: %d, p1: %d (mode %d), p2: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), m.Get(i+2), i + 3, opcode, p1, modes[0], p2, modes[1])
				i += 3
			}
		case 7:
			p1 := readParameter(m, i+1, offset, modes[0], false)
			p2 := readParameter(m, i+2, offset, modes[1], false)
			p3 := readParameter(m, i+3, offset, modes[2], true)
			// p3 := m.Get(i+3)
			if p1 < p2 {
				fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), m.Get(i+2), m.Get(i+3), p3, 1, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
				m.Set(int(p3), 1)
			} else {
				fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), m.Get(i+2), m.Get(i+3), p3, 0, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
				m.Set(int(p3), 0)
			}
			i += 4
		case 8:
			p1 := readParameter(m, i+1, offset, modes[0], false)
			p2 := readParameter(m, i+2, offset, modes[1], false)
			p3 := readParameter(m, i+3, offset, modes[2], true)
			// p3 := m.Get(i+3)
			if p1 == p2 {
				fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), m.Get(i+2), m.Get(i+3), p3, 1, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
				m.Set(int(p3), 1)
			} else {
				fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), m.Get(i+2), m.Get(i+3), p3, 0, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
				m.Set(int(p3), 0)
			}
			i += 4
		case 9:
			p1 := readParameter(m, i+1, offset, modes[0] ,false)
			fmt.Printf("i=%d [%d,%d] BASE: +%d (%d) (code: %d, p1: %d (mode %d))\n", i, m.Get(i), m.Get(i+1), p1, offset+int(p1), opcode, p1, modes[0])
			offset += int(p1)
			i += 2
		case 99:
			return
		default:
			fmt.Printf("Unknown opcode %d (original %d), stopping!\n", opcode, m.Get(i))
			return
		}
	}
}

func readParameter(m *memory, pos int, offset int, mode int, isWrite bool) int64 {
	if mode == 0 && !isWrite {
		return m.Get(int(m.Get(pos)))
	}
	if mode == 2 && !isWrite {
		return m.Get(int(m.Get(pos)) + offset)
	}
	if mode == 2 && isWrite {
		return m.Get(pos) + int64(offset)
	}

	return m.Get(pos)
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
