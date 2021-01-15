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

	log.Printf("overlapping inches: %d\n", overlaps)

	claimId, err := FindNonOverlappingClaim(lines)
	if err != nil {
		log.Fatalf("error finding non-overlapping claim: %v", err)
	}

	log.Printf("non-overlapping claim: %d", claimId)
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

	canvas := draw(claims)

	count := 0
	for x := range canvas {
		for y := range canvas[x] {
			if canvas[x][y] > 1 {
				count++
			}
		}
	}

	return count, nil
}

// FindNonOverlappingClaim looks for the claim that does not overlap with any other claim. The resulting claim's ID is returned.
func FindNonOverlappingClaim(input []string) (int, error) {
	claims, err := parseClaims(input)
	if err != nil {
		return 0, fmt.Errorf("failed to parse claims: %v", err)
	}

	canvas := draw(claims)

	for _, claim := range claims {
		overlap := false
		for x := claim.X; x < claim.X+claim.W; x++ {
			for y := claim.Y; y < claim.Y+claim.H; y++ {
				if canvas[x][y] > 1 {
					overlap = true
				}
			}
		}

		if !overlap {
			return claim.ID, nil
		}
	}

	return 0, fmt.Errorf("no claim found")
}

// draw creates a 2D canvas that contains the number of claims for each square inch.
func draw(claims []claim) [][]int {
	canvas := make([][]int, 1000)
	for i := range canvas {
		canvas[i] = make([]int, 1000)
	}

	for _, claim := range claims {
		for x := claim.X; x < claim.X+claim.W; x++ {
			for y := claim.Y; y < claim.Y+claim.H; y++ {
				canvas[x][y] = canvas[x][y] + 1
			}
		}
	}

	return canvas
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
