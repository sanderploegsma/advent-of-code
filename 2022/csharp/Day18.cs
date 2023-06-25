namespace AdventOfCode2022;

internal class Day18
{
    private readonly record struct Cube(int X, int Y, int Z)
    {
        public IEnumerable<Cube> Neighbours
        {
            get
            {
                yield return this with { X = X - 1 };
                yield return this with { X = X + 1 };
                yield return this with { Y = Y - 1 };
                yield return this with { Y = Y + 1 };
                yield return this with { Z = Z - 1 };
                yield return this with { Z = Z + 1 };
            }
        }
    }

    private readonly ISet<Cube> _cubes;

    public Day18(IEnumerable<string> input)
    {
        _cubes = input.Select(line =>
            {
                var coordinates = line.Split(",").Select(int.Parse).ToArray();
                return new Cube(coordinates[0], coordinates[1], coordinates[2]);
            })
            .ToHashSet();
    }

    public int PartOne()
    {
        var freeSides = from cube in _cubes
            from side in cube.Neighbours
            where !_cubes.Contains(side)
            select side;

        return freeSides.Count();
    }

    public int PartTwo()
    {
        return -1;
    }
}

public class Day18Test
{
    private const string InputFile = "Day18.Input.txt";
    private const string Example1File = "Day18.Example1.txt";
    private const string Example2File = "Day18.Example2.txt";

    [Theory]
    [FileData(Example1File, FileContents.StringPerLine, 10)]
    [FileData(Example2File, FileContents.StringPerLine, 64)]
    [FileData(InputFile, FileContents.StringPerLine, 3396)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day18(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory(Skip = "Not yet implemented")]
    [FileData(Example1File, FileContents.StringPerLine, 10)]
    [FileData(Example2File, FileContents.StringPerLine, 58)]
    [FileData(InputFile, FileContents.StringPerLine, -1)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day18(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
