using System.IO;
using Xunit;

namespace AdventOfCode2020.Day06
{
    public class Tests
    {
        private const string ExampleFile = "Day06\\Example.txt";
        private const string InputFile = "Day06\\Input.txt";
        
        [Theory]
        [InlineData(ExampleFile, 11)]
        [InlineData(InputFile, 6809)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadAllText(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleFile, 6)]
        [InlineData(InputFile, 3394)]
        public void TestPartTwo(string file, long expected)
        {
            var solution = new Solution(File.ReadAllText(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}