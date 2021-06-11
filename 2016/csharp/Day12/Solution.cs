using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Day12
{
    public class Solution
    {
        private readonly IReadOnlyList<IInstruction> _instructions;

        public Solution(IEnumerable<string> input)
        {
            _instructions = input.Select(ParseInstruction).ToList();
        }

        public int PartOne()
        {
            var registers = new Dictionary<Register, int>
            {
                [Register.A] = 0,
                [Register.B] = 0,
                [Register.C] = 0,
                [Register.D] = 0
            };

            Run(registers);

            return registers[Register.A];
        }

        public int PartTwo()
        {
            var registers = new Dictionary<Register, int>
            {
                [Register.A] = 0,
                [Register.B] = 0,
                [Register.C] = 1,
                [Register.D] = 0
            };

            Run(registers);

            return registers[Register.A];
        }

        private void Run(IDictionary<Register, int> registers)
        {
            var idx = 0;

            while (idx < _instructions.Count)
            {
                switch (_instructions[idx])
                {
                    case CopyLiteralValue cpy1:
                        registers[cpy1.Destination] = cpy1.Literal;
                        break;
                    case CopyRegisterValue cpy2:
                        registers[cpy2.Destination] = registers[cpy2.Source];
                        break;
                    case IncrementRegisterValue inc:
                        registers[inc.Destination]++;
                        break;
                    case DecrementRegisterValue dec:
                        registers[dec.Destination]--;
                        break;
                    case JumpIfValueNotZero jnz1 when jnz1.Source != 0:
                        idx += jnz1.Offset;
                        continue;
                    case JumpIfRegisterNotZero jnz2 when registers[jnz2.Source] != 0:
                        idx += jnz2.Offset;
                        continue;
                }

                idx++;
            }
        }

        private static IInstruction ParseInstruction(string instruction)
        {
            var segments = instruction.Split(" ");

            return segments[0] switch
            {
                "cpy" when int.TryParse(segments[1], out var value) => new CopyLiteralValue(value,
                    ParseRegister(segments[2])),
                "cpy" => new CopyRegisterValue(ParseRegister(segments[1]), ParseRegister(segments[2])),
                "inc" => new IncrementRegisterValue(ParseRegister(segments[1])),
                "dec" => new DecrementRegisterValue(ParseRegister(segments[1])),
                "jnz" when int.TryParse(segments[1], out var value) => new JumpIfValueNotZero(value,
                    int.Parse(segments[2])),
                "jnz" => new JumpIfRegisterNotZero(ParseRegister(segments[1]), int.Parse(segments[2])),
                _ => throw new ArgumentException($"Invalid instruction '{instruction}'")
            };
        }

        private static Register ParseRegister(string register) => register switch
        {
            "a" => Register.A,
            "b" => Register.B,
            "c" => Register.C,
            "d" => Register.D,
            _ => throw new ArgumentException($"Invalid register '{register}'")
        };

        private enum Register
        {
            A,
            B,
            C,
            D
        }

        private interface IInstruction
        {
        }

        private class CopyLiteralValue : IInstruction
        {
            public CopyLiteralValue(int literal, Register destination)
            {
                Literal = literal;
                Destination = destination;
            }

            public int Literal { get; }
            public Register Destination { get; }
        }

        private class CopyRegisterValue : IInstruction
        {
            public CopyRegisterValue(Register source, Register destination)
            {
                Source = source;
                Destination = destination;
            }

            public Register Source { get; }
            public Register Destination { get; }
        }

        private class IncrementRegisterValue : IInstruction
        {
            public IncrementRegisterValue(Register destination)
            {
                Destination = destination;
            }

            public Register Destination { get; }
        }

        private class DecrementRegisterValue : IInstruction
        {
            public DecrementRegisterValue(Register destination)
            {
                Destination = destination;
            }

            public Register Destination { get; }
        }

        private class JumpIfValueNotZero : IInstruction
        {
            public JumpIfValueNotZero(int source, int offset)
            {
                Source = source;
                Offset = offset;
            }

            public int Source { get; }
            public int Offset { get; }
        }

        private class JumpIfRegisterNotZero : IInstruction
        {
            public JumpIfRegisterNotZero(Register source, int offset)
            {
                Source = source;
                Offset = offset;
            }

            public Register Source { get; }
            public int Offset { get; }
        }
    }
}