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

                if (Math.Abs(tail.X - head.X) < 2 && Math.Abs(tail.Y - head.Y) < 2)
                {
                    continue;
                }

                if (tail.X - head.X == 2)
                {
                    tail = new IntVector(head.X + 1, head.Y);
                }
                else if (tail.X - head.X == -2)
                {
                    tail = new IntVector(head.X - 1, head.Y);
                }
                else if (tail.Y - head.Y == 2)
                {
                    tail = new IntVector(head.X, head.Y + 1);
                }
                else if (tail.Y - head.Y == -2)
                {
                    tail = new IntVector(head.X, head.Y - 1);
                }

                tailPositions.Add(tail);
            }
        }

        return tailPositions.Count;
    }

    public int PartTwo()
    {
        return -1;
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
    private const string ExampleFile = "Day09.Example.txt";

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 13)]
    [FileData(InputFile, FileContents.StringPerLine, 6337)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day09(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, -1)]
    [FileData(InputFile, FileContents.StringPerLine, -1)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day09(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
