using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    internal class Solution
    {
        private readonly IReadOnlyCollection<int> _adapters;

        public Solution(IEnumerable<string> input)
        {
            _adapters = input.Select(int.Parse).ToList();
        }

        public long PartOne()
        {
            var differences = _adapters
                .Append(0)
                .Append(_adapters.Max() + 3)
                .OrderBy(x => x)
                .Pairwise()
                .GroupBy(x => x.Item2 - x.Item1)
                .ToDictionary(g => g.Key, g => g.LongCount());

            return differences[1] * differences[3];
        }

        public long PartTwo()
        {
            var cache = new Dictionary<int, long>
            {
                {_adapters.Max(), 1L}
            };

            foreach (var adapter in _adapters.Append(0).OrderByDescending(x => x).Skip(1))
            {
                var count = Enumerable.Range(1, 3)
                    .Select(delta => adapter + delta)
                    .Select(x => cache.GetValueOrDefault(x, 0L))
                    .Sum();

                cache[adapter] = count;
            }

            return cache[0];
        }
    }
}