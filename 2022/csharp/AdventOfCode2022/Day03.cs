namespace AdventOfCode2022;

internal class Day03
{
    private static readonly Dictionary<char, int> priorities = Enumerable.Range('a', 26).Select(x => (char)x)
        .Concat(Enumerable.Range('A', 26).Select(x => (char)x))
        .Zip(Enumerable.Range(1, 2 * 26))
        .ToDictionary(x => x.First, x => x.Second);

    private readonly List<string> values;

    public Day03(IEnumerable<string> input)
    {
        values = input.ToList();
    }

    public int PartOne()
    {
        return values
            .Select(x => x[..(x.Length / 2)].Intersect(x[(x.Length / 2)..]).First())
            .Sum(c => priorities[c]);
    }

    public int PartTwo()
    {
        return values
            .Chunk(3)
            .Select(xs => xs.Select(x => x.ToHashSet()).IntersectAll().First())
            .Sum(c => priorities[c]);
    }
}

public class Day03Test
{
    private const string InputFile = "Day03.Input.txt";
    private const string ExampleFile = "Day03.Example.txt";

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 157)]
    [FileData(InputFile, FileContents.StringPerLine, 8493)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day03(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 70)]
    [FileData(InputFile, FileContents.StringPerLine, 2552)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day03(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
