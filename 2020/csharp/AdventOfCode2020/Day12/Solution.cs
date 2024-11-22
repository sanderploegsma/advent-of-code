using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day12
{
    internal class Solution
    {
        private static readonly IntVector HStep = new IntVector(1, 0);
        private static readonly IntVector VStep = new IntVector(0, 1);

        private readonly IReadOnlyCollection<(char, int)> _instructions;

        public Solution(IEnumerable<string> input)
        {
            _instructions = input.Select(line => (line[0], int.Parse(line.Substring(1)))).ToList();
        }

        public int PartOne()
        {
            var ship = _instructions.Aggregate(ShipState.Initial, (state, instruction) =>
            {
                var (action, value) = instruction;

                switch (action)
                {
                    case 'N':
                    case 'F' when state.Direction == Direction.North:
                        return state.WithPosition(state.Position + VStep * value);
                    case 'E':
                    case 'F' when state.Direction == Direction.East:
                        return state.WithPosition(state.Position + HStep * value);
                    case 'S':
                    case 'F' when state.Direction == Direction.South:
                        return state.WithPosition(state.Position - VStep * value);
                    case 'W':
                    case 'F' when state.Direction == Direction.West:
                        return state.WithPosition(state.Position - HStep * value);
                    case 'L':
                        return state.WithDirection(state.Direction.TurnLeft(times: value / 90));
                    case 'R':
                        return state.WithDirection(state.Direction.TurnRight(times: value / 90));
                    default:
                        throw new ArgumentException();
                }
            });

            return ship.Position.ManhattanDistance;
        }

        public int PartTwo()
        {
            var ship = _instructions.Aggregate(ShipWaypointState.Initial, (state, instruction) =>
            {
                var (action, value) = instruction;

                return action switch
                {
                    'F' => state.WithShipPosition(state.ShipPosition + state.WaypointPosition * value),
                    'N' => state.WithWaypointPosition(state.WaypointPosition + VStep * value),
                    'E' => state.WithWaypointPosition(state.WaypointPosition + HStep * value),
                    'S' => state.WithWaypointPosition(state.WaypointPosition - VStep * value),
                    'W' => state.WithWaypointPosition(state.WaypointPosition - HStep * value),
                    'L' => state.WithWaypointPosition(state.WaypointPosition.Rotate(value)),
                    'R' => state.WithWaypointPosition(state.WaypointPosition.Rotate(-value)),
                    _ => throw new ArgumentException()
                };
            });

            return ship.ShipPosition.ManhattanDistance;
        }

        private class ShipState
        {
            public ShipState(IntVector position, Direction direction)
            {
                Position = position;
                Direction = direction;
            }

            public IntVector Position { get; }
            public Direction Direction { get; }

            public ShipState WithPosition(IntVector position) => new ShipState(position, Direction);
            public ShipState WithDirection(Direction direction) => new ShipState(Position, direction);

            public static ShipState Initial => new ShipState(new IntVector(0, 0), Direction.East);
        }

        private class ShipWaypointState
        {
            public ShipWaypointState(IntVector shipPosition, IntVector waypointPosition)
            {
                ShipPosition = shipPosition;
                WaypointPosition = waypointPosition;
            }

            public IntVector ShipPosition { get; }
            public IntVector WaypointPosition { get; }

            public ShipWaypointState WithShipPosition(IntVector position) =>
                new ShipWaypointState(position, WaypointPosition);

            public ShipWaypointState WithWaypointPosition(IntVector position) =>
                new ShipWaypointState(ShipPosition, position);

            public static ShipWaypointState Initial => new ShipWaypointState(new IntVector(0, 0), new IntVector(10, 1));
        }
    }
}
