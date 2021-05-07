using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2016
{
    public static class StringExtensions
    {
        public static string Hash(this string value, HashAlgorithm algorithm)
        {
            var input = Encoding.ASCII.GetBytes(value);
            return string.Concat(algorithm.ComputeHash(input).Select(b => b.ToString("X2")));
        }
    }
}