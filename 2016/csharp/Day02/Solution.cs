using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Day02
{
    internal class Solution
    {
        private static readonly IDictionary<IntVector, char> Keypad1 = new Dictionary<IntVector, char>
        {
            [new IntVector(-1, 1)] = '1',
            [new IntVector(0, 1)] = '2',
            [new IntVector(1, 1)] = '3',
            [new IntVector(-1, 0)] = '4',
            [new IntVector(0, 0)] = '5',
            [new IntVector(1, 0)] = '6',
            [new IntVector(-1, -1)] = '7',
            [new IntVector(0, -1)] = '8',
            [new IntVector(1, -1)] = '9',
        };

        private static readonly IDictionary<IntVector, char> Keypad2 = new Dictionary<IntVector, char>
        {
            [new IntVector(0, 2)] = '1',
            [new IntVector(-1, 1)] = '2',
            [new IntVector(0, 1)] = '3',
            [new IntVector(1, 1)] = '4',
            [new IntVector(-2, 0)] = '5',
            [new IntVector(-1, 0)] = '6',
            [new IntVector(0, 0)] = '7',
            [new IntVector(1, 0)] = '8',
            [new IntVector(2, 0)] = '9',
            [new IntVector(-1, -1)] = 'A',
            [new IntVector(0, -1)] = 'B',
            [new IntVector(1, -1)] = 'C',
            [new IntVector(0, -2)] = 'D',
        };

        private readonly IReadOnlyCollection<IEnumerable<Direction>> _instructions;

        public Solution(IEnumerable<string> input)
        {
            _instructions = input.Select(ParseInstructions).ToList();
        }

        public string PartOne() => FindCode(Keypad1, new IntVector(0, 0));

        public string PartTwo() => FindCode(Keypad2, new IntVector(-2, 0));

        private string FindCode(IDictionary<IntVector, char> keypad, IntVector start)
        {
            var position = start;
            var code = "";

            foreach (var instruction in _instructions)
            {
                foreach (var direction in instruction)
                {
                    if (keypad.ContainsKey(position + direction.Move()))
                    {
                        position += direction.Move();
                    }
                }

                code += keypad[position];
            }

            return code;
        }

        private static IEnumerable<Direction> ParseInstructions(string instructions) => instructions.Select(
            instruction => instruction switch
            {
                'U' => Direction.North,
                'R' => Direction.East,
                'D' => Direction.South,
                'L' => Direction.West,
                _ => throw new ArgumentException($"Unknown instruction {instruction}")
            });
    }
}
