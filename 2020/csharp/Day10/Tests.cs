using System.IO;
using Xunit;

namespace AdventOfCode2020.Day10
{
    public class Tests
    {
        private const string ExampleOneFile = "Day10\\ExampleOne.txt";
        private const string ExampleTwoFile = "Day10\\ExampleTwo.txt";
        private const string InputFile = "Day10\\Input.txt";
        
        [Theory]
        [InlineData(ExampleOneFile, 35L)]
        [InlineData(ExampleTwoFile, 220L)]
        [InlineData(InputFile, 1890L)]
        public void TestPartOne(string file, long expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleOneFile, 8L)]
        [InlineData(ExampleTwoFile, 19208L)]
        [InlineData(InputFile, 49607173328384L)]
        public void TestPartTwo(string file, long expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}