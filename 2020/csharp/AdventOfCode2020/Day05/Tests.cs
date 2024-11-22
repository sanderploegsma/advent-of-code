using System.IO;
using Xunit;

namespace AdventOfCode2020.Day05
{
    public class Tests
    {
        private const string InputFile = "Day05\\Input.txt";

        [Theory]
        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void TestPartOneExamples(string input, int expected)
        {
            var solution = new Solution(new[] {input});
            Assert.Equal(expected, solution.PartOne());
        }

        [Fact]
        public void TestPartOne()
        {
            var solution = new Solution(File.ReadLines(InputFile));
            Assert.Equal(813, solution.PartOne());
        }

        [Fact]
        public void TestPartTwo()
        {
            var solution = new Solution(File.ReadLines(InputFile));
            Assert.Equal(612, solution.PartTwo());
        }
    }
}
