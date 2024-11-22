using System.IO;
using Xunit;

namespace AdventOfCode2020.Day11
{
    public class Tests
    {
        private const string ExampleFile = "Day11\\Example.txt";
        private const string InputFile = "Day11\\Input.txt";

        [Theory]
        [InlineData(ExampleFile, 37)]
        [InlineData(InputFile, 2281)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [InlineData(ExampleFile, 26)]
        [InlineData(InputFile, 2085)]
        public void TestPartTwo(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}
