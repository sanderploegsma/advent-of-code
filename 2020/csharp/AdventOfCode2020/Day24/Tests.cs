using System.IO;
using Xunit;

namespace AdventOfCode2020.Day24
{
    public class Tests
    {
        private const string InputFile = "Day24\\Input.txt";
        private const string ExampleFile = "Day24\\Example.txt";

        [Theory]
        [InlineData(ExampleFile, 10)]
        [InlineData(InputFile, 277)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [InlineData(ExampleFile, 2208)]
        [InlineData(InputFile, 3531)]
        public void TestPartTwo(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}
