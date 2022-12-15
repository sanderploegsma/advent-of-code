using Xunit.Abstractions;

namespace AdventOfCode2022;

internal class Day14
{
    private const char Air = '.';
    private const char Rock = '#';
    private const char Sand = 'o';
    private const char Source = '+';

    private record struct LineSegment(IntVector Start, IntVector End)
    {
        public bool Contains(IntVector point)
        {
            if (Start.X == End.X && Start.X == point.X)
            {
                var minY = Math.Min(Start.Y, End.Y);
                var maxY = Math.Max(Start.Y, End.Y);
                return point.Y >= minY && point.Y <= maxY;
            }

            if (Start.Y == End.Y && Start.Y == point.Y)
            {
                var minX = Math.Min(Start.X, End.X);
                var maxX = Math.Max(Start.X, End.X);
                return point.X >= minX && point.X <= maxX;
            }

            return false;
        }
    }

    private readonly List<LineSegment> _rocks;
    private readonly ITestOutputHelper _output;

    public Day14(IEnumerable<string> input, ITestOutputHelper output)
    {
        _rocks = input
            .SelectMany(line => line
                .Split(" -> ")
                .Select(c => new IntVector(int.Parse(c.Split(',')[0]), int.Parse(c.Split(',')[1])))
                .Pairwise()
                .Select(pair => new LineSegment(pair.Item1, pair.Item2)))
            .ToList();

        _output = output;
    }

    public int PartOne()
    {
        var cave = CreateCave();
        return Simulate(cave);
    }

    public int PartTwo()
    {
        var cave = CreateCave(floor: true);
        return Simulate(cave);
    }

    private int Simulate(char[,] cave)
    {
        DrawCave(cave);

        while (TryAddSand(cave, out var x, out var y))
        {
            cave[x, y] = Sand;
        }

        DrawCave(cave);

        return cave.Enumerate().Count(x => x.Data == Sand);
    }

    private static bool TryAddSand(char[,] cave, out int x, out int y)
    {
        y = 0;
        x = Array.IndexOf(cave.GetRow(0).ToArray(), Source);

        while (y < cave.Height() - 1 && x > 0 && x < cave.Width() - 1)
        {
            if (cave[x, y + 1] == Air)
            {
                y++;
                continue;
            }

            if (cave[x - 1, y + 1] == Air)
            {
                y++;
                x--;
                continue;
            }

            if (cave[x + 1, y + 1] == Air)
            {
                y++;
                x++;
                continue;
            }

            return true;
        }

        return false;
    }

    private void DrawCave(char[,] cave)
    {
        _output.WriteLine("");

        for (var y = 0; y < cave.Height(); y++)
        {
            var line = "";
            for (var x = 0; x < cave.Width(); x++)
            {
                line += cave[x, y];
            }
            _output.WriteLine(line);
        }
    }

    private char[,] CreateCave(bool floor = false)
    {
        var points = _rocks.SelectMany(r => new[] { r.Start, r.End }).ToList();
        var height = points.Max(p => p.Y) + 1;

        if (floor)
        {
            height += 2;
        }

        var width = 2 * height + 1;
        var cave = new char[width, height];

        var minX = points.Min(p => p.X);
        var maxX = points.Max(p => p.X);
        var padding = (width - (maxX - minX + 1)) / 2;
        var xOffset = minX - padding;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                cave[x, y] = Air;

                if (_rocks.Any(r => r.Contains(new IntVector(x + xOffset, y))))
                {
                    cave[x, y] = Rock;
                }
            }
        }

        cave[500 - xOffset, 0] = Source;

        if (floor)
        {
            for (int x = 0; x < width; x++)
            {
                cave[x, height - 1] = Rock;
            }
        }

        return cave;
    }
}

public class Day14Test
{
    private const string InputFile = "Day14.Input.txt";
    private const string ExampleFile = "Day14.Example.txt";

    private readonly ITestOutputHelper _output;

    public Day14Test(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 24)]
    [FileData(InputFile, FileContents.StringPerLine, 913)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day14(input, _output);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 93)]
    [FileData(InputFile, FileContents.StringPerLine, 30762)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day14(input, _output);
        Assert.Equal(expected, solution.PartTwo());
    }
}