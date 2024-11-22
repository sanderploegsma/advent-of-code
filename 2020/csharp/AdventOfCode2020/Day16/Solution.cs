using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day16
{
    internal class Solution
    {
        private readonly IReadOnlyCollection<Rule> _rules;
        private readonly Ticket _myTicket;
        private readonly IReadOnlyCollection<Ticket> _otherTickets;

        public Solution(IReadOnlyCollection<string> input)
        {
            _rules = input.TakeWhile(line => !string.IsNullOrWhiteSpace(line)).Select(ParseRule).ToList();
            _myTicket = ParseTicket(input.Skip(_rules.Count + 2).First());
            _otherTickets = input.Skip(_rules.Count + 2 + 3).Select(ParseTicket).ToList();
        }

        public long PartOne() => _otherTickets.SelectMany(GetInvalidTicketFields).Sum();

        public long PartTwo()
        {
            var validTickets = _otherTickets.Where(ticket => !GetInvalidTicketFields(ticket).Any());
            return MapRulesToFields(validTickets)
                .Where(x => x.Value.Field.StartsWith("departure"))
                .Select(x => (long) _myTicket.FieldValues[x.Key])
                .Product();
        }

        private IEnumerable<int> GetInvalidTicketFields(Ticket ticket) =>
            ticket.FieldValues.Where(x => _rules.All(rule => !rule.IsSatisfied(x)));

        private IDictionary<int, Rule> MapRulesToFields(IEnumerable<Ticket> validTickets)
        {
            var mapping = new Dictionary<int, Rule>();
            var fieldIdToValues = validTickets
                .SelectMany(ticket => ticket.FieldValues.Indexed())
                .GroupBy(x => x.Index, x => x.Value)
                .ToDictionary(x => x.Key, x => x.ToList());

            var rules = _rules.ToList();
            while (rules.Count > 0)
            {
                var matchingRule = rules
                    .Select(rule => new {Rule = rule, Matches = fieldIdToValues.Where(x => x.Value.All(rule.IsSatisfied))})
                    .First(rule => rule.Matches.Count() == 1);

                var fieldIdx = matchingRule.Matches.Single().Key;
                mapping[fieldIdx] = matchingRule.Rule;
                rules.Remove(matchingRule.Rule);
                fieldIdToValues.Remove(fieldIdx);
            }

            return mapping;
        }

        private static Rule ParseRule(string line)
        {
            var fieldAndRanges = line.Split(": ");
            var ranges = fieldAndRanges[1].Split(" or ");

            return new Rule
            {
                Field = fieldAndRanges[0],
                Range1 = ParseRange(ranges[0]),
                Range2 = ParseRange(ranges[1]),
            };
        }

        private static (int, int) ParseRange(string range)
        {
            var fromAndTo = range.Split('-');
            return (int.Parse(fromAndTo[0]), int.Parse(fromAndTo[1]));
        }

        private static Ticket ParseTicket(string line) => new Ticket
        {
            FieldValues = line.Split(',').Select(int.Parse).ToArray()
        };
    }

    internal class Rule
    {
        public string Field { get; set; }
        public (int Lo, int Hi) Range1 { get; set; }
        public (int Lo, int Hi) Range2 { get; set; }

        public bool IsSatisfied(int value) => IsInRange(value, Range1.Lo, Range1.Hi) || IsInRange(value, Range2.Lo, Range2.Hi);

        private static bool IsInRange(int value, int lo, int hi) => lo <= value && hi >= value;
    }

    internal class Ticket
    {
        public int[] FieldValues { get; set; }
    }
}
