using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2016.Day08
{
    public class Tests
    {
        private const string InputFile = @"Day08\\Input.txt";

        private readonly ITestOutputHelper _output;

        public Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void PartOne()
        {
            const int expected = 128;
            var solution = new Solution(File.ReadLines(InputFile));
            Assert.Equal(expected, solution.PartOne());
        }

        [Fact]
        public void PartTwo()
        {
            var solution = new Solution(File.ReadLines(InputFile));
            _output.WriteLine(solution.PartTwo());
        }
    }
}