using System.IO;
using Xunit;

namespace AdventOfCode2020.Day03
{
    public class Tests
    {
        private const string ExampleFile = "Day03\\Example.txt";
        private const string InputFile = "Day03\\Input.txt";
        
        [Theory]
        [InlineData(ExampleFile, 7)]
        [InlineData(InputFile, 216)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleFile, 336L)]
        [InlineData(InputFile, 6708199680L)]
        public void TestPartTwo(string file, long expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}