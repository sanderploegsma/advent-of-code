using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace AdventOfCode2016
{
    public static class StringExtensions
    {
        public static string Hash(this string value, HashAlgorithm algorithm)
        {
            var input = Encoding.ASCII.GetBytes(value);
            return string.Concat(algorithm.ComputeHash(input).Select(b => b.ToString("X2")));
        }

        public static string Rev(this string value) => string.Concat(value.Reverse());
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Windowed<T>(this IEnumerable<T> source, int windowSize)
        {
            var items = source.ToArray();
            for (var i = 0; i < items.Length - windowSize + 1; i++)
            {
                yield return items[i..(i + windowSize)];
            }
        }
    }

    public class EnumerableExtensionsTest
    {
        [Fact]
        public void TestWindowed()
        {
            var input = new[] {1, 2, 3, 4, 5};
            var expected = new[] {new[] {1, 2}, new[] {2, 3}, new[] {3, 4}, new[] {4, 5}};
            Assert.Equal(expected, input.Windowed(2));
        }
    }
}
