namespace AdventOfCode2022;

internal class Day01
{
    private readonly List<List<int>> _data;

    public Day01(string input)
    {
        _data = input
            .Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList())
            .ToList();
    }

    public int PartOne()
    {
        return _data
            .Select(x => x.Sum())
            .Max();
    }

    public int PartTwo()
    {
        return _data
            .Select(x => x.Sum())
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
    }
}

public class Day01Test
{
    private const string InputFile = "Day01.Input.txt";
    private const string ExampleFile = "Day01.Example.txt";

    [Theory]
    [FileData(ExampleFile, 24000)]
    [FileData(InputFile, 69528)]
    public void TestPartOne(string input, int expected)
    {
        var solution = new Day01(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, 45000)]
    [FileData(InputFile, 206152)]
    public void TestPartTwo(string input, int expected)
    {
        var solution = new Day01(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
