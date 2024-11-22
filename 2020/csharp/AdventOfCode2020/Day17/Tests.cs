using System.IO;
using Xunit;

namespace AdventOfCode2020.Day17
{
    public class Tests
    {
        private const string ExampleFile = "Day17\\ExampleOne.txt";
        private const string InputFile = "Day17\\Input.txt";

        [Theory]
        [InlineData(ExampleFile, 112)]
        [InlineData(InputFile, 293)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadAllLines(file));
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [InlineData(ExampleFile, 848)]
        [InlineData(InputFile, 1816)]
        public void TestPartTwo(string file, int expected)
        {
            var solution = new Solution(File.ReadAllLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}
