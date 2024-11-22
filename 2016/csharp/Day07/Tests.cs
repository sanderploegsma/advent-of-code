using System.IO;
using Xunit;

namespace AdventOfCode2016.Day07
{
    public class Tests
    {
        private const string InputFile = @"Day07\\Input.txt";

        [Fact]
        public void PartOne()
        {
            const int expected = 115;
            var solution = new Solution(File.ReadLines(InputFile));
            Assert.Equal(expected, solution.PartOne());
        }

        [Fact]
        public void PartTwo()
        {
            const int expected = 231;
            var solution = new Solution(File.ReadLines(InputFile));
            Assert.Equal(expected, solution.PartTwo());
        }

        [Theory]
        [InlineData("abba[mnop]qrst", true)]
        [InlineData("abcd[bddb]xyyx", false)]
        [InlineData("aaaa[qwer]tyui", false)]
        [InlineData("ioxxoj[asdfgh]zxcvbn", true)]
        public void TestTls(string address, bool expected)
        {
            Assert.Equal(expected, IpV7Address.Parse(address).IsTlsSupported);
        }

        [Theory]
        [InlineData("aba[bab]xyz", true)]
        [InlineData("xyx[xyx]xyx", false)]
        [InlineData("aaa[kek]eke", true)]
        [InlineData("zazbz[bzb]cdb", true)]
        public void TestSsl(string address, bool expected)
        {
            Assert.Equal(expected, IpV7Address.Parse(address).IsSslSupported);
        }
    }
}
