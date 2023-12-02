using System.Text.RegularExpressions;

namespace AdventOfCode2022;

internal class Day11
{
    public class Monkey
    {
        public int Id { get; init; }
        public List<long> StartingItems { get; init; }
        public Func<long, long> Operation { get; init; }
        public int TestDivisor { get; init; }
        public int TargetMonkeyIfTrue { get; init; }
        public int TargetMonkeyIfFalse { get; init; }
    }

    private readonly Monkey[] _monkeys;

    public Day11(string input)
    {
        _monkeys = input
            .Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(ParseMonkey)
            .ToArray();
    }

    public long PartOne()
    {
        return Run(20, x => x / 3);
    }

    public long PartTwo()
    {
        var gcd = _monkeys.Select(m => m.TestDivisor).Product();
        return Run(10000, x => x % gcd);
    }

    private long Run(int rounds, Func<long, long> destressor)
    {
        var inventories = _monkeys.Select(m => new Queue<long>(m.StartingItems)).ToArray();
        var inspectionCounts = new long[_monkeys.Length];

        for (var round = 0; round < rounds; round++)
        {
            for (var monkeyId = 0; monkeyId < _monkeys.Length; monkeyId++)
            {
                var monkey = _monkeys[monkeyId];
                while (inventories[monkeyId].TryDequeue(out var current))
                {
                    inspectionCounts[monkeyId]++;
                    var next = destressor.Invoke(monkey.Operation.Invoke(current));
                    var targetId = next % monkey.TestDivisor == 0 ? monkey.TargetMonkeyIfTrue : monkey.TargetMonkeyIfFalse;
                    inventories[targetId].Enqueue(next);
                }
            }
        }

        return inspectionCounts.OrderDescending().Take(2).Product();
    }

    private static Monkey ParseMonkey(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var operation = lines[2].Split(": new = ")[1].Split(' ');

        return new Monkey
        {
            Id = int.Parse(lines[0].Split(' ').Last().TrimEnd(':')),
            StartingItems = lines[1].Split(": ").Last().Split(", ").Select(long.Parse).ToList(),
            Operation = x =>
            {
                var lhs = operation[0] == "old" ? x : long.Parse(operation[0]);
                var rhs = operation[2] == "old" ? x : long.Parse(operation[2]);
                return operation[1] switch
                {
                    "+" => lhs + rhs,
                    "-" => lhs - rhs,
                    "*" => lhs * rhs,
                    _ => throw new ArgumentException()
                };
            },
            TestDivisor = int.Parse(lines[3].Split(' ').Last()),
            TargetMonkeyIfTrue = int.Parse(lines[4].Split(' ').Last()),
            TargetMonkeyIfFalse = int.Parse(lines[5].Split(' ').Last()),
        };
    }
}

public class Day11Test
{
    private const string InputFile = "Day11.Input.txt";
    private const string ExampleFile = "Day11.Example.txt";

    [Theory]
    [FileData(ExampleFile, 10605)]
    [FileData(InputFile, 316888)]
    public void TestPartOne(string input, long expected)
    {
        var solution = new Day11(input);
        Assert.Equal(expected, solution.PartOne());
    }

    [Theory]
    [FileData(ExampleFile, 2713310158)]
    [FileData(InputFile, 35270398814)]
    public void TestPartTwo(string input, long expected)
    {
        var solution = new Day11(input);
        Assert.Equal(expected, solution.PartTwo());
    }
}