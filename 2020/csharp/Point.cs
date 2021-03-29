using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public interface IPoint
    {
        IEnumerable<IPoint> Neighbours { get; }
    }

    public class Point2D : IPoint
    {
        private readonly Lazy<IEnumerable<IPoint>> _neighbours;

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
            _neighbours = new Lazy<IEnumerable<IPoint>>(GetNeighbours);
        }

        public int X { get; }
        public int Y { get; }

        public IEnumerable<IPoint> Neighbours => _neighbours.Value;

        public bool Equals(Point2D other) => X == other.X && Y == other.Y;

        public override bool Equals(object? obj) => obj is Point2D other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        private IEnumerable<IPoint> GetNeighbours()
        {
            for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                    continue;

                yield return new Point2D(X + dx, Y + dy);
            }
        }
    }

    public class Point3D : IPoint
    {
        private readonly Lazy<IEnumerable<IPoint>> _neighbours;

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            _neighbours = new Lazy<IEnumerable<IPoint>>(GetNeighbours);
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public IEnumerable<IPoint> Neighbours => _neighbours.Value;

        public bool Equals(Point3D other) => X == other.X && Y == other.Y && Z == other.Z;

        public override bool Equals(object? obj) => obj is Point3D other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        private IEnumerable<IPoint> GetNeighbours()
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

    public class Point4D : IPoint
    {
        private readonly Lazy<IEnumerable<IPoint>> _neighbours;

        public Point4D(int a, int b, int c, int d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            _neighbours = new Lazy<IEnumerable<IPoint>>(GetNeighbours);
        }

        public int A { get; }
        public int B { get; }
        public int C { get; }
        public int D { get; }

        public IEnumerable<IPoint> Neighbours => _neighbours.Value;

        public bool Equals(Point4D other) => A == other.A && B == other.B && C == other.C && D == other.D;

        public override bool Equals(object? obj) => obj is Point4D other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(A, B, C);

        private IEnumerable<IPoint> GetNeighbours()
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