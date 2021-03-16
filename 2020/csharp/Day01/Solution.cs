using System.Collections.Generic;
using System.Linq;
using Combinatorics.Collections;

namespace AdventOfCode2020.Day01
{
    internal class Solution
    {
        private readonly IList<int> _entries;

        public Solution(IEnumerable<string> input)
        {
            _entries = input.Select(int.Parse).ToList();
        }
        
        public int PartOne() =>
            FindEntriesSummingTo2020(_entries, 2).Product();
        
        public int PartTwo() =>
            FindEntriesSummingTo2020(_entries, 3).Product();

        private static IEnumerable<int> FindEntriesSummingTo2020(IList<int> entries, int numberOfEntries) => 
            new Combinations<int>(entries, numberOfEntries).First(candidates => candidates.Sum() == 2020);
    }
}