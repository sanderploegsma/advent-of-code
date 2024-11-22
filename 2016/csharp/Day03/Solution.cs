using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Day03
{
    internal class Solution
    {
        private readonly int[,] _data;

        public Solution(IEnumerable<string> input)
        {
            var lines = input.ToArray();
            _data = new int[lines.Length, 3];

            for (var i = 0; i < lines.Length; i++)
            {
                var xs = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
                for (var j = 0; j < 3; j++)
                {
                    _data[i, j] = int.Parse(xs[j]);
                }
            }
        }

        public int PartOne()
        {
            var valid = 0;

            for (var i = 0; i < _data.GetLength(0); i++)
            {
                if (IsValidTriangle(_data[i, 0], _data[i, 1], _data[i, 2]))
                {
                    valid++;
                }
            }

            return valid;
        }

        public int PartTwo()
        {
            var valid = 0;

            for (var i = 0; i < _data.GetLength(0); i += 3)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (IsValidTriangle(_data[i, j], _data[i + 1, j], _data[i + 2, j]))
                    {
                        valid++;
                    }
                }
            }

            return valid;
        }

        private static bool IsValidTriangle(int a, int b, int c) =>
            a + b > c &&
            a + c > b &&
            b + c > a;
    }
}
