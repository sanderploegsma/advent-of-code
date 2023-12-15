package main

import (
	"fmt"
	"os"

	"github.com/sanderploegsma/advent-of-code/2019/go/utils"
)

const (
	AsciiScaffold = 35
	AsciiSpace    = 46
	AsciiNewLine  = 10
)

func main() {
	instructions, err := utils.ReadIntCode("input.txt")
	if err != nil {
		fmt.Printf("failed to read input: %v\n", err)
		os.Exit(1)
	}

	in := make(chan int)
	out := make(chan int)

	go utils.RunIntCode(in, out, instructions)

	x, y := 0, 0
	points := make(map[Point]bool)
	for o := range out {
		switch o {
		case AsciiScaffold:
			points[Point{x, y}] = true
			x++
		case AsciiSpace:
			points[Point{x, y}] = false
			x++
		case AsciiNewLine:
			x = 0
			y++
		}
	}

	intersections := make([]Point, 0)
	for p := range points {
		if isIntersection(p, points) {
			intersections = append(intersections, p)
		}
	}

	sum := 0
	for _, i := range intersections {
		sum += i.X * i.Y
	}

	fmt.Println(sum)
}

func isIntersection(p Point, points map[Point]bool) bool {
	return points[p] &&
		points[Point{p.X + 1, p.Y}] &&
		points[Point{p.X - 1, p.Y}] &&
		points[Point{p.X, p.Y + 1}] &&
		points[Point{p.X, p.Y - 1}]
}

type Point struct {
	X, Y int
}
