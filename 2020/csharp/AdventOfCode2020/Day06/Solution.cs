using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day06
{
    internal class Solution
    {
        private readonly IReadOnlyCollection<IReadOnlyCollection<ISet<char>>> _groups;
        
        public Solution(string input)
        {
            _groups = input
                .Split(Environment.NewLine + Environment.NewLine)
                .Select(group => group.Split(Environment.NewLine).Select(line => line.ToHashSet()).ToList())
                .ToList();
        }

        public int PartOne() => _groups.Sum(group => group.UnionAll().Count);

        public int PartTwo() => _groups.Sum(group => group.IntersectAll().Count);
    }
}