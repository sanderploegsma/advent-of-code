using System.IO;
using Xunit;

namespace AdventOfCode2020.Day09
{
    public class Tests
    {
        private const string ExampleFile = "Day09\\Example.txt";
        private const string InputFile = "Day09\\Input.txt";

        [Theory]
        [InlineData(ExampleFile, 5, 127L)]
        [InlineData(InputFile, 25, 26134589L)]
        public void TestPartOne(string file, int preambleSize, long expected)
        {
            var solution = new Solution(preambleSize, File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [InlineData(ExampleFile, 5, 62L)]
        [InlineData(InputFile, 25, 3535124L)]
        public void TestPartTwo(string file, int preambleSize, long expected)
        {
            var solution = new Solution(preambleSize, File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}
