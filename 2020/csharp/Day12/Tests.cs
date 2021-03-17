using System.IO;
using Xunit;

namespace AdventOfCode2020.Day12
{
    public class Tests
    {
        private const string ExampleFile = "Day12\\Example.txt";
        private const string InputFile = "Day12\\Input.txt";
        
        [Theory]
        [InlineData(ExampleFile, 25)]
        [InlineData(InputFile, 562)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleFile, 286)]
        [InlineData(InputFile, 101860)]
        public void TestPartTwo(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}