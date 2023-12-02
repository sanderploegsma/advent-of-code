using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day19
{
    internal class Solution
    {
        private readonly IDictionary<string, string> _rules;
        private readonly IReadOnlyCollection<string> _messages;

        public Solution(IEnumerable<string> input)
        {
            _rules = input
                .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
                .Select(line => line.Split(": "))
                .ToDictionary(x => x[0], x => x[1]);

            _messages = input.Skip(_rules.Count + 1).ToList();
        }

        public int PartOne()
        {
            var pattern = CreatePattern("0", _rules);
            return _messages.Count(x => Regex.IsMatch(x, pattern));
        }

        public int PartTwo()
        {
            var newRules = _rules.ToDictionary(x => x.Key, x => x.Value);
            newRules["8"] = "42 | 42 8";
            newRules["11"] = "42 31 | 42 11 31";

            var pattern = CreatePattern("0", newRules);
            return _messages.Count(x => Regex.IsMatch(x, pattern));
        }

        private static string CreatePattern(string rule, IDictionary<string, string> rules) =>
            $"^{CreatePattern(rule, rules, 0)}$";

        private static string CreatePattern(string rule, IDictionary<string, string> rules, int nestingLevel)
        {
            const string singleCharacterPattern = "^\"(?<character>[a-z])\"$";

            if (nestingLevel > 25)
                return "";

            var subRule = rules[rule];
            if (Regex.IsMatch(subRule, singleCharacterPattern))
            {
                return Regex.Replace(subRule, singleCharacterPattern, match => match.Groups["character"].Value);
            }

            var parts = subRule.Split(" ")
                .Select(x => x == "|" ? x : CreatePattern(x, rules, nestingLevel + 1));

            return $"({string.Concat(parts)})";
        }
    }
}