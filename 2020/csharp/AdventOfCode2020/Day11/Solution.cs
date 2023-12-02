using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day11
{
    internal class Solution
    {
        private const char FloorTile = '.';
        private const char EmptySeatTile = 'L';
        private const char FilledSeatTile = '#';

        private readonly IDictionary<(int X, int Y), bool> _cells;
        private readonly int _width;
        private readonly int _height;
        
        public Solution(IEnumerable<string> input)
        {
            _cells = ParseCells(input);
            _width = _cells.Keys.Select(key => key.X).Max();
            _height = _cells.Keys.Select(key => key.Y).Max();
        }

        public int PartOne()
        {
            var state = _cells;
            var newState = new Dictionary<(int X, int Y), bool>();

            do
            {
                newState.Clear();
                foreach (var (cell, isActive) in state)
                {
                    var activeNeighbours = Neighbours(cell.X, cell.Y)
                        .Where(key => state.ContainsKey(key))
                        .Count(key => state[key]);

                    switch (isActive)
                    {
                        case true when activeNeighbours >= 4:
                            newState[cell] = false;
                            break;
                        case false when activeNeighbours == 0:
                            newState[cell] = true;
                            break;
                    }
                }

                foreach (var (cell, isActive) in newState)
                {
                    state[cell] = isActive;
                }
            } while (newState.Count > 0);
            
            return state.Count(x => x.Value);
        }

        public int PartTwo()
        {
            var state = _cells;
            var newState = new Dictionary<(int X, int Y), bool>();

            do
            {
                newState.Clear();
                foreach (var (cell, isActive) in state)
                {
                    var activeNeighbours = LineOfSight(cell.X, cell.Y)
                        .SelectMany(line => line.SkipWhile(key => !state.ContainsKey(key)).Take(1))
                        .Count(key => state[key]);

                    switch (isActive)
                    {
                        case true when activeNeighbours >= 5:
                            newState[cell] = false;
                            break;
                        case false when activeNeighbours == 0:
                            newState[cell] = true;
                            break;
                    }
                }

                foreach (var (cell, isActive) in newState)
                {
                    state[cell] = isActive;
                }
            } while (newState.Count > 0);
            
            return state.Count(x => x.Value);
        }

        private static IEnumerable<(int X, int Y)> Neighbours(int x, int y)
        {
            for (var i = x - 1; i <= x + 1; i++)
            for (var j = y - 1; j <= y + 1; j++)
                if (i != x || j != y)
                    yield return (i, j);
        }

        private IEnumerable<IEnumerable<(int X, int Y)>> LineOfSight(int x, int y)
        {
            var steps = Enumerable.Range(1, Math.Max(_width, _height)).ToList();
            yield return steps.Select(d => (x - d, y - d));
            yield return steps.Select(d => (x, y - d));
            yield return steps.Select(d => (x + d, y - d));
            yield return steps.Select(d => (x + d, y));
            yield return steps.Select(d => (x + d, y + d));
            yield return steps.Select(d => (x, y + d));
            yield return steps.Select(d => (x - d, y + d));
            yield return steps.Select(d => (x - d, y));
        }

        private static IDictionary<(int X, int Y), bool> ParseCells(IEnumerable<string> input)
        {
            return input
                .Select((line, y) => line.Select((cell, x) => ((x, y), cell)))
                .SelectMany(x => x)
                .Where(x => x.cell != FloorTile)
                .ToDictionary(x => x.Item1, x => x.cell == FilledSeatTile);
        }
    }
}