package main

import (
	"fmt"
	"os"

	"github.com/sanderploegsma/advent-of-code/2019/go/utils"
)

const (
	TileEmpty  = 0
	TileWall   = 1
	TileBlock  = 2
	TilePaddle = 3
	TileBall   = 4
)

func main() {
	instructions, err := utils.ReadIntCode("input.txt")
	if err != nil {
		fmt.Printf("failed to read input file: %v\n", err)
		os.Exit(1)
	}

	fmt.Println(CountBlockTiles(instructions))
	fmt.Println(Play(instructions))
}

func CountBlockTiles(instructions []int) int {
	out := make(chan int)

	go utils.RunIntCode(make(chan int), out, instructions)

	blockTiles := 0
	for range out {
		<-out
		if TileBlock == <-out {
			blockTiles++
		}
	}

	return blockTiles
}

func Play(instructions []int) int {
	in := make(chan int, 1)
	out := make(chan int)
	instructions[0] = 2

	go utils.RunIntCode(in, out, instructions)

	score := 0
	ballX := -1
	paddleX := -1
	for x := range out {
		y := <-out
		v := <-out

		if x == -1 && y == 0 {
			score = v
			continue
		}

		switch v {
		case TilePaddle:
			paddleX = x
		case TileBall:
			ballX = x
			in <- utils.Compare(ballX, paddleX)
		}
	}

	return score
}
