using System;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace AdventOfCode2020
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

    public class IntVectorTest
    {
        [Fact]
        public void ManhattanDistance_ShouldBeZero_ForOrigin()
        {
            Assert.Equal(0, new IntVector(0, 0).ManhattanDistance);
        }
        
        [Property]
        public Property ManhattanDistance_ShouldBeGreaterThanZero_ForAllVectorsExceptOrigin(int x, int y)
        {
            Func<bool> property = () => new IntVector(x, y).ManhattanDistance > 0;
            return property.When(x != 0 || y != 0);
        }

        [Property]
        public Property MultiplyingOriginByAConstant_ShouldReturnOrigin(int c)
        {
            return (IntVector.Origin * c).Equals(IntVector.Origin).ToProperty();
        }

        [Property]
        public Property SubtractingVectorFromItself_ShouldReturnOrigin(int x, int y)
        {
            var vector = new IntVector(x, y);
            return (vector - vector).Equals(IntVector.Origin).ToProperty();
        }

        [Property]
        public Property AddingVectorToItself_ShouldEqualMultiplyingByTwo(int x, int y)
        {
            var vector = new IntVector(x, y);
            return (vector + vector).Equals(vector * 2).ToProperty();
        }

        [Property]
        public Property AddingTwoVectors_ShouldNotCareAboutOperandOrder(int x1, int y1, int x2, int y2)
        {
            var a = new IntVector(x1, y1);
            var b = new IntVector(x2, y2);
            return (a + b).Equals(b + a).ToProperty();
        }
        
        [Property]
        public Property RotatingBy360Degrees_ShouldEqualItself(int x, int y)
        {
            var vector = new IntVector(x, y);
            return vector.Rotate(360).Equals(vector).ToProperty();
        }

        [Property]
        public Property RotatingBy180Degrees_ShouldEqualMultiplyingByMinusOne(int x, int y)
        {
            var vector = new IntVector(x, y);
            return vector.Rotate(180).Equals(vector * -1).ToProperty();
        }

        [Property]
        public Property AddingInverses_ShouldReturnOrigin(int x, int y)
        {
            var vector = new IntVector(x, y);
            return (vector.Rotate(180) + vector).Equals(IntVector.Origin).ToProperty();
        }
    }
}