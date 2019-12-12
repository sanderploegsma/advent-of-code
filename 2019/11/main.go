package main

import (
	"fmt"
	"math"
	"os"
	"time"

	"github.com/sanderploegsma/advent-of-code/2019/intcode"
	"github.com/sanderploegsma/advent-of-code/2019/utils"
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
	instructions, err := utils.ReadIntCode("input.txt")
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
	in := make(chan int, 1)
	defer close(in)
	out := make(chan int)

	c := intcode.NewVM(in, out, instructions)
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

		color := <-out
		turn := <-out

		painted[position] = color
		position, direction = move(position, direction, turn)
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
	minX, minY := math.MaxInt64, math.MaxInt64
	maxX, maxY := math.MinInt64, math.MinInt64

	for p := range points {
		minX = utils.Min(minX, p.x)
		maxX = utils.Max(maxX, p.x)
		minY = utils.Min(minY, p.y)
		maxY = utils.Max(maxY, p.y)
	}

	grid := make([][]int, maxY-minY+1)
	for i := range grid {
		grid[i] = make([]int, maxX-minX+1)
	}

	for p, v := range points {
		if v > 0 {
			grid[p.y-minY][p.x-minX] = v
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
