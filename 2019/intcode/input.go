package intcode

import (
	"fmt"
	"strconv"

	"github.com/sanderploegsma/advent-of-code/2019/reader"
)

func ReadInstructions(file string) ([]int, error) {
	input, err := reader.ReadDelim(file, ",")
	if err != nil {
		return nil, fmt.Errorf("failed to read file %s: %w", file, err)
	}

	instructions := make([]int, len(input))
	for i := range input {
		instructions[i], err = strconv.Atoi(input[i])
		if err != nil {
			return nil, fmt.Errorf("unable to parse '%s' as int: %w", input[i], err)
		}
	}

	return instructions, nil
}
