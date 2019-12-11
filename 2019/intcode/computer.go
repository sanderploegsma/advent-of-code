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
	add:         []paramType{read, read, write},
	multiply:    []paramType{read, read, write},
	input:       []paramType{write},
	output:      []paramType{read},
	jumpIfTrue:  []paramType{read, read},
	jumpIfFalse: []paramType{read, read},
	lessThan:    []paramType{read, read, write},
	equals:      []paramType{read, read, write},
	adjust:      []paramType{read},
	halt:        []paramType{},
}

type Computer struct {
	In, Out  chan int
	Finished bool
	memory   []int
}

func NewComputer(in, out chan int, instructions []int) *Computer {
	c := Computer{In: in, Out: out, Finished: false, memory: make([]int, len(instructions))}
	copy(c.memory, instructions)
	return &c
}

func (c *Computer) Run() {
	defer close(c.Out)

	i := 0
	offset := 0
	for !c.Finished {
		opcode, params := c.instruction(i, offset)
		i += len(params) + 1

		switch opcode {
		case add:
			c.set(params[2], params[0]+params[1])
		case multiply:
			c.set(params[2], params[0]*params[1])
		case input:
			in := <-c.In
			c.set(params[0], in)
		case output:
			c.Out <- params[0]
		case jumpIfTrue:
			if params[0] != 0 {
				i = params[1]
			}
		case jumpIfFalse:
			if params[0] == 0 {
				i = params[1]
			}
		case lessThan:
			if params[0] < params[1] {
				c.set(params[2], 1)
			} else {
				c.set(params[2], 0)
			}
		case equals:
			if params[0] == params[1] {
				c.set(params[2], 1)
			} else {
				c.set(params[2], 0)
			}
		case adjust:
			offset += params[0]
		case halt:
			c.Finished = true
			return
		default:
			fmt.Printf("Unknown opcode %d (original %d), stopping!\n", opcode, c.get(i))
			c.Finished = true
			return
		}
	}
}

func (c *Computer) get(addr int) int {
	c.checkSize(addr)
	return c.memory[addr]
}

func (c *Computer) set(addr int, val int) {
	c.checkSize(addr)
	c.memory[addr] = val
}

func (c *Computer) checkSize(addr int) {
	if addr >= len(c.memory) {
		extra := make([]int, addr-(len(c.memory)-1))
		c.memory = append(c.memory, extra...)
	}
}

func (c *Computer) instruction(position, offset int) (opCode, []int) {
	input := c.get(position)
	opcode := opCode(input % 100)
	modes := (input - int(opcode)) / 100

	params := make([]int, 0)
	for i, t := range parameters[opcode] {
		mode := modes % 10
		params = append(params, c.readParameter(position+i+1, offset, mode, t))
		modes = (modes - mode) / 10
	}

	return opcode, params
}

func (c *Computer) readParameter(pos int, offset int, mode int, t paramType) int {
	if mode == 0 && t == read {
		return c.get(c.get(pos))
	}
	if mode == 2 && t == read {
		return c.get(c.get(pos) + offset)
	}
	if mode == 2 && t == write {
		return c.get(pos) + offset
	}

	return c.get(pos)
}
