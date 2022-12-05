namespace AdventOfCode2022;

internal class Day04
{
    private readonly List<(List<int>, List<int>)> values;

    public Day04(IEnumerable<string> input)
    {
        values = input
            .Select(s => s.Split(',').Select(ParseRange).ToArray())
            .Select(x => (x[0], x[1]))
            .ToList();
    }

    public int PartOne()
    {
        return values.Count(x =>
        {
            var intersect = x.Item1.Intersect(x.Item2);
            return intersect.SequenceEqual(x.Item1) || intersect.SequenceEqual(x.Item2);
        });
    }

    public int PartTwo()
    {
        return values.Count(x => x.Item1.Any(y => x.Item2.Contains(y)) || x.Item2.Any(y => x.Item1.Contains(y)));
    }

    private static List<int> ParseRange(string range)
    {
        var start = int.Parse(range.Split('-')[0]);
        var end = int.Parse(range.Split('-')[1]);

        return Enumerable.Range(start, end - start + 1).ToList();
    }
}

public class Day04Test
{
    private const string InputFile = "Day04.Input.txt";
    private const string ExampleFile = "Day04.Example.txt";

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 2)]
    [FileData(InputFile, FileContents.StringPerLine, 542)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day04(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 4)]
    [FileData(InputFile, FileContents.StringPerLine, 900)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day04(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
