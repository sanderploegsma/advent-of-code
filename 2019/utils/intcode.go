package utils

import (
	"fmt"
)

const (
	add         = 1
	multiply    = 2
	input       = 3
	output      = 4
	jumpIfTrue  = 5
	jumpIfFalse = 6
	lessThan    = 7
	equals      = 8
	adjust      = 9
	halt        = 99
)

var parameters = map[int]int{
	add:         3,
	multiply:    3,
	input:       1,
	output:      1,
	jumpIfTrue:  2,
	jumpIfFalse: 2,
	lessThan:    3,
	equals:      3,
	adjust:      1,
	halt:        0,
}

// RunIntCode runs the given instructions. The provided output is closed when the program halts.
func RunIntCode(in, out chan int, instructions []int) {
	defer close(out)

	memory := make(map[int]int)
	for i, instruction := range instructions {
		memory[i] = instruction
	}

	i := 0
	offset := 0
	for {
		opcode, ptrs := instruction(memory, i, offset)
		i += len(ptrs) + 1

		switch opcode {
		case add:
			memory[ptrs[2]] = memory[ptrs[0]] + memory[ptrs[1]]
		case multiply:
			memory[ptrs[2]] = memory[ptrs[0]] * memory[ptrs[1]]
		case input:
			memory[ptrs[0]] = <-in
		case output:
			out <- memory[ptrs[0]]
		case jumpIfTrue:
			if itob(memory[ptrs[0]]) {
				i = memory[ptrs[1]]
			}
		case jumpIfFalse:
			if !itob(memory[ptrs[0]]) {
				i = memory[ptrs[1]]
			}
		case lessThan:
			memory[ptrs[2]] = btoi(memory[ptrs[0]] < memory[ptrs[1]])
		case equals:
			memory[ptrs[2]] = btoi(memory[ptrs[0]] == memory[ptrs[1]])
		case adjust:
			offset += memory[ptrs[0]]
		case halt:
			return
		default:
			fmt.Printf("Unknown opcode %d (original %d), stopping!\n", opcode, memory[i])
			return
		}
	}
}

func instruction(memory map[int]int, pos, offset int) (int, []int) {
	opcode := memory[pos] % 100
	mask := memory[pos] / 100

	ptrs := make([]int, parameters[opcode])
	for i := 0; i < parameters[opcode]; i++ {
		switch mask % 10 {
		case 0:
			ptrs[i] = memory[pos+i+1]
		case 1:
			ptrs[i] = pos + i + 1
		case 2:
			ptrs[i] = memory[pos+i+1] + offset
		}
		mask = mask / 10
	}

	return opcode, ptrs
}

func btoi(b bool) int {
	if b {
		return 1
	}
	return 0
}

func itob(i int) bool {
	if i != 0 {
		return true
	}
	return false
}
