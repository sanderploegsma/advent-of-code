using Xunit.Abstractions;

namespace AdventOfCode2022;

internal class Day10
{
    private readonly List<Func<int, int>> _operations;

    public Day10(IEnumerable<string> input)
    {
        _operations = new List<Func<int, int>>();

        foreach (var line in input)
        {
            if (line == "noop")
            {
                _operations.Add(x => x);
            }
            else
            {
                var n = int.Parse(line.Split(' ')[1]);
                _operations.Add(x => x);
                _operations.Add(x => x + n);
            }
        }
    }

    public int PartOne()
    {
        var register = 1;
        var values = new List<int>();

        foreach (var op in _operations)
        {
            values.Add(register);
            register = op.Invoke(register);
        }

        return new int[] { 20, 60, 100, 140, 180, 220 }
            .Sum(c => c * values[c - 1]);
    }

    public string PartTwo()
    {
        var register = 1;
        var output = "";

        for (var i = 0; i < _operations.Count; i++)
        {
            var pixel = i % 40;

            if (pixel == 0)
            {
                output += "\n";
            }

            if (pixel == register || pixel == register - 1 || pixel == register + 1)
            {
                output += '#';
            }
            else
            {
                output += '.';
            }

            register = _operations[i].Invoke(register);
        }

        return output;
    }
}

public class Day10Test
{
    private const string InputFile = "Day10.Input.txt";
    private const string ExampleFile = "Day10.Example.txt";

    private readonly ITestOutputHelper _output;

    public Day10Test(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 13140)]
    [FileData(InputFile, FileContents.StringPerLine, -1)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day10(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine)]
    [FileData(InputFile, FileContents.StringPerLine)]
    public void TestPartTwo(IEnumerable<string> input)
    {
        var solution = new Day10(input);
        _output.WriteLine(solution.PartTwo());
    }
}