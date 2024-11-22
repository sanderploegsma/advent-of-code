using System.IO;
using Xunit;

namespace AdventOfCode2016.Day10
{
    public class Tests
    {
        private const string InputFile = @"Day10\\Input.txt";

        [Fact]
        public void PartOne()
        {
            const int expected = 147;
            var solution = new Solution(File.ReadLines(InputFile));
            Assert.Equal(expected, solution.PartOne());
        }

        [Fact]
        public void PartTwo()
        {
            const int expected = 55637;
            var solution = new Solution(File.ReadLines(InputFile));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}
