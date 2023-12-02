using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day15
{
    internal class Solution
    {
        private readonly IReadOnlyList<int> _startingNumbers;

        public Solution(string input)
        {
            _startingNumbers = input.Split(",").Select(int.Parse).ToList();
        }

        public int PartOne() => Generate().Skip(2020 - 1).First();

        public int PartTwo() => Generate().Skip(30_000_000 - 1).First();

        private IEnumerable<int> Generate()
        {
            var state = new Dictionary<int, int>();

            for (var i = 0; i < _startingNumbers.Count - 1; i++)
            {
                var n = _startingNumbers[i];
                state[n] = i + 1;
                yield return n;
            }

            var number = _startingNumbers.Last();
            var turn = _startingNumbers.Count;

            while (true)
            {
                yield return number;
                var nextNumber = state.TryGetValue(number, out var lastTurn) ? turn - lastTurn : 0;

                state[number] = turn;
                number = nextNumber;
                turn++;
            }
        }
    }
}