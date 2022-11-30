using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common
{
    public static class NumbersExtensions
    {
        public static int Product(this IEnumerable<int> numbers) => numbers.Aggregate(1, (a, b) => a * b);
        public static long Product(this IEnumerable<long> numbers) => numbers.Aggregate(1L, (a, b) => a * b);

        public static int Mod(this int n, int m) => (n % m + m) % m;
    }
}