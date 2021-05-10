using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2016.Day10
{
    internal class Solution
    {
        private readonly IDictionary<int, Node> _nodes;
        private readonly IDictionary<int, int> _inputs;
        private readonly IDictionary<int, int> _outputs;
        
        public Solution(IEnumerable<string> input)
        {
            _nodes = new Dictionary<int, Node>();
            _inputs = new Dictionary<int, int>();
            _outputs = new Dictionary<int, int>();

            foreach (var line in input)
            {
                var match = Regex.Match(line, @"value (?<value>\d+) goes to bot (?<id>\d+)");
                if (match.Success)
                {
                    var value = int.Parse(match.Groups["value"].Value);
                    var id = int.Parse(match.Groups["id"].Value);
                    _inputs[value] = id;
                    continue;
                }

                match = Regex.Match(line, @"bot (?<id>\d+) gives low to (?<lowType>bot|output) (?<lowId>\d+) and high to (?<highType>bot|output) (?<highId>\d+)");
                if (match.Success)
                {
                    var id = int.Parse(match.Groups["id"].Value);
                    var lowType = ParseDestinationType(match.Groups["lowType"].Value);
                    var lowId = int.Parse(match.Groups["lowId"].Value);
                    var highType = ParseDestinationType(match.Groups["highType"].Value);
                    var highId = int.Parse(match.Groups["highId"].Value);
                    _nodes[id] = new Node(new Destination { Type = lowType, Id = lowId }, new Destination { Type = highType, Id = highId});
                    continue;
                }

                throw new InvalidOperationException($"Unable to parse line: {line}");
            }
            
            Solve();
        }

        public int PartOne() => _nodes.Single(x => x.Value.Values.Contains(17) && x.Value.Values.Contains(61)).Key;

        public int PartTwo() => _outputs[0] * _outputs[1] * _outputs[2];

        private void Solve()
        {
            foreach (var (value, id) in _inputs)
            {
                _nodes[id].Values.Add(value);
            }
            
            var queue = new Queue<int>();
            var (startId, _) = _nodes.Single(x => x.Value.Values.Count == 2);
            queue.Enqueue(startId);

            while (queue.Count > 0)
            {
                var id = queue.Dequeue();
                var node = _nodes[id];
                var (lo, hi) = (node.Values.Min(), node.Values.Max());
               
                if (node.Lo.Type == DestinationType.Bot)
                {
                    var loNode = _nodes[node.Lo.Id];
                    loNode.Values.Add(lo);
                    if (loNode.Values.Count == 2)
                        queue.Enqueue(node.Lo.Id);
                }
                else
                {
                    _outputs[node.Lo.Id] = lo;
                }
                
                if (node.Hi.Type == DestinationType.Bot)
                {
                    var hiNode = _nodes[node.Hi.Id];
                    hiNode.Values.Add(hi);
                    if (hiNode.Values.Count == 2)
                        queue.Enqueue(node.Hi.Id);
                }
                else
                {
                    _outputs[node.Hi.Id] = hi;
                }
            }
        }

        private static DestinationType ParseDestinationType(string type) => type switch
        {
            "bot" => DestinationType.Bot,
            "output" => DestinationType.Output,
            _ => throw new ArgumentException()
        };
    }

    internal enum DestinationType
    {
        Bot,
        Output
    }

    internal class Node
    {
        public Node(Destination lo, Destination hi)
        {
            Lo = lo;
            Hi = hi;
            Values = new List<int>();
        }

        public IList<int> Values { get; }
        public Destination Lo { get; }
        public Destination Hi { get; }
    }
    
    internal class Destination
    {
        public DestinationType Type { get; set; }
        public int Id { get; set; }
    }
}