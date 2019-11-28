package main

import (
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

	id, minute := FindGuardMostAsleep(lines)
	log.Printf("Guard %d sleeps the most. The minute he is most asleep is: %d. %d * %d = %d\n", id, minute, id, minute, id*minute)

	id, minute = FindGuardMostFrequentlyAsleep(lines)
	log.Printf("Guard %d is asleep most frequently on the same minute (minute %d). %d * %d = %d\n", id, minute, id, minute, id*minute)
}

// Schedule contains a count of times a guard is asleep for each minute (0-59) for each guard.
type Schedule map[int][]int

// Statistics describe the sleep patterns of a single guard.
type Statistics struct {
	TotalMinutes       int
	MaxFrequency       int
	MinuteMostFrequent int
}

// FindGuardMostAsleep looks for the guard that is asleep the most.
// It returns the ID of the found guard, together with the minute that he is asleep most frequently on.
func FindGuardMostAsleep(input []string) (guard int, minute int) {
	schedule := createSchedule(input)
	stats := createStatistics(schedule)

	maxTotal := 0
	for id, s := range stats {
		if s.TotalMinutes > maxTotal {
			guard = id
			maxTotal = s.TotalMinutes
		}
	}

	return guard, stats[guard].MinuteMostFrequent
}

// FindGuardMostFrequentlyAsleep looks for the guard that is most frequently asleep on the same minute.
// It returns the ID of the found guard, together with the minute that he is asleep most frequently on.
func FindGuardMostFrequentlyAsleep(input []string) (guard int, minute int) {
	schedule := createSchedule(input)
	stats := createStatistics(schedule)

	maxFrequency := 0
	for id, s := range stats {
		if s.MaxFrequency > maxFrequency {
			guard = id
			maxFrequency = s.MaxFrequency
		}
	}

	return guard, stats[guard].MinuteMostFrequent
}

// createStatistics converts the given schedule to a map of statistics per guard.
func createStatistics(schedule Schedule) map[int]Statistics {
	result := make(map[int]Statistics)
	for guardId, minutes := range schedule {
		stats := Statistics{}

		for minute, frequency := range minutes {
			if frequency > stats.MaxFrequency {
				stats.MaxFrequency = frequency
				stats.MinuteMostFrequent = minute
			}
			stats.TotalMinutes += frequency
		}

		result[guardId] = stats
	}
	return result
}

// createSchedule takes the given input and creates a schedule from it.
func createSchedule(input []string) Schedule {
	records := make([]string, len(input))
	for i := range input {
		records[i] = input[i]
	}

	sort.Strings(records)

	minute := 0
	guard := -1
	asleep := false
	schedule := make(Schedule)

	newShiftPattern := regexp.MustCompile(".*Guard #(\\d+) begins shift")

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

	return schedule
}
