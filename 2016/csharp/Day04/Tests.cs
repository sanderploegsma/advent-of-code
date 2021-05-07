using System.IO;
using Xunit;

namespace AdventOfCode2016.Day04
{
    public class Tests
    {
        private const string InputFile = @"Day04\Input.txt";
        
        [Theory]
        [InlineData(InputFile, 137896)]
        public void PartOne(string input, int expected)
        {
            var solution = new Solution(File.ReadLines(input));
            Assert.Equal(expected, solution.PartOne());
        }
        
        [Theory]
        [InlineData(InputFile, 501)]
        public void PartTwo(string input, int expected)
        {
            var solution = new Solution(File.ReadLines(input));
            Assert.Equal(expected, solution.PartTwo());
        }

        [Theory]
        [InlineData("aaaaa-bbb-z-y-x-123[abxyz]", true)]
        [InlineData("a-b-c-d-e-f-g-h-987[abcde]", true)]
        [InlineData("not-a-real-room-404[oarel]", true)]
        [InlineData("totally-real-room-200[decoy]", false)]
        public void TestValidateRoom(string room, bool expected)
        {
            Assert.Equal(expected, Solution.ValidateRoom(room, out _));
        }

        [Fact]
        public void TestDecryptRoomName()
        {
            var room = new Room {EncryptedName = "qzmt-zixmtkozy-ivhz", SectorId = 343};
            Assert.Equal("very encrypted name", Solution.DecryptRoomName(room));
        }
    }
}