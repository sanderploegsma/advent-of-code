package intcode

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

type VM struct {
	In, Out  chan int
	Finished bool
	memory   map[int]int
}

func NewVM(in, out chan int, instructions []int) *VM {
	c := VM{In: in, Out: out, Finished: false, memory: make(map[int]int)}
	for i, instruction := range instructions {
		c.memory[i] = instruction
	}
	return &c
}

func (c *VM) Run() {
	defer close(c.Out)

	i := 0
	offset := 0
	for !c.Finished {
		opcode, ptrs := c.instruction(i, offset)
		i += len(ptrs) + 1

		switch opcode {
		case add:
			c.memory[ptrs[2]] = c.memory[ptrs[0]] + c.memory[ptrs[1]]
		case multiply:
			c.memory[ptrs[2]] = c.memory[ptrs[0]] * c.memory[ptrs[1]]
		case input:
			c.memory[ptrs[0]] = <-c.In
		case output:
			c.Out <- c.memory[ptrs[0]]
		case jumpIfTrue:
			if itob(c.memory[ptrs[0]]) {
				i = c.memory[ptrs[1]]
			}
		case jumpIfFalse:
			if !itob(c.memory[ptrs[0]]) {
				i = c.memory[ptrs[1]]
			}
		case lessThan:
			c.memory[ptrs[2]] = btoi(c.memory[ptrs[0]] < c.memory[ptrs[1]])
		case equals:
			c.memory[ptrs[2]] = btoi(c.memory[ptrs[0]] == c.memory[ptrs[1]])
		case adjust:
			offset += c.memory[ptrs[0]]
		case halt:
			c.Finished = true
			return
		default:
			fmt.Printf("Unknown opcode %d (original %d), stopping!\n", opcode, c.memory[i])
			c.Finished = true
			return
		}
	}
}

func (c *VM) instruction(pos, offset int) (int, []int) {
	opcode := c.memory[pos] % 100
	mask := c.memory[pos] / 100

	ptrs := make([]int, parameters[opcode])
	for i := 0; i < parameters[opcode]; i++ {
		switch mask % 10 {
		case 0:
			ptrs[i] = c.memory[pos+i+1]
		case 1:
			ptrs[i] = pos + i + 1
		case 2:
			ptrs[i] = c.memory[pos+i+1] + offset
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
