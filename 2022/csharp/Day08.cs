namespace AdventOfCode2022;

internal class Day08
{
    private readonly int[,] _input;

    public Day08(IEnumerable<string> input)
    {
        var data = input.Select(line => line.Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();
        _input = Matrix.Create(data);
    }

    public int PartOne()
    {
        var visible = 0;

        for (var x = 0; x < _input.Width(); x++)
        {
            for (var y = 0; y < _input.Height(); y++)
            {
                if (IsVisible(x, y))
                {
                    visible++;
                }
            }
        }

        return visible;
    }

    public int PartTwo()
    {
        var maxScore = 0;

        for (var x = 0; x < _input.Width(); x++)
        {
            for (var y = 0; y < _input.Height(); y++)
            {
                var score = GetScenicScore(x, y);
                if (score > maxScore)
                {
                    maxScore = score;
                }
            }
        }

        return maxScore;
    }

    private bool IsVisible(int x, int y)
    {
        if (y == 0 || y == _input.Height() - 1) return true;
        if (x == 0 || x == _input.Width() - 1) return true;

        var height = _input[x, y];

        var row = _input.GetRow(y);
        if (row.Take(x).All(h => h < height)) return true;
        if (row.Skip(x + 1).All(h => h < height)) return true;

        var col = _input.GetColumn(x);
        if (col.Take(y).All(h => h < height)) return true;
        if (col.Skip(y + 1).All(h => h < height)) return true;

        return false;
    }

    private int GetScenicScore(int x, int y)
    {
        var height = _input[x, y];
        var row = _input.GetRow(y);
        var col = _input.GetColumn(x);

        var left = row.Take(x).Reverse().TakeUntilIncluding(h => h >= height);
        var right = row.Skip(x + 1).TakeUntilIncluding(h => h >= height);
        var up = col.Take(y).Reverse().TakeUntilIncluding(h => h >= height);
        var down = col.Skip(y + 1).TakeUntilIncluding(h => h >= height);

        return left.Count() * right.Count() * up.Count() * down.Count();
    }
}

public class Day08Test
{
    private const string InputFile = "Day08.Input.txt";
    private const string ExampleFile = "Day08.Example.txt";

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 21)]
    [FileData(InputFile, FileContents.StringPerLine, 1733)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day08(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 8)]
    [FileData(InputFile, FileContents.StringPerLine, 284648)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day08(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
