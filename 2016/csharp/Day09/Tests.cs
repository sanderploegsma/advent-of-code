using System.Collections.Generic;
using System.IO;
using Xunit;

namespace AdventOfCode2016.Day09
{
    public class Tests
    {
        private const string InputFile = @"Day09\\Input.txt";

        [Theory]
        [MemberData(nameof(PartOneData))]
        public void PartOne(string input, long expected)
        {
            var solution = new Solution(input);
            Assert.Equal(expected, solution.PartOne());
        }

        [Theory]
        [MemberData(nameof(PartTwoData))]
        public void PartTwo(string input, long expected)
        {
            var solution = new Solution(input);
            Assert.Equal(expected, solution.PartTwo());
        }

        public static IEnumerable<object[]> PartOneData()
        {
            yield return new object[] {"ADVENT", 6L};
            yield return new object[] {"A(1x5)BC", 7L};
            yield return new object[] {"(3x3)XYZ", 9L};
            yield return new object[] {"A(2x2)BCD(2x2)EFG", 11L};
            yield return new object[] {"(6x1)(1x3)A", 6L};
            yield return new object[] {"X(8x2)(3x3)ABCY", 18L};
            yield return new object[] {File.ReadAllText(InputFile), 70186L};
        }
        
        public static IEnumerable<object[]> PartTwoData()
        {
            yield return new object[] {"(3x3)XYZ", 9L};
            yield return new object[] {"X(8x2)(3x3)ABCY", 20L};
            yield return new object[] {"(27x12)(20x12)(13x14)(7x10)(1x12)A", 241920L};
            yield return new object[] {"(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", 445L};
            yield return new object[] {File.ReadAllText(InputFile), 10915059201L};
        }
    }
}