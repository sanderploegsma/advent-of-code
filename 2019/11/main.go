package main

import (
	"fmt"
	"math"
	"strconv"
	"time"

	"github.com/sanderploegsma/advent-of-code/2019/reader"
)

var numberOfParameters = map[int]int{1: 3, 2: 3, 3: 1, 4: 1, 5: 2, 6: 2, 7: 3, 8: 3, 9: 1, 99: 0}

const (
	Up int = iota
	Right
	Down
	Left
)

const (
	Black = 0
	White = 1
)

func main() {
	input, _ := reader.ReadDelim("input.txt", ",")
	instructions := make([]int64, len(input))
	for i := range input {
		instructions[i], _ = strconv.ParseInt(input[i], 10, 64)
	}

	start := time.Now()
	result := Paint(instructions, Black)
	fmt.Printf("[PART 1] panels painted: %d (took %s)\n", len(result), time.Since(start))

	start = time.Now()
	result = Paint(instructions, White)
	fmt.Printf("[PART 2] panels painted: %d (took %s)\n", len(result), time.Since(start))
	fmt.Println("Registration identifier:")
	draw(result)
}

type Point struct{ x, y int64 }

func Paint(instructions []int64, startingColor int64) map[Point]int64 {
	in := make(chan int64)
	defer close(in)
	out := make(chan int64)

	c := NewComputer(in, out)
	go c.Run(instructions)

	painted := make(map[Point]int64)
	position := Point{0, 0}
	direction := Up

	painted[position] = startingColor

	for !c.Finished {
		if color, ok := painted[position]; ok {
			in <- color
		} else {
			in <- Black
		}

		color, ok := <-out
		if !ok {
			break
		}
		turn, ok := <-out
		if !ok {
			break
		}

		painted[position] = color
		position, direction = move(position, direction, int(turn))
	}

	return painted
}

func move(pos Point, dir, turn int) (newPos Point, newDir int) {
	if turn == 0 {
		switch dir {
		case Up:
			newDir = Left
			newPos = Point{pos.x - 1, pos.y}
		case Right:
			newDir = Up
			newPos = Point{pos.x, pos.y + 1}
		case Down:
			newDir = Right
			newPos = Point{pos.x + 1, pos.y}
		case Left:
			newDir = Down
			newPos = Point{pos.x, pos.y - 1}
		}
	} else {
		switch dir {
		case Up:
			newDir = Right
			newPos = Point{pos.x + 1, pos.y}
		case Right:
			newDir = Down
			newPos = Point{pos.x, pos.y - 1}
		case Down:
			newDir = Left
			newPos = Point{pos.x - 1, pos.y}
		case Left:
			newDir = Up
			newPos = Point{pos.x, pos.y + 1}
		}
	}

	return newPos, newDir
}

type Computer struct {
	In, Out  chan int64
	Finished bool
	memory   []int64
}

func NewComputer(in, out chan int64) *Computer {
	return &Computer{In: in, Out: out, Finished: false}
}

func (c *Computer) get(addr int) int64 {
	c.checkSize(addr)
	return c.memory[addr]
}

func (c *Computer) set(addr int, val int64) {
	c.checkSize(addr)
	c.memory[addr] = val
}

func (c *Computer) checkSize(addr int) {
	if addr >= len(c.memory) {
		extra := make([]int64, addr-(len(c.memory)-1))
		c.memory = append(c.memory, extra...)
	}
}

func (c *Computer) Run(instructions []int64) {
	defer close(c.Out)
	c.memory = make([]int64, len(instructions))
	copy(c.memory, instructions)

	i := 0
	offset := 0
	for !c.Finished {
		opcode, modes := parseOpcode(int(c.get(i)))

		switch opcode {
		case 1:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			p2 := c.readParameter(i+2, offset, modes[1], false)
			p3 := c.readParameter(i+3, offset, modes[2], true)
			// fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), c.get(i+3), p3, p1+p2, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
			c.set(int(p3), p1+p2)
			i += 4
		case 2:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			p2 := c.readParameter(i+2, offset, modes[1], false)
			p3 := c.readParameter(i+3, offset, modes[2], true)
			// fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), c.get(i+3), p3, p1*p2, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
			c.set(int(p3), p1*p2)
			i += 4
		case 3:
			p1 := c.readParameter(i+1, offset, modes[0], true)
			in := <-c.In
			// fmt.Printf("i=%d [%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d))\n", i, c.get(i), c.get(i+1), p1, in, opcode, p1, modes[0])
			c.set(int(p1), in)
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
				i = int(p2)
			} else {
				// fmt.Printf("i=%d [%d,%d,%d] GOTO: %d (code: %d, p1: %d (mode %d), p2: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), i+3, opcode, p1, modes[0], p2, modes[1])
				i += 3
			}
		case 6:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			p2 := c.readParameter(i+2, offset, modes[1], false)
			if p1 == 0 {
				// fmt.Printf("i=%d [%d,%d,%d] GOTO: %d (code: %d, p1: %d (mode %d), p2: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), p2, opcode, p1, modes[0], p2, modes[1])
				i = int(p2)
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
				c.set(int(p3), 1)
			} else {
				// fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), c.get(i+3), p3, 0, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
				c.set(int(p3), 0)
			}
			i += 4
		case 8:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			p2 := c.readParameter(i+2, offset, modes[1], false)
			p3 := c.readParameter(i+3, offset, modes[2], true)
			if p1 == p2 {
				// fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), c.get(i+3), p3, 1, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
				c.set(int(p3), 1)
			} else {
				// fmt.Printf("i=%d [%d,%d,%d,%d] SET: %d=%d (code: %d, p1: %d (mode %d), p2: %d (mode %d), p3: %d (mode %d))\n", i, c.get(i), c.get(i+1), c.get(i+2), c.get(i+3), p3, 0, opcode, p1, modes[0], p2, modes[1], p3, modes[2])
				c.set(int(p3), 0)
			}
			i += 4
		case 9:
			p1 := c.readParameter(i+1, offset, modes[0], false)
			// fmt.Printf("i=%d [%d,%d] BASE: +%d (%d) (code: %d, p1: %d (mode %d))\n", i, c.get(i), c.get(i+1), p1, offset+int(p1), opcode, p1, modes[0])
			offset += int(p1)
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

func (c *Computer) readParameter(pos int, offset int, mode int, isWrite bool) int64 {
	if mode == 0 && !isWrite {
		return c.get(int(c.get(pos)))
	}
	if mode == 2 && !isWrite {
		return c.get(int(c.get(pos)) + offset)
	}
	if mode == 2 && isWrite {
		return c.get(pos) + int64(offset)
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

func draw(points map[Point]int64) {
	minX, minY := int64(math.MaxInt64), int64(math.MaxInt64)
	maxX, maxY := int64(math.MinInt64), int64(math.MinInt64)

	for p := range points {
		if p.x < minX {
			minX = p.x
		}
		if p.x > maxX {
			maxX = p.x
		}
		if p.y < minY {
			minY = p.y
		}
		if p.y > maxY {
			maxY = p.y
		}
	}

	grid := make([][]int, maxY-minY+1)
	for i := range grid {
		grid[i] = make([]int, maxX-minX+1)
	}

	for p, v := range points {
		if v > 0 {
			grid[p.y-minY][p.x-minX] = int(v)
		}
	}

	for i := len(grid) - 1; i >= 0; i-- {
		for j := range grid[i] {
			if grid[i][j] == 1 {
				fmt.Print("█")
			} else {
				fmt.Print(" ")
			}
		}
		fmt.Printf("\n")
	}
}
