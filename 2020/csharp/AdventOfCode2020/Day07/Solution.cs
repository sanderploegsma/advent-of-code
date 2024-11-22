using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace AdventOfCode2020.Day07
{
    internal class Solution
    {
        private const string ShinyGold = "shiny gold";

        private readonly IReadOnlyCollection<Bag> _bags;

        public Solution(IEnumerable<string> input)
        {
            _bags = input.Select(Parse).ToList();
        }

        public int PartOne()
        {
            var bags = new HashSet<string>();

            var queue = new Queue<string>();
            queue.Enqueue(ShinyGold);

            while (queue.Count > 0)
            {
                var color = queue.Dequeue();
                var parents =
                    from bag in _bags
                    where bag.Children.ContainsKey(color)
                    select bag.Color;

                foreach (var bag in parents)
                    if (bags.Add(bag))
                        queue.Enqueue(bag);
            }

            return bags.Count;
        }

        public int PartTwo(string color = ShinyGold) => _bags
            .First(b => b.Color == color)
            .Children
            .Sum(child => child.Value + child.Value * PartTwo(child.Key));

        private static Bag Parse(string line)
        {
            const string emptyBagPattern = @"^(?<color>.*) bags contain no other bags\.$";
            const string bagWithChildrenPattern = @"^(?<color>.*) bags contain (?<children>.*)\.$";

            var match = Regex.Match(line, emptyBagPattern);
            if (match.Success)
                return new Bag(match.Groups["color"].Value);

            match = Regex.Match(line, bagWithChildrenPattern);
            var bag = new Bag(match.Groups["color"].Value);

            foreach (var child in match.Groups["children"].Value.Split(", "))
            {
                var parts = child.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var count = int.Parse(parts[0]);
                var color = $"{parts[1]} {parts[2]}";
                bag.Children[color] = count;
            }

            return bag;
        }
    }

    internal class Bag
    {
        public Bag(string color)
        {
            Color = color;
            Children = new Dictionary<string, int>();
        }

        public string Color { get; }
        public IDictionary<string, int> Children { get; }
    }
}
