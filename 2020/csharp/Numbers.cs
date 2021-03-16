using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public static class NumbersExtensions
    {
        public static int Product(this IEnumerable<int> numbers) => numbers.Aggregate(1, (a, b) => a * b);
    }
}