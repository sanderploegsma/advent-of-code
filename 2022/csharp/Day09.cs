namespace AdventOfCode2022;

internal class Day09
{
    private readonly List<(Direction, int)> _moves;

    public Day09(IEnumerable<string> input)
    {
        _moves = input.Select(line =>
        {
            var direction = ToDirection(line[0]);
            var distance = int.Parse(line[2..]);
            return (direction, distance);
        }).ToList();
    }

    public int PartOne()
    {
        var head = new IntVector(0, 0);
        var tail = new IntVector(0, 0);
        var tailPositions = new HashSet<IntVector>() { tail };

        foreach (var (direction, distance) in _moves)
        {
            for (var i = 0; i < distance; i++)
            {
                head = head.Move(direction);
                tail = MoveTail(head, tail);
                tailPositions.Add(tail);
            }
        }

        return tailPositions.Count;
    }

    public int PartTwo()
    {
        var knots = Enumerable.Repeat(new IntVector(0, 0), 10).ToArray();
        var tailPositions = new HashSet<IntVector>() { knots.Last() };

        foreach (var (direction, distance) in _moves)
        {
            for (var i = 0; i < distance; i++)
            {
                knots[0] = knots[0].Move(direction);

                for (var j = 1; j < knots.Length; j++)
                {
                    knots[j] = MoveTail(knots[j - 1], knots[j]);
                }

                tailPositions.Add(knots.Last());
            }
        }

        return tailPositions.Count;
    }

    private static IntVector MoveTail(IntVector head, IntVector tail)
    {
        if (Math.Abs(tail.X - head.X) < 2 && Math.Abs(tail.Y - head.Y) < 2)
        {
            return tail;
        }

        var (dx, dy) = (0, 0);

        if (tail.X == head.X)
        {
            dx = 0;
            dy = (int)((head.Y - tail.Y) / Math.Abs(head.Y - tail.Y));
        }
        else if (tail.Y == head.Y)
        {
            dx = (int)((head.X - tail.X) / Math.Abs(head.X - tail.X));
            dy = 0;
        }
        else
        {
            dx = (int)((head.X - tail.X) / Math.Abs(head.X - tail.X));
            dy = (int)((head.Y - tail.Y) / Math.Abs(head.Y - tail.Y));
        }

        return new IntVector(tail.X + dx, tail.Y + dy);
    }

    private static Direction ToDirection(char c) => c switch
    {
        'R' => Direction.East,
        'U' => Direction.North,
        'L' => Direction.West,
        'D' => Direction.South,
        _ => throw new ArgumentOutOfRangeException()
    };
}

public class Day09Test
{
    private const string InputFile = "Day09.Input.txt";
    private const string FirstExampleFile = "Day09.Example1.txt";
    private const string SecondExampleFile = "Day09.Example2.txt";

    [Theory]
    [FileData(FirstExampleFile, FileContents.StringPerLine, 13)]
    [FileData(InputFile, FileContents.StringPerLine, 6337)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day09(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(FirstExampleFile, FileContents.StringPerLine, 1)]
    [FileData(SecondExampleFile, FileContents.StringPerLine, 36)]
    [FileData(InputFile, FileContents.StringPerLine, 2455)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day09(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
