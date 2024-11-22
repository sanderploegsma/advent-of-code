using System.IO;
using System.Runtime.InteropServices;
using Xunit;

namespace AdventOfCode2020.Day21
{
    public class Tests
    {
        private const string InputFile = "Day21\\Input.txt";
        private const string ExampleFile = "Day21\\Example.txt";

        [Theory]
        [InlineData(ExampleFile, 5)]
        [InlineData(InputFile, 2098)]
        public void TestPartOne(string file, int expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [InlineData(ExampleFile, "mxmxvkd,sqjhc,fvjkl")]
        [InlineData(InputFile, "ppdplc,gkcplx,ktlh,msfmt,dqsbql,mvqkdj,ggsz,hbhsx")]
        public void TestPartTwo(string file, string expected)
        {
            var solution = new Solution(File.ReadLines(file));
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}
