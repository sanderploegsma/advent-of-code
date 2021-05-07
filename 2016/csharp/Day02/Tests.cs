using System.IO;
using Xunit;

namespace AdventOfCode2016.Day02
{
    public class Tests
    {
        private const string ExampleFile = @"Day02\Example.txt";
        private const string InputFile = @"Day02\Input.txt";
        
        [Theory]
        [InlineData(ExampleFile, "1985")]
        [InlineData(InputFile, "65556")]
        public void PartOne(string input, string expected)
        {
            var solution = new Solution(File.ReadLines(input));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleFile, "5DB3")]
        [InlineData(InputFile, "CB779")]
        public void PartTwo(string input, string expected)
        {
            var solution = new Solution(File.ReadLines(input));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}