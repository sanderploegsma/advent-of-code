namespace AdventOfCode2022;

internal class Day12
{
    private const char StartMarker = 'S';
    private const char EndMarker = 'E';
    private const char MinHeight = 'a';
    private const char MaxHeight = 'z';

    private readonly Dictionary<Point, char> _map;
    private readonly Dictionary<Point, int> _distances;
    private readonly Point _start;
    private readonly Point _end;

    public Day12(IEnumerable<string> input)
    {
        var heights = from line in input.Indexed()
                      from c in line.Value.Indexed()
                      select new { Point = new Point(c.Index, line.Index), Height = c.Value };

        _map = heights.ToDictionary(x => x.Point, x => x.Height);
        _start = heights.First(x => x.Height == StartMarker).Point;
        _end = heights.First(x => x.Height == EndMarker).Point;
        _distances = new Dictionary<Point, int> { [_end] = 0 };

        var queue = new Queue<Point>();
        queue.Enqueue(_end);

        while (queue.TryDequeue(out var point))
        {
            var neighbours = from neighbour in GetNeighbours(point)
                             where _map.ContainsKey(neighbour)
                             where !_distances.ContainsKey(neighbour)
                             where GetHeight(_map[point]) - GetHeight(_map[neighbour]) <= 1
                             select neighbour;

            foreach (var neighbour in neighbours)
            {
                _distances[neighbour] = _distances[point] + 1;
                queue.Enqueue(neighbour);
            }
        }
    }

    private static int GetHeight(char c) => c switch
    {
        StartMarker => 0,
        EndMarker => MaxHeight - MinHeight,
        _ => c - MinHeight,
    };

    public int PartOne()
    {
        return _distances[_start];
    }

    public int PartTwo()
    {
        return _distances.Where(x => _map[x.Key] == MinHeight).Min(x => x.Value);
    }

    private record struct Point(int X, int Y);

    private static IEnumerable<Point> GetNeighbours(Point point)
    {
        yield return new Point(point.X + 1, point.Y);
        yield return new Point(point.X - 1, point.Y);
        yield return new Point(point.X, point.Y + 1);
        yield return new Point(point.X, point.Y - 1);
    }
}

public class Day12Test
{
    private const string InputFile = "Day12.Input.txt";
    private const string ExampleFile = "Day12.Example.txt";

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 31)]
    [FileData(InputFile, FileContents.StringPerLine, 330)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day12(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 29)]
    [FileData(InputFile, FileContents.StringPerLine, 321)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day12(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
