package main

import (
	"fmt"
	"io/ioutil"
	"strconv"
	"strings"
	"time"
)

func main() {
	input, _ := ioutil.ReadFile("input.txt")
	paths := strings.Split(string(input), "\n")

	start := time.Now()
	result := DistanceToClosestIntersection(paths[0], paths[1])
	end := time.Since(start)
	fmt.Printf("[PART ONE] distance to closest intersection: %d (took %s)\n", result, end)
}

// DistanceToClosestIntersection finds all intersections between the given lines `a` and `b`,
// and returns the distance between the origin and the closest intersection.
func DistanceToClosestIntersection(a, b string) int {
	pathA := generatePath(a)
	pathB := generatePath(b)

	closest := point{0, 0}
	for i := range pathA {
		for j := range pathB {
			if !pathA[i].Equals(pathB[j]) {
				continue
			}

			if closest.DistanceToOrigin() == 0 || closest.DistanceToOrigin() > pathA[i].DistanceToOrigin() {
				closest = pathA[i]
			}
		}
	}

	return closest.DistanceToOrigin()
}

// generatePath converts instructions such as 'U1,R2' to a list of points from the origin,
// representing the path taken when following each instruction.
func generatePath(line string) []point {
	points := make([]point, 0)
	lastPoint := point{0, 0}
	for _, segmentBytes := range strings.Split(line, ",") {
		segment := string(segmentBytes)
		dir := segment[0:1]
		dist, _ := strconv.Atoi(segment[1:])

		for i := 1; i <= dist; i++ {
			dx, dy := move(dir)
			point := point{lastPoint.X + dx, lastPoint.Y + dy}
			points = append(points, point)
			lastPoint = point
		}
	}

	return points
}

// move returns (dx,dy) based on the given direction.
// For example, direction "U" (up) yields (dx,dy) = (0,1)
func move(dir string) (dx, dy int) {
	switch dir {
	case "U":
		return 0, 1
	case "D":
		return 0, -1
	case "L":
		return -1, 0
	case "R":
		return 1, 0
	default:
		return 0, 0 // This should of course never happen
	}
}

type point struct {
	X, Y int
}

// DistanceToOrigin returns the Manhattan Distance to the origin (0, 0)
func (p *point) DistanceToOrigin() int {
	return abs(p.X) + abs(p.Y)
}

// Equals returns true if the given point has the same coordinates as this
func (p *point) Equals(o point) bool {
	return p.X == o.X && p.Y == o.Y
}

func abs(x int) int {
	if x >= 0 {
		return x
	}

	return -x
}
