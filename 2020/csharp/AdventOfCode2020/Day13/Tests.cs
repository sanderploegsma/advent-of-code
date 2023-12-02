using System.IO;
using Xunit;

namespace AdventOfCode2020.Day13
{
    public class Tests
    {
        private const string ExampleFile = "Day13\\Example.txt";
        private const string InputFile = "Day13\\Input.txt";
        
        [Theory]
        [InlineData(ExampleFile, 295L)]
        [InlineData(InputFile, 2382L)]
        public void TestPartOne(string file, long expected)
        {
            var solution = new Solution(File.ReadAllLines(file));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(ExampleFile, 1068781L)]
        [InlineData(InputFile, 906332393333683L)]
        public void TestPartTwo(string file, long expected)
        {
            var solution = new Solution(File.ReadAllLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}