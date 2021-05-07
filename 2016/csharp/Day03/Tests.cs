using System.IO;
using Xunit;

namespace AdventOfCode2016.Day03
{
    public class Tests
    {
        private const string InputFile = @"Day03\Input.txt";
        
        [Theory]
        [InlineData(InputFile, 1050)]
        public void PartOne(string input, int expected)
        {
            var solution = new Solution(File.ReadLines(input));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(InputFile, 1921)]
        public void PartTwo(string input, int expected)
        {
            var solution = new Solution(File.ReadLines(input));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}