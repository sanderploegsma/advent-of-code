using System.Collections.Generic;
using System.IO;
using Xunit;

namespace AdventOfCode2020.Day02
{
    public class Tests
    {
        private const string ExampleFile = "Day02\\Example.txt";
        private const string InputFile = "Day02\\Input.txt";
        
        [Theory]
        [InlineData(ExampleFile, 2)]
        [InlineData(InputFile, 556)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleFile, 1)]
        [InlineData(InputFile, 605)]
        public void TestPartTwo(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}