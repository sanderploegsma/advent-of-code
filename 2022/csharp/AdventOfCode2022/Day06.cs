using System;

namespace AdventOfCode2022;

internal class Day06
{
    private readonly string _input;

    public Day06(string input)
    {
        _input = input;
    }

    public int PartOne() => FindMarker(4);

    public int PartTwo() => FindMarker(14);

    private int FindMarker(int markerSize)
    {
        for (var i = markerSize - 1; i < _input.Length; i++)
        {
            if (_input.Substring(i - (markerSize - 1), markerSize).ToHashSet().Count == markerSize)
            {
                return i + 1;
            }
        }

        return -1;
    }
}

public class Day06Test
{
    private const string InputFile = "Day06.Input.txt";

    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    [FileData(InputFile, 1282)]
    public void TestPartOne(string input, int expected)
    {
        var solution = new Day06(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    [FileData(InputFile, 3513)]
    public void TestPartTwo(string input, int expected)
    {
        var solution = new Day06(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}
