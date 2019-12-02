package main

import (
	"io/ioutil"
	"log"
	"strconv"
	"strings"
)

func main() {
	program, err := parseProgram("input.txt")
	if err != nil {
		log.Fatalf("unable to read file: %v", err)
	}

	log.Printf("[PART ONE] result: %v", RunProgramWithInput(program, 12, 2))

	noun, verb := FindNounAndVerb(program, 19690720)
	log.Printf("[PART TWO] result: 100 * %d + %d = %d", noun, verb, 100*noun+verb)
}

func RunProgram(program []int) []int {
	return RunProgramWithInput(program, program[1], program[2])
}

func RunProgramWithInput(program []int, noun, verb int) []int {
	result := make([]int, len(program))
	for i := range program {
		result[i] = program[i]
	}
	result[1] = noun
	result[2] = verb

	index := 0
	for index <= len(result)-1 {
		opcode := result[index]

		if opcode == 99 {
			break
		}

		x := result[result[index+1]]
		y := result[result[index+2]]
		pos := result[index+3]

		if opcode == 1 {
			result[pos] = x + y
		}

		if opcode == 2 {
			result[pos] = x * y
		}

		index += 4
	}

	return result
}

func FindNounAndVerb(program []int, outcome int) (noun int, verb int) {
	for noun := 0; noun < 100; noun++ {
		for verb := 0; verb < 100; verb++ {
			result := RunProgramWithInput(program, noun, verb)
			if result[0] == outcome {
				return noun, verb
			}
		}
	}
	return noun, verb
}

func parseProgram(file string) (input []int, err error) {
	text, err := ioutil.ReadFile(file)
	if err != nil {
		return nil, err
	}

	for _, item := range strings.Split(string(text), ",") {
		val, err := strconv.Atoi(item)
		if err != nil {
			return nil, err
		}
		input = append(input, val)
	}

	return input, err
}
