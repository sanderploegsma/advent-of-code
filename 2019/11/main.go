package main

import (
	"fmt"
	"math"
	"os"
	"time"

	"github.com/sanderploegsma/advent-of-code/2019/intcode"
)

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
	instructions, err := intcode.ReadInstructions("input.txt")
	if err != nil {
		fmt.Printf("failed to read input: %v\n", err)
		os.Exit(1)
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

type Point struct{ x, y int }

func Paint(instructions []int, startingColor int) map[Point]int {
	in := make(chan int)
	defer close(in)
	out := make(chan int)

	c := intcode.NewComputer(in, out, instructions)
	go c.Run()

	painted := make(map[Point]int)
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

func draw(points map[Point]int) {
	minX, minY := int(math.MaxInt64), int(math.MaxInt64)
	maxX, maxY := int(math.MinInt64), int(math.MinInt64)

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
