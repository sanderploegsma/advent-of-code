using Xunit;

namespace AdventOfCode2016.Day05
{
    public class Tests
    {
        private const string Input = "cxdnnyjw";
        
        [Theory]
        [InlineData("abc", "18f47a30")]
        [InlineData(Input, "f77a0e6e")]
        public void PartOne(string input, string expected)
        {
            var solution = new Solution(input);
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData("abc", "05ace8e3")]
        [InlineData(Input, "999828ec")]
        public void PartTwo(string input, string expected)
        {
            var solution = new Solution(input);
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}