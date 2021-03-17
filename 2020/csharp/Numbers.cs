using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;

namespace AdventOfCode2020
{
    public static class NumbersExtensions
    {
        public static int Product(this IEnumerable<int> numbers) => numbers.Aggregate(1, (a, b) => a * b);
        public static long Product(this IEnumerable<long> numbers) => numbers.Aggregate(1L, (a, b) => a * b);

        public static int Mod(this int n, int m) => (n % m + m) % m;
    }

    public class NumbersExtensionsTest
    {
        [Property]
        public Property Mod_AlwaysReturnsANumberGreaterThanOrEqualToZero(int n, int m)
        {
            Func<bool> property = () => n.Mod(m) >= 0;
            return property.When(m > 0);
        }
    }
}