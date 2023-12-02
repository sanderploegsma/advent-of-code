using System.IO;
using System.Runtime.InteropServices;
using Xunit;

namespace AdventOfCode2020.Day22
{
    public class Tests
    {
        private const string InputFile = "Day22\\Input.txt";
        private const string ExampleFile = "Day22\\Example.txt";

        [Theory]
        [InlineData(ExampleFile, 306)]
        [InlineData(InputFile, 31781)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadAllText(file));
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [InlineData(ExampleFile, 291)]
        [InlineData(InputFile, 35154)]
        public void TestPartTwo(string file, int expected)
        {
            var solution = new Solution(File.ReadAllText(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}