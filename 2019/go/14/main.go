package main

import (
	"fmt"
	"math"
	"os"
	"regexp"
	"strconv"

	"github.com/sanderploegsma/advent-of-code/2019/go/utils"
)

func main() {
	input, err := utils.ReadLines("input.txt")
	if err != nil {
		fmt.Printf("failed to read input file: %v\n", err)
		os.Exit(1)
	}
	r := parse(input)
	orePerFuel := OreNeededForFuel(r, 1)
	fmt.Println(orePerFuel)
	fmt.Println(MaximizeFuelForOre(r, 1000000000000, orePerFuel))
}

type chemical struct {
	name     string
	quantity int
}

func parse(input []string) map[chemical][]chemical {
	var r = regexp.MustCompile("(\\d+) ([A-Z]+)")
	res := make(map[chemical][]chemical)
	for _, s := range input {
		matches := r.FindAllStringSubmatch(s, -1)
		l := make([]chemical, len(matches)-1)
		for i := 0; i < len(matches)-1; i++ {
			q, _ := strconv.Atoi(matches[i][1])
			l[i] = chemical{matches[i][2], q}
		}
		q, _ := strconv.Atoi(matches[len(matches)-1][1])
		root := chemical{matches[len(matches)-1][2], q}
		res[root] = l
	}
	return res
}

func MaximizeFuelForOre(r map[chemical][]chemical, ore, orePerFuel int) int {
	fuel := ore / orePerFuel
	steps := []int{1000, 100, 10, 1}
	for {
		newFuel := fuel + steps[0]
		needed := OreNeededForFuel(r, newFuel)
		if needed <= ore {
			fuel = newFuel
		} else {
			steps = steps[1:]
			if len(steps) == 0 {
				return fuel
			}
		}
	}
}

func OreNeededForFuel(r map[chemical][]chemical, fuel int) int {
	produced := make(map[string]int)
	wasted := make(map[string]int)
	queue := make([]chemical, 0)
	queue = append(queue, chemical{"FUEL", fuel})

	for len(queue) > 0 {
		i := queue[0]
		queue = queue[1:]

		needed := i.quantity
		for wasted[i.name] > 0 && needed > 0 {
			needed--
			wasted[i.name]--
		}

		produced[i.name] += needed

		var needsProducing bool
		var batchSize int
		var ingredients []chemical
		for k, v := range r {
			if k.name == i.name {
				needsProducing = true
				batchSize = k.quantity
				ingredients = v
			}
		}

		if !needsProducing || needed == 0 {
			continue
		}

		batches := int(math.Ceil(float64(needed) / float64(batchSize)))
		total := batches * batchSize
		waste := total - needed
		wasted[i.name] += waste
		for _, chem := range ingredients {
			queue = append(queue, chemical{chem.name, chem.quantity * batches})
		}

	}

	return produced["ORE"]
}
