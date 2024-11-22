using System.IO;
using Xunit;

namespace AdventOfCode2020.Day20
{
    public class Tests
    {
        private const string InputFile = "Day20\\Input.txt";
        private const string ExampleFile = "Day20\\Example.txt";

        [Theory]
        [InlineData(ExampleFile, 20899048083289)]
        [InlineData(InputFile, 7901522557967)]
        public void TestPartOne(string file, long expected)
        {
            var solution = new Solution(File.ReadAllText(file));
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [InlineData(ExampleFile, 273)]
        [InlineData(InputFile, 2476)]
        public void TestPartTwo(string file, int expected)
        {
            var solution = new Solution(File.ReadAllText(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}
