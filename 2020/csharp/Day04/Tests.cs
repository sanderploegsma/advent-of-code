using System.IO;
using Xunit;

namespace AdventOfCode2020.Day04
{
    public class Tests
    {
        private const string ExamplePartOneFile = "Day04\\ExamplePartOne.txt";
        private const string ExampleInvalidFile = "Day04\\ExampleInvalid.txt";
        private const string ExampleValidFile = "Day04\\ExampleValid.txt";
        private const string InputFile = "Day04\\Input.txt";
        
        [Theory]
        [InlineData(ExamplePartOneFile, 2)]
        [InlineData(InputFile, 237)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadAllText(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleInvalidFile, 0)]
        [InlineData(ExampleValidFile, 4)]
        [InlineData(InputFile, 172)]
        public void TestPartTwo(string file, long expected)
        {
            var solution = new Solution(File.ReadAllText(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}