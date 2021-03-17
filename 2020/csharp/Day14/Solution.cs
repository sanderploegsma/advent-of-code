using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Combinatorics.Collections;

namespace AdventOfCode2020.Day14
{
    internal class Solution
    {
        private const string MaskPattern = @"mask = (?<mask>[X01]{36})";
        private const string AssignmentPattern = @"mem\[(?<address>\d+)\] = (?<value>\d+)";

        private readonly IReadOnlyCollection<IOperation> _operations;

        public Solution(IEnumerable<string> input)
        {
            _operations = input.Select(ParseLine).ToList();
        }

        public long PartOne()
        {
            var memory = new Dictionary<long, long>();
            var masks = new {Mask0 = 0L, Mask1 = 0L};

            foreach (var operation in _operations)
            {
                switch (operation)
                {
                    case Bitmask bitmask:
                        masks = new
                        {
                            Mask0 = Convert.ToInt64(bitmask.Value.Replace('X', '1'), 2),
                            Mask1 = Convert.ToInt64(bitmask.Value.Replace('X', '0'), 2),
                        };
                        break;
                    case Assignment assignment:
                        memory[assignment.Address] = (assignment.Value & masks.Mask0) | masks.Mask1;
                        break;
                }
            }

            return memory.Values.Sum();
        }

        public long PartTwo()
        {
            var memory = new Dictionary<long, long>();
            var masks = new {Mask0 = 0L, Mask1 = new[] {0L}};

            foreach (var operation in _operations)
            {
                switch (operation)
                {
                    case Bitmask bitmask:
                        var floatingBits = bitmask.Value.Reverse().Indexed()
                            .Where(x => x.Value == 'X')
                            .Select(x => x.Index);

                        var ones = Convert.ToInt64(bitmask.Value.Replace('X', '0'), 2);

                        masks = new
                        {
                            Mask0 = Convert.ToInt64(bitmask.Value.Replace('0', '1').Replace('X', '0'), 2),
                            Mask1 = floatingBits.SubSets()
                                .Select(bits => bits.Aggregate(0L, (mask, bit) => 1L << bit | mask))
                                .Select(mask => ones | mask)
                                .ToArray()
                        };
                        break;
                    case Assignment assignment:
                        foreach (var mask1 in masks.Mask1)
                        {
                            var address = (assignment.Address & masks.Mask0) | mask1;
                            memory[address] = assignment.Value;
                        }

                        break;
                }
            }

            return memory.Values.Sum();
        }

        private static IOperation ParseLine(string line)
        {
            var match = Regex.Match(line, MaskPattern);
            if (match.Success)
                return new Bitmask(match.Groups["mask"].Value);

            match = Regex.Match(line, AssignmentPattern);
            if (match.Success)
            {
                var address = Convert.ToInt64(match.Groups["address"].Value);
                var value = Convert.ToInt64(match.Groups["value"].Value);
                return new Assignment(address, value);
            }

            throw new ArgumentException($"Unable to parse line '{line}'");
        }
    }

    internal interface IOperation
    {
    }

    internal class Bitmask : IOperation
    {
        public Bitmask(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }

    internal class Assignment : IOperation
    {
        public Assignment(long address, long value)
        {
            Address = address;
            Value = value;
        }

        public long Address { get; }
        public long Value { get; }
    }
}