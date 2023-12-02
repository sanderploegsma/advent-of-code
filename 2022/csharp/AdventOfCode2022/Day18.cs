namespace AdventOfCode2022;

internal class Day18
{
    private const char Lava = '#';
    private const char Steam = '.';

    private readonly record struct Point(int X, int Y, int Z)
    {
        public IEnumerable<Point> Neighbours
        {
            get
            {
                yield return this with { X = X + 1 };
                yield return this with { X = X - 1 };
                yield return this with { Y = Y + 1 };
                yield return this with { Y = Y - 1 };
                yield return this with { Z = Z + 1 };
                yield return this with { Z = Z - 1 };
            }
        }
    }

    private readonly ISet<Point> _cubes;
    private readonly Point _min;
    private readonly Point _max;

    public Day18(IEnumerable<string> input)
    {
        _cubes = input.Select(line =>
            {
                var coordinates = line.Split(",").Select(int.Parse).ToArray();
                return new Point(coordinates[0], coordinates[1], coordinates[2]);
            })
            .ToHashSet();

        _min = new Point(_cubes.Min(c => c.X) - 1, _cubes.Min(c => c.Y) - 1, _cubes.Min(c => c.Z) - 1);
        _max = new Point(_cubes.Max(c => c.X) + 1, _cubes.Max(c => c.Y) + 1, _cubes.Max(c => c.Z) + 1);
    }

    public int PartOne()
    {
        var freeSides =
            from cube in _cubes
            from side in cube.Neighbours
            where !_cubes.Contains(side)
            select side;

        return freeSides.Count();
    }

    public int PartTwo()
    {
        var grid = _cubes.ToDictionary(c => c, _ => Lava);

        for (var x = _min.X; x <= _max.X; x++)
        for (int y = _min.Y; y <= _max.Y; y++)
        for (int z = _min.Z; z <= _max.Z; z++)
        {
            grid[new Point(x, y, _min.Z)] = Steam;
            grid[new Point(x, y, _max.Z)] = Steam;
            grid[new Point(x, _min.Y, z)] = Steam;
            grid[new Point(x, _max.Y, z)] = Steam;
            grid[new Point(_min.X, y, z)] = Steam;
            grid[new Point(_max.X, y, z)] = Steam;
        }

        var hasChanged = true;

        while (hasChanged)
        {
            hasChanged = false;

            for (var x = _min.X; x <= _max.X; x++)
            for (int y = _min.Y; y <= _max.Y; y++)
            for (int z = _min.Z; z <= _max.Z; z++)
            {
                var point = new Point(x, y, z);
                if (grid.ContainsKey(point))
                {
                    continue;
                }

                if (point.Neighbours.Any(p => grid.TryGetValue(p, out var value) && value == Steam))
                {
                    grid[point] = Steam;
                    hasChanged = true;
                }
            }
        }

        var freeSides =
            from cube in _cubes
            from side in cube.Neighbours
            where grid.TryGetValue(side, out var value) && value == Steam
            select side;

        return freeSides.Count();
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

    [Theory]
    [FileData(Example1File, FileContents.StringPerLine, 10)]
    [FileData(Example2File, FileContents.StringPerLine, 58)]
    [FileData(InputFile, FileContents.StringPerLine, 2044)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day18(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
