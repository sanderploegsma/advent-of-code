using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    internal class Solution
    {
        private const int NumberOfIterations = 6;

        private readonly IDictionary<Point2D, bool> _initialState;

        public Solution(IReadOnlyList<string> input)
        {
            var height = input.Count;
            var width = input[0].Length;
            _initialState = new Dictionary<Point2D, bool>();

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                _initialState[new Point2D(x, y)] = input[y][x] == '#';
            }
        }

        public int PartOne()
        {
            var initial = _initialState.ToDictionary(x => (IPoint) new Point3D(x.Key.X, x.Key.Y, 0), x => x.Value);
            var state = Run(initial);
            return state.Count(x => x.Value);
        }

        public int PartTwo()
        {
            var initial = _initialState.ToDictionary(x => (IPoint) new Point4D(x.Key.X, x.Key.Y, 0, 0), x => x.Value);
            var state = Run(initial);
            return state.Count(x => x.Value);
        }

        private static IDictionary<IPoint, bool> Run(IDictionary<IPoint, bool> initial)
        {
            var state = initial;

            for (var i = 0; i < NumberOfIterations; i++)
            {
                foreach (var neighbour in state.Keys.SelectMany(x => x.Neighbours).Distinct().ToList())
                {
                    if (!state.ContainsKey(neighbour))
                        state[neighbour] = false;
                }

                var newState = new Dictionary<IPoint, bool>();
                foreach (var (point, isActive) in state)
                {
                    var activeNeighbours = point.Neighbours.Where(state.ContainsKey).Count(x => state[x]);
                    var willBeActive =
                        isActive ? activeNeighbours == 2 || activeNeighbours == 3 : activeNeighbours == 3;
                    newState[point] = willBeActive;
                }

                state = newState;
            }

            return state;
        }
    }
}