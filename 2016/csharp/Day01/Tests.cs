using System.IO;
using Xunit;

namespace AdventOfCode2016.Day01
{
    public class Tests
    {
        private const string InputFile = @"Day01\Input.txt";
        
        [Theory]
        [InlineData("R2, L3", 5)]
        [InlineData("R2, R2, R2", 2)]
        [InlineData("R5, L5, R5, R3", 12)]
        public void PartOne_Examples(string input, int expected)
        {
            var solution = new Solution(input);
            Assert.Equal(expected, solution.PartOne());
        }

        [Fact]
        public void PartOne()
        {
            const int answer = 241;
            var input = File.ReadAllText(InputFile);
            var solution = new Solution(input);
            Assert.Equal(answer, solution.PartOne());
        }

        [Theory]
        [InlineData("R8, R4, R4, R8", 4)]
        public void PartTwo_Examples(string input, int expected)
        {
            var solution = new Solution(input);
            Assert.Equal(expected, solution.PartTwo());
        }

        [Fact]
        public void PartTwo()
        {
            const int answer = 116;
            var input = File.ReadAllText(InputFile);
            var solution = new Solution(input);
            Assert.Equal(answer, solution.PartTwo());
        }
    }
}