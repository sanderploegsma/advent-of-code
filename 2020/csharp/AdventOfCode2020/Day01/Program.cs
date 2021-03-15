using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Combinatorics.Collections;

namespace Day01
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var input = File.ReadLines("Input.txt").Select(int.Parse).ToList();
            
            Console.WriteLine("Part one: {0}", PartOne(input));
            Console.WriteLine("Part two: {0}", PartTwo(input));
        }

        private static int PartOne(IList<int> entries) =>
            FindEntriesSummingTo2020(entries, 2).Multiply();
        
        private static int PartTwo(IList<int> entries) =>
            FindEntriesSummingTo2020(entries, 3).Multiply();

        private static IEnumerable<int> FindEntriesSummingTo2020(IList<int> entries, int numberOfEntries) => 
            new Combinations<int>(entries, numberOfEntries).First(candidates => candidates.Sum() == 2020);
        
        private static int Multiply(this IEnumerable<int> items) => items.Aggregate(1, (a, b) => a * b);
    }
}