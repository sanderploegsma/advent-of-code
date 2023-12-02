using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day24
{
    internal class Solution
    {
        private const int NumberOfIterations = 100;
        private readonly IReadOnlyCollection<Tile> _tiles;
        private readonly ConwaySimulator _simulator;

        public Solution(IEnumerable<string> input)
        {
            _simulator = new ConwaySimulator(NumberOfIterations, NextTileState);

            _tiles = input
                .Select(ParseDirections)
                .Select(NavigateToTile)
                .ToList();
        }

        public int PartOne() => _tiles.GroupBy(x => x).Count(g => g.Count() % 2 == 1);

        public int PartTwo()
        {
            var initialState = _tiles
                .GroupBy(x => x)
                .ToDictionary(x => (IConwayCell) x.Key, x => x.Count() % 2 == 1);

            var state = _simulator.Simulate(initialState);
            return state.Count(x => x.Value);
        }

        private static bool NextTileState(bool isBlack, int blackNeighbours) => isBlack switch
        {
            true when blackNeighbours == 0 || blackNeighbours > 2 => false,
            false when blackNeighbours == 2 => true,
            _ => isBlack
        };

        private static IEnumerable<HexDirection> ParseDirections(string line)
        {
            while (line.Length > 0)
            {
                if (line.StartsWith("e"))
                {
                    yield return HexDirection.East;
                    line = line.Substring(1);
                    continue;
                }

                if (line.StartsWith("w"))
                {
                    yield return HexDirection.West;
                    line = line.Substring(1);
                    continue;
                }

                if (line.StartsWith("ne"))
                {
                    yield return HexDirection.NorthEast;
                    line = line.Substring(2);
                    continue;
                }

                if (line.StartsWith("se"))
                {
                    yield return HexDirection.SouthEast;
                    line = line.Substring(2);
                    continue;
                }

                if (line.StartsWith("nw"))
                {
                    yield return HexDirection.NorthWest;
                    line = line.Substring(2);
                    continue;
                }

                yield return HexDirection.SouthWest;
                line = line.Substring(2);
            }
        }

        private static Tile NavigateToTile(IEnumerable<HexDirection> directions) =>
            directions.Aggregate(new Tile(0, 0), (tile, direction) => direction.Move(tile));
    }

    internal enum HexDirection
    {
        East,
        West,
        SouthEast,
        SouthWest,
        NorthWest,
        NorthEast,
    }

    internal class Tile : IConwayCell
    {
        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public IEnumerable<IConwayCell> Neighbours
        {
            get
            {
                yield return HexDirection.East.Move(this);
                yield return HexDirection.West.Move(this);
                yield return HexDirection.SouthEast.Move(this);
                yield return HexDirection.SouthWest.Move(this);
                yield return HexDirection.NorthWest.Move(this);
                yield return HexDirection.NorthEast.Move(this);
            }
        }

        public override bool Equals(object? obj) =>
            obj is Tile tile && tile.X == X && tile.Y == Y;

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }

    internal static class DirectionExtensions
    {
        public static Tile Move(this HexDirection direction, Tile from) => direction switch
        {
            HexDirection.East => new Tile(from.X + 1, from.Y + 1),
            HexDirection.West => new Tile(from.X - 1, from.Y - 1),
            HexDirection.SouthEast => new Tile(from.X + 1, from.Y),
            HexDirection.NorthEast => new Tile(from.X, from.Y + 1),
            HexDirection.NorthWest => new Tile(from.X - 1, from.Y),
            HexDirection.SouthWest => new Tile(from.X, from.Y - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}