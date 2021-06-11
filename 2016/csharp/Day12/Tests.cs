using System.IO;
using Xunit;

namespace AdventOfCode2016.Day12
{
    public class Tests
    {
        private const string ExampleFile = @"Day12\\Example.txt";
        private const string InputFile = @"Day12\\Input.txt";

        [Theory]
        [InlineData(ExampleFile, 42)]
        [InlineData(InputFile, 318077)]
        public void PartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleFile, 42)]
        [InlineData(InputFile, 9227731)]
        public void PartTwo(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}