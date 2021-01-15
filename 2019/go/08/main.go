package main

import (
	"fmt"
	"math"
	"strconv"

	"github.com/sanderploegsma/advent-of-code/2019/utils"
)

const w, h = 25, 6

func main() {
	input, _ := utils.ReadFile("input.txt")
	layers := ParseLayers(input)

	fmt.Println(CheckLayers(layers))

	image := OverlayLayers(layers)
	for i := 0; i < w*h; i++ {
		if i%w == 0 {
			fmt.Print("\n")
		}
		if image[i] == 1 {
			fmt.Print("█")
		} else {
			fmt.Print(" ")
		}
	}
}

func CheckLayers(layers [][]int) (out int) {
	minZeroes := math.MaxInt32
	for _, l := range layers {
		zeroes, ones, twos := 0, 0, 0
		for _, d := range l {
			switch d {
			case 0:
				zeroes++
			case 1:
				ones++
			case 2:
				twos++
			}
		}
		if zeroes < minZeroes {
			minZeroes = zeroes
			out = ones * twos
		}
	}
	return out
}

func OverlayLayers(layers [][]int) []int {
	res := make([]int, w*h)
	for i := range res {
		res[i] = 2
	}

	for _, l := range layers {
		for i, d := range l {
			if res[i] == 2 {
				res[i] = d
			}
		}
	}

	return res
}

func ParseLayers(input string) [][]int {
	layers := make([][]int, len(input)/(w*h))
	cur := make([]int, w*h)

	x := 0
	y := 0
	for i := 0; i < len(input); i++ {
		if x >= w*h {
			layers[y] = cur
			cur = make([]int, w*h)
			x = 0
			y++
		}

		cur[x], _ = strconv.Atoi(string(input[i]))
		x++
	}

	layers[y] = cur
	return layers
}
