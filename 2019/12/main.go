package main

import (
	"fmt"
	"reflect"
)

var input = []Moon{
	{Position: XYZ{3, 2, -6}},
	{Position: XYZ{-13, 18, 10}},
	{Position: XYZ{-8, -1, 13}},
	{Position: XYZ{5, 10, 4}},
}

func main() {
	fmt.Println(TotalEnergy(SimulateN(input, 1000)))
	fmt.Println(RepeatUntilPreviousState(input))
}

type Moon struct {
	Position, Velocity XYZ
}

type XYZ struct {
	X, Y, Z int
}

func (m *Moon) PotentialEnergy() int {
	return abs(m.Position.X) + abs(m.Position.Y) + abs(m.Position.Z)
}
func (m *Moon) KineticEnergy() int {
	return abs(m.Velocity.X) + abs(m.Velocity.Y) + abs(m.Velocity.Z)
}

func RepeatUntilPreviousState(moons []Moon) int {
	repeats := make(map[int]int)

	for i := 0; i < 3; i++ {
		steps := 0
		sim := make([]Moon, len(moons))
		copy(sim, moons)
		for {
			steps++
			sim = SimulateAxis(sim, i)
			if reflect.DeepEqual(sim, moons) {
				repeats[i] = steps
				break
			}
		}
	}

	return lcm(repeats[0], repeats[1], repeats[2])
}

func SimulateN(moons []Moon, n int) []Moon {
	sim := make([]Moon, len(moons))
	copy(sim, moons)

	for i := 0; i < n; i++ {
		sim = Simulate(sim)
	}
	return sim
}

func Simulate(moons []Moon) []Moon {
	sim := make([]Moon, len(moons))
	copy(sim, moons)

	for i := 0; i < len(moons); i++ {
		for j := i + 1; j < len(moons); j++ {
			dx1, dx2 := gravity(moons[i].Position.X, moons[j].Position.X)
			sim[i].Velocity.X += dx1
			sim[j].Velocity.X += dx2
			dy1, dy2 := gravity(moons[i].Position.Y, moons[j].Position.Y)
			sim[i].Velocity.Y += dy1
			sim[j].Velocity.Y += dy2
			dz1, dz2 := gravity(moons[i].Position.Z, moons[j].Position.Z)
			sim[i].Velocity.Z += dz1
			sim[j].Velocity.Z += dz2
		}
	}

	for i := range sim {
		sim[i].Position.X += sim[i].Velocity.X
		sim[i].Position.Y += sim[i].Velocity.Y
		sim[i].Position.Z += sim[i].Velocity.Z
	}

	return sim
}

func SimulateAxis(moons []Moon, axis int) []Moon {
	sim := make([]Moon, len(moons))
	copy(sim, moons)

	for i := 0; i < len(moons); i++ {
		for j := i + 1; j < len(moons); j++ {
			if axis == 0 {
				dx1, dx2 := gravity(moons[i].Position.X, moons[j].Position.X)
				sim[i].Velocity.X += dx1
				sim[j].Velocity.X += dx2
			}
			if axis == 1 {
				dy1, dy2 := gravity(moons[i].Position.Y, moons[j].Position.Y)
				sim[i].Velocity.Y += dy1
				sim[j].Velocity.Y += dy2
			}
			if axis == 2 {
				dz1, dz2 := gravity(moons[i].Position.Z, moons[j].Position.Z)
				sim[i].Velocity.Z += dz1
				sim[j].Velocity.Z += dz2
			}
		}
	}

	for i := range sim {
		if axis == 0 {
			sim[i].Position.X += sim[i].Velocity.X
		}
		if axis == 1 {
			sim[i].Position.Y += sim[i].Velocity.Y
		}
		if axis == 2 {
			sim[i].Position.Z += sim[i].Velocity.Z
		}
	}

	return sim
}

func TotalEnergy(moons []Moon) int {
	energy := 0
	for _, m := range moons {
		energy += m.KineticEnergy() * m.PotentialEnergy()
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

func abs(i int) int {
	if i >= 0 {
		return i
	}
	return -1 * i
}

func gcd(a, b int) int {
	if b == 0 {
		return a
	}
	return gcd(b, a%b)
}

func lcm(a, b int, integers ...int) int {
	result := a * b / gcd(a, b)

	for i := 0; i < len(integers); i++ {
		result = lcm(result, integers[i])
	}

	return result
}
