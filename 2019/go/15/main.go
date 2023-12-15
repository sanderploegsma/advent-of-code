package main

import (
	"fmt"
	"math"
	"os"

	"github.com/sanderploegsma/advent-of-code/2019/go/utils"
)

const (
	MoveNorth = 1
	MoveSouth = 2
	MoveWest  = 3
	MoveEast  = 4
)

const (
	StatusWall  = 0
	StatusMoved = 1
	StatusFound = 2
)

func main() {
	instructions, err := utils.ReadIntCode("input.txt")
	if err != nil {
		fmt.Printf("failed to read input: %v", err)
		os.Exit(1)
	}

	in := make(chan int, 1)
	out := make(chan int)
	go utils.RunIntCode(in, out, instructions)

	dist, _ := FindOxygenTank(in, out, Point{0, 0}, make(map[Point]bool), 0, math.MaxInt64)
	fmt.Println(dist)

	distances := make(map[Point]int)
	Explore(in, out, Point{-16, -14}, make(map[Point]bool), 0, distances)

	max := 0
	for _, v := range distances {
		max = utils.Max(max, v)
	}

	fmt.Println(max)
}

type Point struct {
	X, Y int
}

func FindOxygenTank(in, out chan int, p Point, visited map[Point]bool, dist, minDist int) (int, bool) {
	visited[p] = true

	tryMove := func(dir, dirBack int, newP Point) (res int, found bool) {
		res = minDist
		if !visited[newP] {
			in <- dir
			s := <-out
			switch s {
			case StatusMoved:
				res, found = FindOxygenTank(in, out, newP, visited, dist+1, minDist)
				if !found {
					in <- dirBack
					<-out
				}
			case StatusFound:
				found = true
				res = utils.Min(minDist, dist+1)
			}
		}
		return res, found
	}

	if minDist, found := tryMove(MoveEast, MoveWest, Point{X: p.X + 1, Y: p.Y}); found {
		return minDist, true
	}
	if minDist, found := tryMove(MoveWest, MoveEast, Point{X: p.X - 1, Y: p.Y}); found {
		return minDist, true
	}
	if minDist, found := tryMove(MoveNorth, MoveSouth, Point{X: p.X, Y: p.Y + 1}); found {
		return minDist, true
	}
	if minDist, found := tryMove(MoveSouth, MoveNorth, Point{X: p.X, Y: p.Y - 1}); found {
		return minDist, true
	}

	return minDist, false
}

func Explore(in, out chan int, p Point, visited map[Point]bool, dist int, distances map[Point]int) {
	visited[p] = true
	if d, ok := distances[p]; ok {
		distances[p] = utils.Min(d, dist)
	} else {
		distances[p] = dist
	}

	tryMove := func(dir, dirBack int, newP Point) {
		if !visited[newP] {
			in <- dir
			s := <-out
			if s != StatusWall {
				Explore(in, out, newP, visited, dist+1, distances)
				in <- dirBack
				<-out
			}
		} else {
			if distances[newP] > dist+1 {
				distances[newP] = dist + 1
			}
		}
	}

	tryMove(MoveEast, MoveWest, Point{X: p.X + 1, Y: p.Y})
	tryMove(MoveWest, MoveEast, Point{X: p.X - 1, Y: p.Y})
	tryMove(MoveNorth, MoveSouth, Point{X: p.X, Y: p.Y + 1})
	tryMove(MoveSouth, MoveNorth, Point{X: p.X, Y: p.Y - 1})
}
