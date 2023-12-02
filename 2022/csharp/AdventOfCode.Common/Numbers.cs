using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common
{
    public static class NumbersExtensions
    {
        public static int Product(this IEnumerable<int> numbers) => numbers.Aggregate(1, (a, b) => a * b);
        public static int Product<T>(this IEnumerable<T> items, Func<T, int> selector) => items.Select(selector).Product();
        public static long Product(this IEnumerable<long> numbers) => numbers.Aggregate(1L, (a, b) => a * b);
        public static long Product<T>(this IEnumerable<T> items, Func<T, long> selector) => items.Select(selector).Product();

        public static int Mod(this int n, int m) => (n % m + m) % m;
    }
}