using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day13
{
    internal class Solution
    {
        private const string OutOfService = "x";
        
        private readonly long _timestamp;
        private readonly (int Offset, int Id)[] _buses;
        
        public Solution(IReadOnlyList<string> input)
        {
            _timestamp = Convert.ToInt64(input[0]);
            _buses = (
                from bus in input[1].Split(",").Indexed()
                where bus.Value != OutOfService
                select (bus.Index, int.Parse(bus.Value))
            ).ToArray();
        }

        public long PartOne()
        {
            var times =
                from bus in _buses
                let time = (bus.Id - _timestamp % bus.Id) % bus.Id
                orderby time
                select bus.Id * time;

            return times.First(); 
        }

        public long PartTwo()
        {
            var busIds = (from bus in _buses select (long) bus.Id).ToArray();
            var offsets = (from bus in _buses select (long) bus.Id - bus.Offset).ToArray();
            return ChineseRemainderTheorem.Solve(busIds, offsets);
        }
    }
    
    /// <summary>
    /// Source: https://rosettacode.org/wiki/Chinese_remainder_theorem#C.23
    /// </summary>
    internal static class ChineseRemainderTheorem
    {
        public static long Solve(long[] n, long[] a)
        {
            var prod = n.Product();
            var sm = 0L;
            for (var i = 0; i < n.Length; i++)
            {
                var p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }
            return sm % prod;
        }
 
        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            var b = a % mod;
            for (var x = 1; x < mod; x++)
            {
                if (b * x % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }
}