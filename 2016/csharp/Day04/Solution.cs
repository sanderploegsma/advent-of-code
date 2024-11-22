using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2016.Day04
{
    internal class Solution
    {
        private readonly IReadOnlyCollection<Room> _rooms;

        public Solution(IEnumerable<string> input)
        {
            var rooms = new List<Room>();

            foreach (var line in input)
            {
                if (ValidateRoom(line, out var room))
                {
                    rooms.Add(room);
                }
            }

            _rooms = rooms;
        }

        public int PartOne() => _rooms.Sum(r => r.SectorId);

        public int PartTwo()
        {
            var decrypted = _rooms.ToDictionary(DecryptRoomName, r => r.SectorId);
            var target = decrypted.First(x => x.Key.Contains("north") && x.Key.Contains("pole"));
            return target.Value;
        }

        internal static string DecryptRoomName(Room room)
        {
            var decrypted = room.EncryptedName.Select(c => c == '-' ? ' ' : RotateChar(c, room.SectorId));
            return string.Concat(decrypted);
        }

        private static char RotateChar(char c, int n)
        {
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            var idx = alphabet.IndexOf(c);
            var newIdx = (idx + n) % alphabet.Length;
            return alphabet[newIdx];
        }

        internal static bool ValidateRoom(string room, out Room validRoom)
        {
            const string pattern = @"(?<name>[a-z\-]+)\-(?<id>\d+)\[(?<checksum>[a-z]{5})\]";
            validRoom = new Room();

            var match = Regex.Match(room, pattern);

            if (!match.Success)
                return false;

            validRoom.EncryptedName = match.Groups["name"].Value;
            validRoom.SectorId = int.Parse(match.Groups["id"].Value);
            validRoom.Checksum = match.Groups["checksum"].Value;

            var letterCount =
                from c in validRoom.EncryptedName
                where c != '-'
                group c by c
                into grouped
                orderby grouped.Count() descending, grouped.Key ascending
                select grouped.Key;

            return string.Concat(letterCount.Take(5)) == validRoom.Checksum;
        }
    }

    internal class Room
    {
        public string EncryptedName { get; set; }
        public int SectorId { get; set; }
        public string Checksum { get; set; }
    }
}
