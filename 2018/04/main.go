package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"regexp"
	"sort"
	"strconv"
	"strings"
)

func main() {
	input, err := ioutil.ReadFile("input.txt")
	if err != nil {
		log.Fatalf("unable to read file: %v", err)
	}
	lines := strings.Split(string(input), "\n")

	id, minute, err := FindGuardMostAsleep(lines)
	if err != nil {
		log.Fatal(err)
	}

	log.Printf("Guard %d sleeps the most. The minute he is most asleep is: %d. %d * %d = %d\n", id, minute, id, minute, id*minute)
}

// FindGuardMostAsleep looks for the guard that is asleep the most, and returns the guard's ID together with the minute that guard is asleep the most.
func FindGuardMostAsleep(input []string) (id int, min int, err error) {
	schedule, err := createSchedule(input)
	if err != nil {
		return 0, 0, fmt.Errorf("unable to create schedule from input: %v", err)
	}

	guard := 0
	maxTotalMinutesAsleep := 0
	minuteMostAsleepPerGuard := make(map[int]int)

	for guardId, minutes := range schedule {
		totalMinutesAsleep := 0
		minuteMostAsleep := 0
		maxTimesAsleep := 0

		for i, timesAsleep := range minutes {
			if timesAsleep > maxTimesAsleep {
				maxTimesAsleep = timesAsleep
				minuteMostAsleep = i
			}
			totalMinutesAsleep += timesAsleep
		}

		minuteMostAsleepPerGuard[guardId] = minuteMostAsleep

		if totalMinutesAsleep > maxTotalMinutesAsleep {
			maxTotalMinutesAsleep = totalMinutesAsleep
			guard = guardId
		}
	}

	return guard, minuteMostAsleepPerGuard[guard], nil
}

// Schedule contains a count of times a guard is asleep for each minute (0-59) for each guard
type Schedule map[int][]int

var newShiftPattern = regexp.MustCompile(".*Guard #(\\d+) begins shift")

// createSchedule takes the given input and creates a schedule from it
func createSchedule(input []string) (Schedule, error) {
	records := make([]string, len(input))
	for i := range input {
		records[i] = input[i]
	}

	sort.Strings(records)

	minute := 0
	guard := -1
	asleep := false
	schedule := make(Schedule)

	for len(records) > 0 {
		record := records[0]
		currentMinute, _ := strconv.Atoi(record[15:17])

		if minute == 0 && newShiftPattern.MatchString(record) {
			match := newShiftPattern.FindStringSubmatch(record)
			guard, _ = strconv.Atoi(match[1])
			records = records[1:]

			if schedule[guard] == nil {
				schedule[guard] = make([]int, 60)
			}
		}

		if strings.Contains(record, "falls asleep") && currentMinute == minute {
			asleep = true
			records = records[1:]
		}

		if strings.Contains(record, "wakes up") && currentMinute == minute {
			asleep = false
			records = records[1:]
		}

		if asleep {
			schedule[guard][minute] = schedule[guard][minute] + 1
		}

		minute = (minute + 1) % 60
	}

	return schedule, nil
}
