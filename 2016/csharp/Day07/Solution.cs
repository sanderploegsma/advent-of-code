using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace AdventOfCode2016.Day07
{
    internal class Solution
    {
        private readonly IReadOnlyCollection<IpV7Address> _addresses;

        public Solution(IEnumerable<string> input)
        {
            _addresses = input.Select(IpV7Address.Parse).ToList();
        }

        public int PartOne() => _addresses.Count(x => x.IsTlsSupported);

        public int PartTwo() => _addresses.Count(x => x.IsSslSupported);
    }

    internal class IpV7Address
    {
        public IpV7Address(IReadOnlyCollection<string> hypernetSequences, IReadOnlyCollection<string> supernetSequences)
        {
            HypernetSequences = hypernetSequences;
            SupernetSequences = supernetSequences;
        }

        public IReadOnlyCollection<string> HypernetSequences { get; }
        public IReadOnlyCollection<string> SupernetSequences { get; }

        public bool IsTlsSupported => SupernetSequences.Any(HasAbba) && !HypernetSequences.Any(HasAbba);

        public bool IsSslSupported
        {
            get
            {
                var aba = SupernetSequences.SelectMany(GetAba);
                var bab = HypernetSequences.SelectMany(GetAba);

                return aba.Any(x => bab.Any(y => AreComplements(x, y)));
            }
        }

        private static bool HasAbba(string sequence)
            => sequence.Windowed(4).Select(x => string.Concat(x)).Any(IsAba);

        private static bool IsAba(string sequence)
            => sequence == sequence.Rev() && sequence.Distinct().Count() > 1;

        private static bool AreComplements(string left, string right)
        {
            var leftChars = string.Concat(left.Distinct().OrderBy(x => x));
            var rightChars = string.Concat(right.Distinct().OrderBy(x => x));

            return left != right && leftChars == rightChars;
        }

        private static IEnumerable<string> GetAba(string sequence) =>
            sequence
                .Windowed(3)
                .Select(x => string.Concat(x))
                .Where(IsAba);

        public static IpV7Address Parse(string address)
        {
            var sequences = address.Split('[', ']');

            var supernet = sequences.Where((x, i) => i % 2 == 0).ToList();
            var hypernet = sequences.Where((x, i) => i % 2 == 1).ToList();

            return new IpV7Address(hypernet, supernet);
        }
    }
}
