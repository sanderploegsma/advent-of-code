using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day05
{
    internal class Solution
    {
        private const int NumberOfRowBits = 7;
        private const int NumberOfColBits = 3;

        private readonly IReadOnlyCollection<int> _seats;
        
        public Solution(IEnumerable<string> input)
        {
            _seats = input.Select(ParseSeatId).ToList();
        }

        public int PartOne() => _seats.Max();

        public int PartTwo()
        {
            var missing =
                from seat in AllPossibleSeats()
                where !_seats.Contains(seat)
                where _seats.Contains(seat - 1)
                where _seats.Contains(seat + 1)
                select seat;

            return missing.Single();
        }

        private static int ParseSeatId(string seat)
        {
            var id = seat.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');
            return Convert.ToInt32(id, 2);
        }

        private static IEnumerable<int> AllPossibleSeats()
        {
            var rows = (int) Math.Pow(2, NumberOfRowBits);
            var cols = (int) Math.Pow(2, NumberOfColBits);
            
            for (var row = 0; row < rows; row++)
            for (var col = 0; col < cols; col++)
                yield return (row << NumberOfColBits) + col;
        }
    }
}