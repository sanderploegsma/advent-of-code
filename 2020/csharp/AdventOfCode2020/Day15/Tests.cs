using System.IO;
using Xunit;

namespace AdventOfCode2020.Day15
{
    public class Tests
    {
        private const string Input = "9,6,0,10,18,2,1";
            
        [Theory]
        [InlineData("0,3,6", 436)]
        [InlineData("1,3,2", 1)]
        [InlineData("2,1,3", 10)]
        [InlineData("1,2,3", 27)]
        [InlineData("2,3,1", 78)]
        [InlineData("3,2,1", 438)]
        [InlineData("3,1,2", 1836)]
        [InlineData(Input, 1238)]
        public void TestPartOne(string input, int expected)
        {
            var solution = new Solution(input);
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData("0,3,6", 175594)]
        [InlineData("1,3,2", 2578)]
        [InlineData("2,1,3", 3544142)]
        [InlineData("1,2,3", 261214)]
        [InlineData("2,3,1", 6895259)]
        [InlineData("3,2,1", 18)]
        [InlineData("3,1,2", 362)]
        [InlineData(Input, 3745954)]
        public void TestPartTwo(string input, int expected)
        {
            var solution = new Solution(input);
            Assert.Equal(expected, solution.PartTwo());
        }
    }
}