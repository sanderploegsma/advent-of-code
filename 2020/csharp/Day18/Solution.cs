using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day18
{
    internal class Solution
    {
        private const string OperatorAdd = "+";
        private const string OperatorMultiply = "*";

        private static readonly Func<long, long, long> OperationAdd = (lhs, rhs) => lhs + rhs;
        private static readonly Func<long, long, long> OperationMultiply = (lhs, rhs) => lhs * rhs;
        
        private readonly IReadOnlyCollection<string> _input;
        
        public Solution(IReadOnlyCollection<string> input)
        {
            _input = input;
        }

        public long PartOne()
        {
            var operators = new Dictionary<string, Operator>
            {
                [OperatorAdd] = new Operator(1, OperationAdd),
                [OperatorMultiply] = new Operator(1, OperationMultiply),
            };

            var solver = new EquationSolver(operators);

            return _input.Sum(solver.Solve);
        }

        public long PartTwo()
        {
            var operators = new Dictionary<string, Operator>
            {
                [OperatorAdd] = new Operator(2, OperationAdd),
                [OperatorMultiply] = new Operator(1, OperationMultiply),
            };

            var solver = new EquationSolver(operators);

            return _input.Sum(solver.Solve);
        }
    }

    internal class EquationSolver
    {
        private const string EquationGroupPattern = @"\([\s\+\*\d]+\)";
        
        private readonly IDictionary<string, Operator> _operators;

        public EquationSolver(IDictionary<string, Operator> operators)
        {
            _operators = operators;
        }

        public long Solve(string equation)
        {
            while (Regex.IsMatch(equation, EquationGroupPattern))
            {
                equation = Regex.Replace(equation, EquationGroupPattern, match =>
                {
                    var group = match.Value.TrimStart('(').TrimEnd(')');
                    return SolveGroup(group).ToString();
                });
            }

            return SolveGroup(equation);
        }

        private long SolveGroup(string group)
        {
            var queue = new Queue<string>(group.Split(" "));
            var lhs = long.Parse(queue.Dequeue());
            return SolveGroup(queue, lhs, 0);
        }

        private long SolveGroup(Queue<string> tokens, long lhs, int minPrecedence)
        {
            while (tokens.TryPeek(out var nextOperator) && _operators[nextOperator].Precedence >= minPrecedence)
            {
                var @operator = _operators[tokens.Dequeue()];
                var rhs = long.Parse(tokens.Dequeue());
                while (tokens.TryPeek(out nextOperator) && _operators[nextOperator].Precedence > @operator.Precedence)
                {
                    rhs = SolveGroup(tokens, rhs, _operators[nextOperator].Precedence);
                }

                lhs = @operator.Operation.Invoke(lhs, rhs);
            }

            return lhs;
        }
    }

    internal class Operator
    {
        public Operator(int precedence, Func<long, long, long> operation)
        {
            Precedence = precedence;
            Operation = operation;
        }

        public int Precedence { get; }
        public Func<long, long, long> Operation { get; }
    }
}