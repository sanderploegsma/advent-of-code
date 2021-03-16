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
            var numberOfSeats = (int) Math.Pow(2, NumberOfRowBits + NumberOfColBits);
            var (min, max) = (_seats.Min(), _seats.Max());
            
            var missing =
                from seat in Enumerable.Range(0, numberOfSeats)
                where seat > min && seat < max
                where !_seats.Contains(seat)
                select seat;

            return missing.Single();
        }

        private static int ParseSeatId(string seat)
        {
            var id = seat
                .Replace('F', '0')
                .Replace('B', '1')
                .Replace('L', '0')
                .Replace('R', '1');
            
            return Convert.ToInt32(id, 2);
        }
    }
}