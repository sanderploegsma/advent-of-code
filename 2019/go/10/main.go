package main

import (
	"fmt"
	"math"
	"sort"
	"strings"
	"time"

	"github.com/sanderploegsma/advent-of-code/2019/go/utils"
)

func main() {
	input, _ := utils.ReadFile("input.txt")
	asteroids := ParseInput(input)

	start := time.Now()
	p, num := PartOne(asteroids)
	fmt.Printf("[PART ONE] position (%d, %d) can detect %d asteroids (took %s)\n", p.x, p.y, num, time.Since(start))

	start = time.Now()
	destroyed := PartTwo(asteroids, p)
	fmt.Printf("[PART TWO] 200th destroyed asteroid: (%d, %d) (took %s)\n", destroyed[199].x, destroyed[199].y, time.Since(start))
}

// Point describes a point in a 2-dimensional plane
type Point struct{ x, y int }

// DistanceTo returns the distance between this point and the given other point
func (p *Point) DistanceTo(o Point) float64 {
	return math.Sqrt(math.Pow(float64(p.x-o.x), 2) + math.Pow(float64(p.y-o.y), 2))
}

// AngleTo returns the angle in radians of the other point w.r.t. this point
func (p *Point) AngleTo(o Point) float64 {
	return math.Atan2(float64(o.x-p.x), float64(o.y-p.y))
}

// ParseInput parses the given input into a list of points
func ParseInput(input string) (asteroids []Point) {
	rows := strings.Split(input, "\n")

	for y, row := range rows {
		for x := 0; x < len(rows[0]); x++ {
			if string(row[x]) == "#" {
				asteroids = append(asteroids, Point{x, y})
			}
		}
	}

	return asteroids
}

// PartOne - Find the asteroid that can detect the most other asteroids (ones that are directly in its line of sight)
func PartOne(asteroids []Point) (p Point, num int) {
	for _, a := range asteroids {
		slopes := make([]float64, 0)
		for _, b := range asteroids {
			if a.x == b.x && a.y == b.y {
				continue
			}

			d := a.AngleTo(b)
			exists := false
			for _, s := range slopes {
				if d == s {
					exists = true
				}
			}

			if !exists {
				slopes = append(slopes, d)
			}
		}

		if len(slopes) > num {
			num = len(slopes)
			p = a
		}
	}

	return p, num
}

// PartTwo - Destroy all other asteroids with a laser mounted on the given origin.
func PartTwo(asteroids []Point, origin Point) (destroyed []Point) {
	targets := make(map[float64][]Point)
	for _, a := range asteroids {
		if a.x == origin.x && a.y == origin.y {
			continue
		}

		// Calculate the angle wrt origin, offsetting by 45 degrees so that directly upwards counts as 0. Also, since radians go counter-clockwise, multiply by -1
		angle := (origin.AngleTo(a) - 0.5*math.Pi) * -1
		if _, ok := targets[angle]; !ok {
			targets[angle] = make([]Point, 0)
		}

		targets[angle] = append(targets[angle], a)
		// Store the targets in order ascending from closest to origin
		sort.Slice(targets[angle], func(i, j int) bool {
			return origin.DistanceTo(targets[angle][i]) < origin.DistanceTo(targets[angle][j])
		})
	}

	// Order the angles ascending
	order := make([]float64, 0)
	for d := range targets {
		order = append(order, d)
	}
	sort.Float64s(order)

	// Kill 'em all
	i := 0
	for len(destroyed) < len(asteroids)-1 {
		d := order[i%len(order)]
		if len(targets[d]) > 0 {
			destroyed = append(destroyed, targets[d][0])
			targets[d] = targets[d][1:]
		}
		i++
	}

	return destroyed
}
