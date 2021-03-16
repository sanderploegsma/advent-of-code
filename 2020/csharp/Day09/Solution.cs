using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Combinatorics.Collections;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace AdventOfCode2020.Day09
{
    internal class Solution
    {
        private readonly long[] _preamble;
        private readonly long[] _data;

        private readonly Lazy<long> _invalidNumber;

        public Solution(int preambleSize, IEnumerable<string> input)
        {
            var data = input.Select(line => Convert.ToInt64(line)).ToList();
            _preamble = data.Take(preambleSize).ToArray();
            _data = data.Skip(preambleSize).ToArray();
            _invalidNumber = new Lazy<long>(() => FindInvalidNumber(_preamble, _data));
        }

        public long PartOne() => _invalidNumber.Value;

        public long PartTwo() => FindWeakness(_preamble.Concat(_data).ToArray(), _invalidNumber.Value);

        private static long FindWeakness(long[] data, long targetValue)
        {
            var range = new List<long>();
            for (var i = 0; range.Sum() < targetValue && i < data.Length; i++)
                range.Add(data[i]);

            if (range.Sum() != targetValue)
                return FindWeakness(data.Skip(1).ToArray(), targetValue);

            return range.Min() + range.Max();
        }

        private static long FindInvalidNumber(long[] preamble, long[] data)
        {
            var nextNumber = data.First();
            var pairs =
                from pair in new Combinations<long>(preamble.ToList(), 2)
                where pair.Sum() == nextNumber
                select pair;

            return pairs.Any()
                ? FindInvalidNumber(preamble.Skip(1).Append(nextNumber).ToArray(), data.Skip(1).ToArray())
                : nextNumber;
        }
    }
}