using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace AdventOfCode2020.Day08
{
    internal class Solution
    {
        private readonly (Instruction Instruction, int Value)[] _instructions;

        public Solution(IEnumerable<string> input)
        {
            _instructions = input.Select(Parse).ToArray();
        }

        public int PartOne()
        {
            RunToCompletion(_instructions, out var accumulatedValue);
            return accumulatedValue;
        }

        public int PartTwo()
        {
            for (var i = 0; i < _instructions.Length; i++)
            {
                var (instruction, value) = _instructions[i];

                if (!ShouldFlip(instruction, out var newInstruction))
                    continue;

                var copy = _instructions.ToArray();
                copy[i] = (newInstruction, value);

                if (RunToCompletion(copy, out var accumulatedValue))
                    return accumulatedValue;
            }

            return -1;
        }

        private static bool ShouldFlip(Instruction instruction, out Instruction newInstruction)
        {
            switch (instruction)
            {
                case Instruction.Jump:
                    newInstruction = Instruction.NoOperation;
                    return true;
                case Instruction.NoOperation:
                    newInstruction = Instruction.Jump;
                    return true;
                default:
                    newInstruction = instruction;
                    return false;
            }
        }

        private static bool RunToCompletion(IReadOnlyList<(Instruction, int)> instructions, out int accumulatedValue)
        {
            accumulatedValue = 0;

            var i = 0;
            var seen = new HashSet<int> {i};

            do
            {
                var (instruction, value) = instructions[i];
                switch (instruction)
                {
                    case Instruction.Accumulate:
                        accumulatedValue += value;
                        i++;
                        break;
                    case Instruction.Jump:
                        i += value;
                        break;
                    case Instruction.NoOperation:
                        i++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            } while (i < instructions.Count && seen.Add(i));

            return i == instructions.Count;
        }

        private static (Instruction, int) Parse(string line)
        {
            var segments = line.Split(" ");
            return (ParseInstruction(segments[0]), int.Parse(segments[1]));
        }

        private static Instruction ParseInstruction(string instruction) => instruction switch
        {
            "acc" => Instruction.Accumulate,
            "jmp" => Instruction.Jump,
            "nop" => Instruction.NoOperation,
            _ => throw new ArgumentException()
        };
    }

    internal enum Instruction
    {
        Accumulate,
        Jump,
        NoOperation
    }
}