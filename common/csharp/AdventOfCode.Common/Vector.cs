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
            X == other.X && 
            Y == other.Y;
        
        public override bool Equals(object? obj) => obj is IntVector vector && Equals(vector);
        
        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}