package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"regexp"
	"strconv"
	"strings"
)

func main() {
	input, err := ioutil.ReadFile("input.txt")
	if err != nil {
		log.Fatalf("unable to read file: %v", err)
	}
	lines := strings.Split(string(input), "\n")

	overlaps, err := FindOverlaps(lines)
	if err != nil {
		log.Fatalf("error finding overlaps: %v", err)
	}

	log.Printf("overlapping inches: %d", overlaps)
}

type claim struct {
	ID         int
	X, Y, W, H int
}

// FindOverlaps returns the number of square inches that are within two or more claims.
//
// A claim like
//
// 	#123 @ 3,2: 5x4
//
// means that claim ID 123 specifies a rectangle 3 inches from the left edge, 2 inches from the top edge, 5 inches wide, and 4 inches tall.
func FindOverlaps(input []string) (int, error) {
	claims, err := parseClaims(input)
	if err != nil {
		return 0, fmt.Errorf("failed to parse claims: %v", err)
	}

	visited := make([][]int, 1000)
	for i := range visited {
		visited[i] = make([]int, 1000)
	}

	for _, claim := range claims {
		for x := claim.X; x < claim.X+claim.W; x++ {
			for y := claim.Y; y < claim.Y+claim.H; y++ {
				visited[x][y] = visited[x][y] + 1
			}
		}
	}

	count := 0
	for x := range visited {
		for y := range visited[x] {
			if visited[x][y] > 1 {
				count++
			}
		}
	}

	return count, nil
}

func parseClaims(input []string) ([]claim, error) {
	pattern, err := regexp.Compile("#(\\d+) @ (\\d+),(\\d+): (\\d+)x(\\d+)")
	if err != nil {
		return nil, fmt.Errorf("failed to compile pattern: %v", err)
	}

	res := make([]claim, len(input))

	for i, item := range input {
		if !pattern.MatchString(item) {
			return nil, fmt.Errorf("unable to parse line %d", i)
		}

		matches := pattern.FindStringSubmatch(item)
		id, _ := strconv.Atoi(matches[1])
		x, _ := strconv.Atoi(matches[2])
		y, _ := strconv.Atoi(matches[3])
		w, _ := strconv.Atoi(matches[4])
		h, _ := strconv.Atoi(matches[5])

		res[i] = claim{
			ID: id,
			X:  x,
			Y:  y,
			W:  w,
			H:  h,
		}
	}

	return res, nil
}
