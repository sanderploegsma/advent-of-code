package main

import (
	"strings"
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestPartOne(t *testing.T) {
	input := `
......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####
	`

	asteroids := ParseInput(strings.TrimSpace(input))
	p, num := PartOne(asteroids)

	assert.Equal(t, 5, p.x)
	assert.Equal(t, 8, p.y)
	assert.Equal(t, 33, num)
}

func TestPartTwo(t *testing.T) {
	input := `
.#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##
`

	asteroids := ParseInput(strings.TrimSpace(input))
	destroyed := PartTwo(asteroids, Point{11, 13})

	assert.Equal(t, 11, destroyed[0].x)
	assert.Equal(t, 12, destroyed[0].y)

	assert.Equal(t, 12, destroyed[1].x)
	assert.Equal(t, 1, destroyed[1].y)

	assert.Equal(t, 12, destroyed[2].x)
	assert.Equal(t, 2, destroyed[2].y)

	assert.Equal(t, 12, destroyed[9].x)
	assert.Equal(t, 8, destroyed[9].y)

	assert.Equal(t, 16, destroyed[19].x)
	assert.Equal(t, 0, destroyed[19].y)

	assert.Equal(t, 16, destroyed[49].x)
	assert.Equal(t, 9, destroyed[49].y)

	assert.Equal(t, 10, destroyed[99].x)
	assert.Equal(t, 16, destroyed[99].y)

	assert.Equal(t, 9, destroyed[198].x)
	assert.Equal(t, 6, destroyed[198].y)

	assert.Equal(t, 8, destroyed[199].x)
	assert.Equal(t, 2, destroyed[199].y)
}
