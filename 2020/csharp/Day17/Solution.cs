using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    internal class Solution
    {
        private const int NumberOfIterations = 6;

        private readonly IDictionary<(int X, int Y), bool> _initialState;
        private readonly ConwaySimulator _simulator;

        public Solution(IReadOnlyList<string> input)
        {
            _simulator = new ConwaySimulator(NumberOfIterations, NextCellState);
            
            var height = input.Count;
            var width = input[0].Length;
            _initialState = new Dictionary<(int, int), bool>();

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                _initialState[(x, y)] = input[y][x] == '#';
            }
        }

        public int PartOne()
        {
            var initial = _initialState.ToDictionary(x => (IConwayCell) new Point3D(x.Key.X, x.Key.Y, 0), x => x.Value);
            var state = _simulator.Simulate(initial);
            return state.Count(x => x.Value);
        }

        public int PartTwo()
        {
            var initial = _initialState.ToDictionary(x => (IConwayCell) new Point4D(x.Key.X, x.Key.Y, 0, 0), x => x.Value);
            var state = _simulator.Simulate(initial);
            return state.Count(x => x.Value);
        }
        
        private static bool NextCellState(bool isActive, int activeNeighbours) => 
            isActive ? activeNeighbours == 2 || activeNeighbours == 3 : activeNeighbours == 3;
    }
    
    internal class Point3D : IConwayCell
    {
        private readonly Lazy<IEnumerable<IConwayCell>> _neighbours;

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            _neighbours = new Lazy<IEnumerable<IConwayCell>>(GetNeighbours);
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public IEnumerable<IConwayCell> Neighbours => _neighbours.Value;

        public bool Equals(Point3D other) => X == other.X && Y == other.Y && Z == other.Z;

        public override bool Equals(object? obj) => obj is Point3D other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        private IEnumerable<IConwayCell> GetNeighbours()
        {
            for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
            for (var dz = -1; dz <= 1; dz++)
            {
                if (dx == 0 && dy == 0 && dz == 0)
                    continue;

                yield return new Point3D(X + dx, Y + dy, Z + dz);
            }
        }
    }

    internal class Point4D : IConwayCell
    {
        private readonly Lazy<IEnumerable<IConwayCell>> _neighbours;

        public Point4D(int a, int b, int c, int d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            _neighbours = new Lazy<IEnumerable<IConwayCell>>(GetNeighbours);
        }

        public int A { get; }
        public int B { get; }
        public int C { get; }
        public int D { get; }

        public IEnumerable<IConwayCell> Neighbours => _neighbours.Value;

        public bool Equals(Point4D other) => A == other.A && B == other.B && C == other.C && D == other.D;

        public override bool Equals(object? obj) => obj is Point4D other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(A, B, C);

        private IEnumerable<IConwayCell> GetNeighbours()
        {
            for (var da = -1; da <= 1; da++)
            for (var db = -1; db <= 1; db++)
            for (var dc = -1; dc <= 1; dc++)
            for (var dd = -1; dd <= 1; dd++)
            {
                if (da == 0 && db == 0 && dc == 0 && dd == 0)
                    continue;

                yield return new Point4D(A + da, B + db, C + dc, D + dd);
            }
        }
    }
}