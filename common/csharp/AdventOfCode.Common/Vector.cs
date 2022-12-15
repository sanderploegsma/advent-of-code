using System;

namespace AdventOfCode.Common
{
    public class IntVector
    {
        public IntVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public int ManhattanDistance => Math.Abs(X) + Math.Abs(Y);

        public int ManhattanDistanceTo(IntVector other) => (this - other).ManhattanDistance;

        public IntVector Rotate(int degrees)
        {
            var radians = Math.PI * degrees / 180.0;
            var x = X * Math.Cos(radians) - Y * Math.Sin(radians);
            var y = Y * Math.Cos(radians) + X * Math.Sin(radians);
            return new IntVector((int)Math.Round(x), (int)Math.Round(y));
        }

        public IntVector Move(Direction direction) => direction switch
        {
            Direction.North => new IntVector(X, Y + 1),
            Direction.East => new IntVector(X + 1, Y),
            Direction.South => new IntVector(X, Y - 1),
            Direction.West => new IntVector(X - 1, Y),
            _ => throw new InvalidOperationException(),
        };

        public static IntVector Origin => new IntVector(0, 0);

        public static IntVector operator +(IntVector a, IntVector b) => new IntVector(a.X + b.X, a.Y + b.Y);

        public static IntVector operator -(IntVector a, IntVector b) => new IntVector(a.X - b.X, a.Y - b.Y);

        public static IntVector operator *(IntVector a, int c) => new IntVector(a.X * c, a.Y * c);

        public bool Equals(IntVector other) =>
            X == other.X &&
            Y == other.Y;

        public override bool Equals(object? obj) => obj is IntVector vector && Equals(vector);

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}