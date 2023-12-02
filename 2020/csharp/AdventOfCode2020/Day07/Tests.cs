using System.IO;
using Xunit;

namespace AdventOfCode2020.Day07
{
    public class Tests
    {
        private const string ExampleOneFile = "Day07\\ExampleOne.txt";
        private const string ExampleTwoFile = "Day07\\ExampleTwo.txt";
        private const string InputFile = "Day07\\Input.txt";
        
        [Theory]
        [InlineData(ExampleOneFile, 4)]
        [InlineData(InputFile, 144)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleOneFile, 32)]
        [InlineData(ExampleTwoFile, 126)]
        [InlineData(InputFile, 5956)]
        public void TestPartTwo(string file, long expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}