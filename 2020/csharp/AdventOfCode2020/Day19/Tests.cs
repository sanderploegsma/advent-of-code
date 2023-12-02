using System.IO;
using System.Runtime.InteropServices;
using Xunit;

namespace AdventOfCode2020.Day19
{
    public class Tests
    {
        private const string InputFile = "Day19\\Input.txt";
        private const string ExampleOneFile = "Day19\\ExampleOne.txt";
        private const string ExampleTwoFile = "Day19\\ExampleTwo.txt";

        [Theory]
        [InlineData(ExampleOneFile, 2)]
        [InlineData(InputFile, 241)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [InlineData(ExampleTwoFile, 12)]
        [InlineData(InputFile, 424)]
        public void TestPartTwo(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}