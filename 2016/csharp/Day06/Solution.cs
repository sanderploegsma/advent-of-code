using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Day06
{
    internal class Solution
    {
        private readonly char[,] _input;

        public Solution(IEnumerable<string> input)
        {
            _input = Matrix.Create(input.Select(x => x.ToArray()).ToArray());
        }

        public string PartOne()
        {
            var result = "";

            for (var i = 0; i < _input.Width(); i++)
            {
                result += GetColumnCharacterCount(i)
                    .OrderByDescending(x => x.Count)
                    .Select(x => x.Character)
                    .First();
            }

            return result;
        }

        public string PartTwo()
        {
            var result = "";

            for (var i = 0; i < _input.Width(); i++)
            {
                result += GetColumnCharacterCount(i)
                    .OrderBy(x => x.Count)
                    .Select(x => x.Character)
                    .First();
            }

            return result;
        }

        private IEnumerable<(char Character, int Count)> GetColumnCharacterCount(int column) =>
            from c in _input.GetColumn(column)
            group c by c
            into g
            select (g.Key, g.Count());
    }
}