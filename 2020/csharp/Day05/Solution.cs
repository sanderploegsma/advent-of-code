using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day05
{
    internal class Solution
    {
        private const int NumberOfRows = 128;
        private const int NumberOfColumns = 8;

        private readonly IReadOnlyCollection<int> _seats;
        
        public Solution(IEnumerable<string> input)
        {
            _seats = input.Select(FindSeat).ToList();
        }

        public int PartOne() => _seats.Max();

        public long PartTwo()
        {
            var missing =
                from seat in AllPossibleSeats()
                where !_seats.Contains(seat)
                where _seats.Contains(seat - 1)
                where _seats.Contains(seat + 1)
                select seat;

            return missing.Single();
        }

        private static int FindSeat(string seat)
        {
            var rows = Enumerable.Range(0, NumberOfRows).ToArray();
            var columns = Enumerable.Range(0, NumberOfColumns).ToArray();
            var partitions = seat.Select(ParsePartition).ToArray();

            var row = BinaryTraverse(rows, partitions.Take(7).ToList());
            var column = BinaryTraverse(columns, partitions.Skip(7).ToList());

            return SeatId(row, column);
        }

        private static int BinaryTraverse(IReadOnlyCollection<int> values, IReadOnlyCollection<int> choices)
        {
            if (choices.Count == 0)
                return values.First();

            var choice = choices.First();
            var nextChoices = choices.Skip(1).ToArray();
            
            var middle = values.Count / 2;
            var (left, right) = (values.Take(middle).ToArray(), values.Skip(middle).ToArray());

            return choice < 0 ? BinaryTraverse(left, nextChoices) : BinaryTraverse(right, nextChoices);
        }

        private static int ParsePartition(char partition) => partition switch
        {
            'F' => -1,
            'B' => 1,
            'L' => -1,
            'R' => 1,
            _ => throw new ArgumentException($"Invalid partition '{partition}'")
        };
        
        private static int SeatId(int row, int column) => row * 8 + column;
        
        private static IEnumerable<int> AllPossibleSeats()
        {
            for (var row = 0; row < NumberOfRows; row++)
            for (var col = 0; col < NumberOfColumns; col++)
                yield return SeatId(row, col);
        }
    }
}