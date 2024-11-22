using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using Combinatorics.Collections;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace AdventOfCode2020.Day09
{
    internal class Solution
    {
        private readonly int _preambleSize;
        private readonly long[] _data;

        private readonly Lazy<long> _invalidNumber;

        public Solution(int preambleSize, IEnumerable<string> input)
        {
            _preambleSize = preambleSize;
            _data = input.Select(line => Convert.ToInt64(line)).ToArray();
            _invalidNumber = new Lazy<long>(FindInvalidNumber);
        }

        public long PartOne() => _invalidNumber.Value;

        public long PartTwo()
        {
            var target = _invalidNumber.Value;
            var targetIndex = Array.IndexOf(_data, target);
            var result = Enumerable.Range(0, targetIndex)
                .Select(n => _data.Skip(n))
                .Select(values => SelectRangeWithSumNotExceedingTarget(values, target))
                .First(values => values.Sum() == target)
                .ToList();

            return result.Min() + result.Max();
        }

        private long FindInvalidNumber()
        {
            var windowSize = _preambleSize + 1;

            return _data
                .ToObservable()
                .Buffer(windowSize, 1)
                .ToEnumerable()
                .First(data =>
                    new Combinations<long>(data.SkipLast(1).ToList(), 2)
                        .All(pair => pair.Sum() != data.Last()))
                .Last();
        }

        private static IEnumerable<long> SelectRangeWithSumNotExceedingTarget(IEnumerable<long> numbers, long target)
        {
            var rangeWithSum = numbers
                .ToObservable()
                .Scan(new {Value = 0L, Sum = 0L}, (acc, cur) => new {Value = cur, Sum = acc.Sum + cur});

            return rangeWithSum
                .TakeWhile(value => value.Sum <= target)
                .Select(value => value.Value)
                .ToEnumerable();
        }
    }
}
