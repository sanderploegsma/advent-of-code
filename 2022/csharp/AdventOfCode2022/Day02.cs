using System;

namespace AdventOfCode2022;

internal class Day02
{
    public enum Shape
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3,
    }

    public enum Result
    {
        Loss = 0,
        Draw = 3,
        Win = 6,
    }

    private readonly List<(char, char)> _rounds;

    public Day02(IEnumerable<string> input)
    {
        _rounds = input.Select(x => (x[0], x[2])).ToList();
    }

    public int PartOne()
    {
        var scores = from round in _rounds
                     let theirs = ParseShape(round.Item1)
                     let mine = ParseShape(round.Item2)
                     let result = GetResult(theirs, mine)
                     select GetScore(mine, result);

        return scores.Sum();
    }

    public int PartTwo()
    {
        var scores = from round in _rounds
                     let theirs = ParseShape(round.Item1)
                     let result = ParseResult(round.Item2)
                     from mine in Enum.GetValues<Shape>()
                     where GetResult(theirs, mine) == result
                     select GetScore(mine, result);

        return scores.Sum();
    }

    private static Shape ParseShape(char c) => c switch
    {
        'A' or 'X' => Shape.Rock,
        'B' or 'Y' => Shape.Paper,
        'C' or 'Z' => Shape.Scissors,
        _ => throw new ArgumentException(),
    };

    private static Result ParseResult(char c) => c switch
    {
        'X' => Result.Loss,
        'Y' => Result.Draw,
        'Z' => Result.Win,
        _ => throw new ArgumentException(),
    };

    static Result GetResult(Shape theirs, Shape mine)
    {
        if (theirs == mine)
        {
            return Result.Draw;
        }

        if (theirs == Shape.Rock && mine == Shape.Paper ||
            theirs == Shape.Paper && mine == Shape.Scissors ||
            theirs == Shape.Scissors && mine == Shape.Rock)
        {
            return Result.Win;
        }

        return Result.Loss;
    }

    static int GetScore(Shape shape, Result result) => ((int)shape) + ((int)result);
}

public class Day02Test
{
    private const string InputFile = "Day02.Input.txt";
    private const string ExampleFile = "Day02.Example.txt";

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 15)]
    [FileData(InputFile, FileContents.StringPerLine, 10718)]
    public void TestPartOne(IEnumerable<string> input, int expected)
    {
        var solution = new Day02(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 12)]
    [FileData(InputFile, FileContents.StringPerLine, 14652)]
    public void TestPartTwo(IEnumerable<string> input, int expected)
    {
        var solution = new Day02(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
