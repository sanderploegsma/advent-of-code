namespace AdventOfCode2022;

internal class Day13
{
    private readonly List<string> _input;

    public Day13(IEnumerable<string> input)
    {
        _input = input
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Replace("10", "A"))
            .ToList();
    }

    public int PartOne()
    {
        return _input
            .Chunk(2)
            .Indexed(start: 1)
            .Where(x => IsCorrectlyOrdered(x.Value[0], x.Value[1]))
            .Sum(x => x.Index);
    }

    public int PartTwo()
    {
        var dividers = new[] { "[[2]]", "[[6]]" };
        var comparer = Comparer<string>.Create((left, right) => IsCorrectlyOrdered(left, right) ? -1 : 1);

        return _input
            .Concat(dividers)
            .Order(comparer)
            .Indexed(start: 1)
            .Where(x => dividers.Contains(x.Value))
            .Product(x => x.Index);
    }

    private bool IsCorrectlyOrdered(string left, string right)
    {
        var (lhead, ltail) = (left[0], left[1..]);
        var (rhead, rtail) = (right[0], right[1..]);

        return (lhead, rhead) switch
        {
            (char l, char r) when l == r => IsCorrectlyOrdered(ltail, rtail),
            (']', _) => true,
            (_, ']') => false,
            ('[', char r) => IsCorrectlyOrdered(ltail, $"{r}]{rtail}"),
            (char l, '[') => IsCorrectlyOrdered($"{l}]{ltail}", rtail),
            (char l, char r) => l < r,
        };
    }
}

public class Day13Test
{
    private const string InputFile = "Day13.Input.txt";
    private const string ExampleFile = "Day13.Example.txt";

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 13)]
    [FileData(InputFile, FileContents.StringPerLine, 6070)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day13(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 140)]
    [FileData(InputFile, FileContents.StringPerLine, 20758)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day13(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
