package intcode

import (
	"fmt"
)

type opCode int

const (
	add         opCode = 1
	multiply    opCode = 2
	input       opCode = 3
	output      opCode = 4
	jumpIfTrue  opCode = 5
	jumpIfFalse opCode = 6
	lessThan    opCode = 7
	equals      opCode = 8
	adjust      opCode = 9
	halt        opCode = 99
)

type paramType int

const (
	read  paramType = 0
	write paramType = 1
)

var parameters = map[opCode][]paramType{
	add:         {read, read, write},
	multiply:    {read, read, write},
	input:       {write},
	output:      {read},
	jumpIfTrue:  {read, read},
	jumpIfFalse: {read, read},
	lessThan:    {read, read, write},
	equals:      {read, read, write},
	adjust:      {read},
	halt:        {},
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
		opcode, params := c.instruction(i, offset)
		i += len(params) + 1

		switch opcode {
		case add:
			c.memory[params[2]] = params[0] + params[1]
		case multiply:
			c.memory[params[2]] = params[0] * params[1]
		case input:
			c.memory[params[0]] = <-c.In
		case output:
			c.Out <- params[0]
		case jumpIfTrue:
			if itob(params[0]) {
				i = params[1]
			}
		case jumpIfFalse:
			if !itob(params[0]) {
				i = params[1]
			}
		case lessThan:
			c.memory[params[2]] = btoi(params[0] < params[1])
		case equals:
			c.memory[params[2]] = btoi(params[0] == params[1])
		case adjust:
			offset += params[0]
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

func (c *VM) instruction(position, offset int) (opCode, []int) {
	input := c.memory[position]
	opcode := opCode(input % 100)
	modes := (input - int(opcode)) / 100

	params := make([]int, len(parameters[opcode]))
	for i, t := range parameters[opcode] {
		mode := modes % 10
		params[i] = c.readParameter(position+i+1, offset, mode, t)
		modes = (modes - mode) / 10
	}

	return opcode, params
}

func (c *VM) readParameter(pos int, offset int, mode int, t paramType) int {
	if mode == 0 && t == read {
		return c.memory[c.memory[pos]]
	}
	if mode == 2 && t == read {
		return c.memory[c.memory[pos]+offset]
	}
	if mode == 2 && t == write {
		return c.memory[pos] + offset
	}

	return c.memory[pos]
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
