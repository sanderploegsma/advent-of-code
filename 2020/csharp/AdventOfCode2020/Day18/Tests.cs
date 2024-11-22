using System.IO;
using System.Runtime.InteropServices;
using Xunit;

namespace AdventOfCode2020.Day18
{
    public class Tests
    {
        private const string InputFile = "Day18\\Input.txt";
        private const long AnswerPartOne = 67800526776934;
        private const long AnswerPartTwo = 340789638435483;

        private static readonly string[] Input = File.ReadAllLines(InputFile);

        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 71)]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [InlineData("2 * 3 + (4 * 5)", 26)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
        public void TestPartOneExamples(string expression, long expected)
        {
            var solution = new Solution(new[] {expression});
            Assert.Equal(expected, solution.PartOne());
        }

        [Fact]
        public void TestPartOne()
        {
            var solution = new Solution(Input);
            Assert.Equal(AnswerPartOne, solution.PartOne());
        }

        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 231)]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [InlineData("2 * 3 + (4 * 5)", 46)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
        public void TestPartTwoExamples(string expression, long expected)
        {
            var solution = new Solution(new[] {expression});
            Assert.Equal(expected, solution.PartTwo());
        }

        [Fact]
        public void TestPartTwo()
        {
            var solution = new Solution(Input);
            Assert.Equal(AnswerPartTwo, solution.PartTwo());
        }
    }
}
