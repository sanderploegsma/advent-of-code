using Xunit;

namespace AdventOfCode2020.Day23
{
    public class Tests
    {
        private const string Input = "586439172";
        private const string Example = "389125467";

        [Theory]
        [InlineData(Example, "67384529")]
        [InlineData(Input, "28946753")]
        public void TestPartOne(string input, string expected)
        {
            var solution = new Solution(input);
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [InlineData(Example, 149245887792L)]
        [InlineData(Input, 519044017360)]
        public void TestPartTwo(string input, long expected)
        {
            var solution = new Solution(input);
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}