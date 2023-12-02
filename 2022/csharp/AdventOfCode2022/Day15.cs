using System.Text.RegularExpressions;

namespace AdventOfCode2022;

internal partial class Day15
{
    private record struct Sensor(IntVector Position, IntVector ClosestBeaconPosition)
    {
        public int Radius => Position.ManhattanDistanceTo(ClosestBeaconPosition);
    }

    private readonly List<Sensor> _sensors;

    public Day15(IEnumerable<string> input)
    {
        _sensors = input.Select(line =>
        {
            var match = InputPattern().Match(line);
            var sensorX = int.Parse(match.Groups["SensorX"].Value);
            var sensorY = int.Parse(match.Groups["SensorY"].Value);
            var beaconX = int.Parse(match.Groups["BeaconX"].Value);
            var beaconY = int.Parse(match.Groups["BeaconY"].Value);
            return new Sensor(new IntVector(sensorX, sensorY), new IntVector(beaconX, beaconY));
        }).ToList();
    }

    public int PartOne(int y)
    {
        var minX = _sensors.Min(s => s.Position.X - s.Radius);
        var maxX = _sensors.Max(s => s.Position.X + s.Radius);
        var beacons = _sensors.Select(s => s.ClosestBeaconPosition);

        var coverage = from x in Enumerable.Range(minX, maxX - minX)
                       let position = new IntVector(x, y)
                       where _sensors.Any(s => s.Position.ManhattanDistanceTo(position) <= s.Radius)
                       where !beacons.Contains(position)
                       select position;

        return coverage.Count();
    }

    public long PartTwo(int range)
    {
        var results = from sensor in _sensors
                      from position in GetPositionsAroundEdge(sensor)
                      where position.X >= 0 && position.X <= range
                      where position.Y >= 0 && position.Y <= range
                      where _sensors.All(s => s.Position.ManhattanDistanceTo(position) > s.Radius)
                      select position.X * 4000000L + position.Y;

        return results.First();

        static IEnumerable<IntVector> GetPositionsAroundEdge(Sensor sensor)
        {
            var distance = sensor.Radius + 1;

            for (var d = 0; d <= distance; d++)
            {
                yield return new IntVector(sensor.Position.X + d, sensor.Position.Y - (distance - d));
                yield return new IntVector(sensor.Position.X - d, sensor.Position.Y - (distance - d));
                yield return new IntVector(sensor.Position.X + d, sensor.Position.Y + (distance - d));
                yield return new IntVector(sensor.Position.X - d, sensor.Position.Y + (distance - d));
            }
        }
    }

    [GeneratedRegex(@"^Sensor at x=(?<SensorX>[\d\-]+), y=(?<SensorY>[\d\-]+): closest beacon is at x=(?<BeaconX>[\d\-]+), y=(?<BeaconY>[\d\-]+)$")]
    private static partial Regex InputPattern();
}

public class Day15Test
{
    private const string InputFile = "Day15.Input.txt";
    private const string ExampleFile = "Day15.Example.txt";

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 10, 26)]
    [FileData(InputFile, FileContents.StringPerLine, 2000000, 5166077)]
    public void TestPartOne(IEnumerable<string> input, int y, int expected)
    {
        var solution = new Day15(input);
        Assert.Equal(expected, solution.PartOne(y));
    }

    [Theory]
    [FileData(ExampleFile, FileContents.StringPerLine, 20, 56000011)]
    [FileData(InputFile, FileContents.StringPerLine, 4000000, 13071206703981)]
    public void TestPartTwo(IEnumerable<string> input, int range, long expected)
    {
        var solution = new Day15(input);
        Assert.Equal(expected, solution.PartTwo(range));
    }
}
