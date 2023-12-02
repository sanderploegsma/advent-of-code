using System.Text.RegularExpressions;

namespace AdventOfCode2022;

internal partial class Day05
{
    private readonly List<List<char>> stacks;
    private readonly List<(int Quantity, int From, int To)> instructions;

    public Day05(string input)
    {
        stacks = new List<List<char>>();
        instructions = new List<(int Quantity, int From, int To)>();

        var stackData = input.Split(Environment.NewLine + Environment.NewLine)[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var instructionData = input.Split(Environment.NewLine + Environment.NewLine)[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var numberOfCrates = stackData.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        for (var i = 0; i < numberOfCrates; i++)
        {
            stacks.Add(new List<char>());
        }

        foreach (var level in stackData.SkipLast(1).Reverse())
        {
            for (var i = 0; i < numberOfCrates; i++)
            {
                var crate = level[4 * i + 1];
                if (crate != ' ')
                {
                    stacks[i].Add(crate);
                }
            }
        }

        foreach (var instruction in instructionData)
        {
            var match = InstructionPattern().Match(instruction);
            instructions.Add((int.Parse(match.Groups["quantity"].Value), int.Parse(match.Groups["from"].Value), int.Parse(match.Groups["to"].Value)));
        }
    }

    public string PartOne()
    {
        var state = stacks.Select(x => new Stack<char>(x)).ToList();

        foreach (var (quantity, from, to) in instructions)
        {
            for (var i = 0; i < quantity; i++)
            {
                var crate = state[from - 1].Pop();
                state[to - 1].Push(crate);
            }
        }

        return string.Concat(state.Select(x => x.Peek()));
    }

    public string PartTwo()
    {
        var state = stacks.Select(x => new Stack<char>(x)).ToList();

        foreach (var (quantity, from, to) in instructions)
        {
            var crates = new List<char>();

            for (var i = 0; i < quantity; i++)
            {
                var crate = state[from - 1].Pop();
                crates.Add(crate);
            }

            foreach (var crate in crates.Reverse<char>())
            {
                state[to - 1].Push(crate);
            }
        }

        return string.Concat(state.Select(x => x.Peek()));
    }

    [GeneratedRegex("move (?<quantity>\\d+) from (?<from>\\d+) to (?<to>\\d+)")]
    private static partial Regex InstructionPattern();
}

public class Day05Test
{
    private const string InputFile = "Day05.Input.txt";
    private const string ExampleFile = "Day05.Example.txt";

    [Theory]
    [FileData(ExampleFile, "CMZ")]
    [FileData(InputFile, "FWSHSPJWM")]
    public void TestPartOne(string input, string expected)
    {
        var solution = new Day05(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, "MCD")]
    [FileData(InputFile, "PWPWHGFZS")]
    public void TestPartTwo(string input, string expected)
    {
        var solution = new Day05(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
