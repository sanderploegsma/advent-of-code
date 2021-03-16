using System.IO;
using Xunit;

namespace AdventOfCode2020.Day01
{
    public class Tests
    {
        private const string ExampleFile = "Day01\\Example.txt";
        private const string InputFile = "Day01\\Input.txt";
        
        [Theory]
        [InlineData(ExampleFile, 514579)]
        [InlineData(InputFile, 1015476)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleFile, 241861950)]
        [InlineData(InputFile, 200878544)]
        public void TestPartTwo(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}