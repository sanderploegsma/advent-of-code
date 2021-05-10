using System.IO;
using Xunit;

namespace AdventOfCode2016.Day06
{
    public class Tests
    {
        private const string ExampleFile = @"Day06\\Example.txt";
        private const string InputFile = @"Day06\\Input.txt";
        
        [Theory]
        [InlineData(ExampleFile, "easter")]
        [InlineData(InputFile, "usccerug")]
        public void PartOne(string file, string expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleFile, "advent")]
        [InlineData(InputFile, "cnvvtafc")]
        public void PartTwo(string file, string expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}