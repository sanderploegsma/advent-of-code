package main

import (
	"fmt"
	"math"
	"strconv"

	"github.com/sanderploegsma/advent-of-code/2019/reader"
)

func main() {
	input, _ := reader.ReadFile("input.txt")
	image := Parse(input, 25, 6)

	fmt.Printf("[PART ONE] Answer: %d\n", CheckImage(image))
}

func CheckImage(img [][][]int) (out int) {
	minZeroes := math.MaxInt32
	for _, l := range img {
		zeroes, ones, twos := 0, 0, 0
		for _, r := range l {
			for _, d := range r {
				switch d {
				case 0:
					zeroes++
				case 1:
					ones++
				case 2:
					twos++
				}
			}
		}
		if zeroes < minZeroes {
			minZeroes = zeroes
			out = ones * twos
		}
	}
	return out
}

func Parse(input string, w, h int) [][][]int {
	layers := make([][][]int, 0)
	layer := make([][]int, 0)
	row := make([]int, 0)

	x := 0
	y := 0
	for i := 0; i < len(input); i++ {
		if x*y > w*h {
			layers = append(layers, layer)
			layer = make([][]int, 0)
			x = 0
			y = 0
		}

		if x > w {
			layer = append(layer, row)
			row = make([]int, w)
			x = 0
			y++
		}

		digit, _ := strconv.Atoi(string(input[i]))
		row = append(row, digit)
		x++
	}
	return layers
}
