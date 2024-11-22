using System;

namespace AdventOfCode2016
{
    public enum Rotation
    {
        Left,
        Right
    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public static class DirectionExtensions
    {
        public static IntVector Move(this Direction direction) => direction switch
        {
            Direction.North => new IntVector(0, 1),
            Direction.East => new IntVector(1, 0),
            Direction.South => new IntVector(0, -1),
            Direction.West => new IntVector(-1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        public static Direction Turn(this Direction direction, Rotation rotation, int times = 1) => rotation switch
        {
            Rotation.Left => direction.TurnLeft(times),
            Rotation.Right => direction.TurnRight(times),
            _ => throw new ArgumentException()
        };

        public static Direction TurnLeft(this Direction direction, int times = 1) => direction.Turn(LeftTurns, times);

        public static Direction TurnRight(this Direction direction, int times = 1) => direction.Turn(RightTurns, times);

        private static Direction Turn(this Direction direction, Direction[] turns, int times = 1)
        {
            var id = (Array.IndexOf(turns, direction) + times).Mod(turns.Length);
            return turns[id];
        }

        private static readonly Direction[] LeftTurns =
            {Direction.North, Direction.West, Direction.South, Direction.East};

        private static readonly Direction[] RightTurns =
            {Direction.North, Direction.East, Direction.South, Direction.West};

        private static int Mod(this int n, int m) => (n % m + m) % m;
    }

    public class IntVector : IEquatable<IntVector>
    {
        public IntVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public int ManhattanDistance => Math.Abs(X) + Math.Abs(Y);

        public IntVector Rotate(int degrees)
        {
            var radians = Math.PI * degrees / 180.0;
            var x = X * Math.Cos(radians) - Y * Math.Sin(radians);
            var y = Y * Math.Cos(radians) + X * Math.Sin(radians);
            return new IntVector((int) Math.Round(x), (int) Math.Round(y));
        }

        public static IntVector Origin => new IntVector(0, 0);

        public static IntVector operator +(IntVector a, IntVector b) => new IntVector(a.X + b.X, a.Y + b.Y);

        public static IntVector operator -(IntVector a, IntVector b) => new IntVector(a.X - b.X, a.Y - b.Y);

        public static IntVector operator *(IntVector a, int c) => new IntVector(a.X * c, a.Y * c);

        public bool Equals(IntVector other) =>
            other != null &&
            X == other.X &&
            Y == other.Y;

        public override bool Equals(object? obj) => obj is IntVector vector && Equals(vector);

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}
