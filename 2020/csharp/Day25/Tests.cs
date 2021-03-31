using Xunit;

namespace AdventOfCode2020.Day25
{
    public class Tests
    {
        [Theory]
        [InlineData(5764801, 17807724, 14897079)]
        [InlineData(335121, 363891, 9420461)]
        public void TestPartOne(long cardPublicKey, long doorPublicKey, long expected)
        {
            var solution = new Solution(cardPublicKey, doorPublicKey);
            Assert.Equal(expected, solution.PartOne());
        }
    }
}