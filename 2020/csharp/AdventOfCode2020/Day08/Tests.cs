using System.IO;
using Xunit;

namespace AdventOfCode2020.Day08
{
    public class Tests
    {
        private const string ExampleFile = "Day08\\Example.txt";
        private const string InputFile = "Day08\\Input.txt";

        [Theory]
        [InlineData(ExampleFile, 5)]
        [InlineData(InputFile, 1654)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [InlineData(ExampleFile, 8)]
        [InlineData(InputFile, 833)]
        public void TestPartTwo(string file, long expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}
