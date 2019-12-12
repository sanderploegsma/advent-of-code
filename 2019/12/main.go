package main

import (
	"fmt"
	"reflect"

	"github.com/sanderploegsma/advent-of-code/2019/utils"
)

var input = [][]int{
	{3, 2, -6, 0, 0, 0},
	{-13, 18, 10, 0, 0, 0},
	{-8, -1, 13, 0, 0, 0},
	{5, 10, 4, 0, 0, 0},
}

func main() {
	fmt.Println(TotalEnergy(SimulateN(input, 1000)))
	fmt.Println(CalculateCycle(input))
}

// CalculateCycle finds the number of steps needed to bring the given moons back to their original position.
// Because the movement on each axis is not influenced by the the other axes, we can determine the cycle for each axis
// individually, and then use the Least Common Multiplier to calculate the combined cycle.
func CalculateCycle(moons [][]int) int {
	repeats := make(map[int]int)

	for i := 0; i < 3; i++ {
		steps := 0
		sim := clone(moons)
		for {
			steps++
			sim = SimulateAxis(sim, i)
			if reflect.DeepEqual(sim, moons) {
				repeats[i] = steps
				break
			}
		}
	}

	return utils.LCM(repeats[0], repeats[1], repeats[2])
}

// SimulateN simulates the movement of the given moons for n steps.
func SimulateN(moons [][]int, n int) [][]int {
	sim := clone(moons)

	for i := 0; i < n; i++ {
		sim = Simulate(sim)
	}
	return sim
}

// Simulate simulates a single movement step for all given moons.
func Simulate(moons [][]int) [][]int {
	sim := clone(moons)

	for i := 0; i < len(moons); i++ {
		for j := i + 1; j < len(moons); j++ {
			for axis := 0; axis < 3; axis++ {
				dx1, dx2 := gravity(moons[i][axis], moons[j][axis])
				sim[i][axis+3] += dx1
				sim[j][axis+3] += dx2
			}
		}
	}

	for i := range sim {
		for axis := 0; axis < 3; axis++ {
			sim[i][axis] += sim[i][axis+3]
		}
	}

	return sim
}

// SimulateAxis simulates a single movement step for a single given axis of all moons
func SimulateAxis(moons [][]int, axis int) [][]int {
	sim := clone(moons)

	for i := 0; i < len(moons); i++ {
		for j := i + 1; j < len(moons); j++ {
			di, dj := gravity(moons[i][axis], moons[j][axis])
			sim[i][axis+3] += di
			sim[j][axis+3] += dj
		}
	}

	for i := range sim {
		sim[i][axis] += sim[i][axis+3]
	}

	return sim
}

// TotalEnergy calculates the total amount of energy of all given moons.
func TotalEnergy(moons [][]int) int {
	energy := 0
	for _, m := range moons {
		energy += (utils.Abs(m[0]) + utils.Abs(m[1]) + utils.Abs(m[2])) * (utils.Abs(m[3]) + utils.Abs(m[4]) + utils.Abs(m[5]))
	}
	return energy
}

func gravity(a, b int) (int, int) {
	if a == b {
		return 0, 0
	}
	if a > b {
		return -1, 1
	}
	return 1, -1
}

func clone(moons [][]int) [][]int {
	c := make([][]int, len(moons))
	for i := range moons {
		c[i] = make([]int, len(moons[i]))
		copy(c[i], moons[i])
	}
	return c
}
