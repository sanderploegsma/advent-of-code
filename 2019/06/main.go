package main

import (
	"fmt"
	"strings"
	"time"

	"github.com/sanderploegsma/advent-of-code/2019/reader"
)

func main() {
	input, _ := reader.ReadLines("input.txt")

	start := time.Now()
	result := Checksum(input)
	fmt.Printf("[PART ONE] checksum: %d (took %s)\n", result, time.Since(start))

	start = time.Now()
	result = Traverse(input, "YOU", "SAN")
	fmt.Printf("[PART TWO] required transfers: %d (took %s)\n", result, time.Since(start))
}

type Object struct {
	Name  string
	Level int
}

// Checksum calculates the total number of direct and indirect orbits in the given input
func Checksum(input []string) int {
	checksum := 0
	visited := make(map[string]bool)
	queue := make([]Object, 0)
	queue = append(queue, Object{"COM", 0})

	for len(queue) > 0 {
		i := queue[0]
		queue = queue[1:]

		visited[i.Name] = true
		checksum += i.Level

		neighbours := FindNeighbours(input, i.Name)
		if len(neighbours) > 0 {
			for _, n := range neighbours {
				if !visited[n] {
					queue = append(queue, Object{n, i.Level + 1})
				}
			}
		}

	}

	return checksum
}

// Traverse calculates the shortest distance from `src` to `dest` in the given input
func Traverse(input []string, src, dest string) int {
	dist := make(map[string]int)
	visited := make(map[string]bool)
	queue := make([]string, 0)
	queue = append(queue, src)
	dist[src] = 0

	for len(queue) > 0 {
		i := queue[0]
		queue = queue[1:]

		visited[i] = true
		neighbours := FindNeighbours(input, i)
		if len(neighbours) > 0 {
			relativeDist := dist[i] + 1

			for _, n := range neighbours {
				if !visited[n] {
					if d, ok := dist[n]; !ok || d > relativeDist {
						dist[n] = relativeDist
					}
					queue = append(queue, n)
				}
			}
		}
	}

	return dist[dest] - 2
}

func FindNeighbours(orbits []string, obj string) []string {
	result := make([]string, 0)
	for i := range orbits {
		if strings.HasPrefix(orbits[i], obj) {
			result = append(result, strings.Split(orbits[i], ")")[1])
		}
		if strings.HasSuffix(orbits[i], obj) {
			result = append(result, strings.Split(orbits[i], ")")[0])
		}
	}
	return result
}
