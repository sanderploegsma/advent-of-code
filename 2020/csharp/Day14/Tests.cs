using System.IO;
using Xunit;

namespace AdventOfCode2020.Day14
{
    public class Tests
    {
        private const string ExampleOneFile = "Day14\\ExampleOne.txt";
        private const string ExampleTwoFile = "Day14\\ExampleTwo.txt";
        private const string InputFile = "Day14\\Input.txt";
        
        [Theory]
        [InlineData(ExampleOneFile, 165L)]
        [InlineData(InputFile, 4886706177792L)]
        public void TestPartOne(string file, long expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleTwoFile, 208L)]
        [InlineData(InputFile, 3348493585827L)]
        public void TestPartTwo(string file, long expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}