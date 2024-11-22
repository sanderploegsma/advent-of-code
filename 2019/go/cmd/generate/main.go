package main

import (
	"fmt"
	"os"
	"path"
	"text/template"
)

type specialCharacters struct {
	BackTick string
}

var characters = specialCharacters{BackTick: "`"}

type data struct {
	Day               string
	Title             string
	SpecialCharacters specialCharacters
}

var solutionTemplate = template.Must(template.New("solution").Parse(
	`// Advent of Code 2019, Day {{ .Day }}: {{ .Title }}
package main

import (
    "io"
    "log"
    "os"
)

const InputFile = "input.txt"

// PartOne solves the first part of the puzzle.
func PartOne(reader io.Reader) int {
    return -1
}

// PartTwo solves the second part of the puzzle.
func PartTwo(reader io.Reader) int {
    return -1
}

func main() {
    file, err := os.Open(InputFile)
    if err != nil {
        log.Fatalf("Error opening %s: %v", InputFile, err)
    }
    defer file.Close()

    log.Printf("Part one: %d", PartOne(file))
    file.Seek(0, 0)
    log.Printf("Part two: %d", PartTwo(file))
}`))

var testTemplate = template.Must(template.New("test").Parse(
	`// Advent of Code 2019, Day {{ .Day }}: {{ .Title }}
package main

import (
    "strings"
    "testing"

    "github.com/stretchr/testify/assert"
)

var Example = strings.TrimSpace({{ .SpecialCharacters.BackTick }}

{{ .SpecialCharacters.BackTick }})

func TestPartOne(t *testing.T) {
    assert.Equal(t, -1, PartOne(strings.NewReader(Example)))
}

func TestPartTwo(t *testing.T) {
    assert.Equal(t, -1, PartTwo(strings.NewReader(Example)))
}
`))

var inputTemplate = template.Must(template.New("input").Parse(``))

func renderTemplate(t *template.Template, target string, data data) error {
	file, err := os.Create(target)

	if err != nil {
		return fmt.Errorf("failed to create file '%s': %v", target, err)
	}

	defer file.Close()

	if err := t.Execute(file, data); err != nil {
		return fmt.Errorf("failed to template file: %v", err)
	}

	return nil
}

func generate(day string, title string) error {
	data := data{
		Day:               day,
		Title:             title,
		SpecialCharacters: characters,
	}

	cwd, _ := os.Getwd()
	dir := path.Join(cwd, day)
	err := os.Mkdir(path.Join(cwd, day), 0755)

	if os.IsExist(err) {
		return fmt.Errorf("directory '%s' already exists", dir)
	}

	if err != nil {
		return fmt.Errorf("failed to create directory '%s': %v", dir, err)
	}

	if err = renderTemplate(solutionTemplate, path.Join(dir, "main.go"), data); err != nil {
		return fmt.Errorf("failed to generate solution file: %v", err)
	}

	if err = renderTemplate(testTemplate, path.Join(dir, "main_test.go"), data); err != nil {
		return fmt.Errorf("failed to generate test file: %v", err)
	}

	if err = renderTemplate(inputTemplate, path.Join(dir, "input.txt"), data); err != nil {
		return fmt.Errorf("failed to generate input file: %v", err)
	}

	return nil
}

func main() {
	args := os.Args

	if len(args[1:]) < 2 {
		fmt.Fprintf(os.Stderr, "Usage: %s <day> <title of puzzle>\n", args[0])
		os.Exit(1)
	}

	if err := generate(args[1], args[2]); err != nil {
		fmt.Fprintf(os.Stderr, "Error while generating: %v\n", err)
		os.Exit(1)
	}
}
