package main

import (
	"fmt"
	"os"

	"github.com/sanderploegsma/advent-of-code/2019/go/utils"
)

func main() {
	instructions, err := utils.ReadIntCode("input.txt")
	if err != nil {
		fmt.Printf("failed to read input: %v\n", err)
		os.Exit(1)
	}

	sum := 0
	for x := 0; x < 50; x++ {
		for y := 0; y < 50; y++ {
			in := make(chan int)
			out := make(chan int)

			go utils.RunIntCode(in, out, instructions)
			in <- x
			in <- y
			sum += <-out
		}
	}

	fmt.Println(sum)
}
