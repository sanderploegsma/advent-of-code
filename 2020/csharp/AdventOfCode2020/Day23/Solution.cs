using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day23
{
    internal class Solution
    {
        private readonly IReadOnlyCollection<int> _cups;
        
        public Solution(string input)
        {
            _cups = input.Select(c => (int) char.GetNumericValue(c)).ToList();
        }

        public string PartOne()
        {
            var game = new CrabCups(_cups);
            var result = game.Play(100);
            return string.Concat(result.Skip(1).Select(x => x.ToString()));
        }

        public long PartTwo()
        {
            var cups = _cups.Concat(Enumerable.Range(1, 1_000_000).SkipWhile(n => n < 10));
            var game = new CrabCups(cups);
            var result = game.Play(10_000_000);
            return result.Skip(1).Take(2).Select(x => (long) x).Product();
        }
    }

    internal class CrabCups
    {
        private readonly IReadOnlyCollection<int> _cups;

        public CrabCups(IEnumerable<int> cups)
        {
            _cups = cups.ToArray();
        }

        public IEnumerable<int> Play(int moves)
        {
            var lowestCup = _cups.Min();
            var highestCup = _cups.Max();
            
            var pointers = new int[_cups.Count + 1];
            pointers[_cups.Last()] = _cups.First();
            foreach (var (left, right) in _cups.Pairwise())
            {
                pointers[left] = right;
            }

            var current = _cups.First();
            for (var i = 0; i < moves; i++)
            {
                // The crab picks up the three cups that are immediately clockwise of the current cup.
                var removed = new List<int> {pointers[current]};
                removed.Add(pointers[removed.Last()]);
                removed.Add(pointers[removed.Last()]);

                pointers[current] = pointers[removed.Last()];

                // The crab selects a destination cup: the cup with a label equal to the current cup's label minus one.
                // If this would select one of the cups that was just picked up, the crab will keep subtracting one
                // until it finds a cup that wasn't just picked up. If at any point in this process the value goes
                // below the lowest value on any cup's label, it wraps around to the highest value on any cup's label
                // instead.
                var destination = current;
                do
                {
                    destination = destination == lowestCup ? highestCup : destination - 1;
                } while (removed.Contains(destination));

                // The crab places the cups it just picked up so that they are immediately clockwise of the destination
                // cup. They keep the same order as when they were picked up.
                var nextToDestination = pointers[destination];
                pointers[destination] = removed.First();
                pointers[removed.Last()] = nextToDestination;
                current = pointers[current];
            }

            var cup = lowestCup;
            do
            {
                yield return cup;
                cup = pointers[cup];
            } while (cup != lowestCup);
        }
    }
}