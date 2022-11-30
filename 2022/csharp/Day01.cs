namespace AdventOfCode2022;

internal class Day01
{
    private readonly string _input;

    public Day01(string input)
    {
        _input = input;
    }

    public int PartOne() { return _input.Length; }
}

public class Day01Test
{
    private const string InputFile = "Day01.Input.txt";

    [Theory]
    [InlineData("OneTwoThree", 11)]
    [FileData(InputFile, 21)]
    public void TestPartOne(string input, int expected)
    {
        var solution = new Day01(input);
        Assert.Equal(expected, solution.PartOne());
    }
}
