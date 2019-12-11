package intcode

import (
	"fmt"
	"strconv"
)

var numberOfParameters = map[int]int{
	1:  3,
	2:  3,
	3:  1,
	4:  1,
	5:  2,
	6:  2,
	7:  3,
	8:  3,
	9:  1,
	99: 0,
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
		opcode, modes := parseOpcode(c.get(i))

		switch opcode {
		case 1:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			p2 := c.readParameter(i+2, offset, modes[1], false)
			p3 := c.readParameter(i+3, offset, modes[2], true)
			// fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), c.get(i+3), p3, p1+p2, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
			c.set(p3, p1+p2)
			i += 4
		case 2:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			p2 := c.readParameter(i+2, offset, modes[1], false)
			p3 := c.readParameter(i+3, offset, modes[2], true)
			// fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), c.get(i+3), p3, p1*p2, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
			c.set(p3, p1*p2)
			i += 4
		case 3:
			p1 := c.readParameter(i+1, offset, modes[0], true)
			in := <-c.In
			// fmt.Printf("i=%d [%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d))\n", i, c.get(i), c.get(i+1), p1, in, opcode, p1, modes[0])
			c.set(p1, in)
			i += 2
		case 4:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			// fmt.Printf("i=%d [%d,%d] OUT: %d (code: %d, p1: %d (mode %d))\n", i, c.get(i), c.get(i+1), p1, opcode, p1, modes[0])
			c.Out <- p1
			i += 2
		case 5:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			p2 := c.readParameter(i+2, offset, modes[1], false)
			if p1 != 0 {
				// fmt.Printf("i=%d [%d,%d,%d] GOTO: %d (code: %d, p1: %d (mode %d), p2: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), p2, opcode, p1, modes[0], p2, modes[1])
				i = p2
			} else {
				// fmt.Printf("i=%d [%d,%d,%d] GOTO: %d (code: %d, p1: %d (mode %d), p2: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), i+3, opcode, p1, modes[0], p2, modes[1])
				i += 3
			}
		case 6:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			p2 := c.readParameter(i+2, offset, modes[1], false)
			if p1 == 0 {
				// fmt.Printf("i=%d [%d,%d,%d] GOTO: %d (code: %d, p1: %d (mode %d), p2: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), p2, opcode, p1, modes[0], p2, modes[1])
				i = p2
			} else {
				// fmt.Printf("i=%d [%d,%d,%d] GOTO: %d (code: %d, p1: %d (mode %d), p2: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), i+3, opcode, p1, modes[0], p2, modes[1])
				i += 3
			}
		case 7:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			p2 := c.readParameter(i+2, offset, modes[1], false)
			p3 := c.readParameter(i+3, offset, modes[2], true)
			if p1 < p2 {
				// fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), c.get(i+3), p3, 1, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
				c.set(p3, 1)
			} else {
				// fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), c.get(i+3), p3, 0, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
				c.set(p3, 0)
			}
			i += 4
		case 8:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			p2 := c.readParameter(i+2, offset, modes[1], false)
			p3 := c.readParameter(i+3, offset, modes[2], true)
			if p1 == p2 {
				// fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), c.get(i+3), p3, 1, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
				c.set(p3, 1)
			} else {
				// fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), c.get(i+3), p3, 0, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
				c.set(p3, 0)
			}
			i += 4
		case 9:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			// fmt.Printf("i=%d [%d,%d] BASE: +%d (%d) (code: %d, p1: %d (mode %d))\n", i, c.get(i), c.get(i+1), p1, offset+p1, opcode, p1, modes[0])
			offset += p1
			i += 2
		case 99:
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

func (c *Computer) readParameter(pos int, offset int, mode int, isWrite bool) int {
	if mode == 0 && !isWrite {
		return c.get(c.get(pos))
	}
	if mode == 2 && !isWrite {
		return c.get(c.get(pos) + offset)
	}
	if mode == 2 && isWrite {
		return c.get(pos) + offset
	}

	return c.get(pos)
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
