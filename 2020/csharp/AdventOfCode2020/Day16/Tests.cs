using System.IO;
using Xunit;

namespace AdventOfCode2020.Day16
{
    public class Tests
    {
        private const string ExampleFile = "Day16\\ExampleOne.txt";
        private const string InputFile = "Day16\\Input.txt";

        [Theory]
        [InlineData(ExampleFile, 71)]
        [InlineData(InputFile, 27898)]
        public void TestPartOne(string file, long expected)
        {
            var solution = new Solution(File.ReadAllLines(file));
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [InlineData(InputFile, 2766491048287)]
        public void TestPartTwo(string file, long expected)
        {
            var solution = new Solution(File.ReadAllLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}
