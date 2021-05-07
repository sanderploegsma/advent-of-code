using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Day01
{
    internal class Solution
    {
        private readonly IReadOnlyCollection<(Rotation, int Steps)> _instructions;

        public Solution(string input)
        {
            _instructions = input.Split(", ").Select(ParseInstruction).ToList();
        }

        public int PartOne()
        {
            var finalPosition = Traverse().Last();
            return finalPosition.ManhattanDistance;
        }

        public int PartTwo()
        {
            var visited = new HashSet<IntVector>();

            foreach (var position in Traverse())
            {
                if (!visited.Add(position))
                {
                    return position.ManhattanDistance;
                }
            }

            throw new InvalidOperationException();
        }

        private IEnumerable<IntVector> Traverse()
        {
            var position = new IntVector(0, 0);
            var direction = Direction.North;

            foreach (var (rotation, steps) in _instructions)
            {
                direction = direction.Turn(rotation);

                for (var i = 0; i < steps; i++)
                {
                    position += direction.Move();
                    yield return position;
                }
            }
        }

        private static (Rotation, int Steps) ParseInstruction(string instruction)
        {
            var rotation = instruction.StartsWith('R') ? Rotation.Right : Rotation.Left;
            var steps = int.Parse(instruction[1..]);
            return (rotation, steps);
        }
    }
}