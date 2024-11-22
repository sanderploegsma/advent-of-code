using AdventOfCode.Common;
using FsCheck;
using FsCheck.Xunit;
using System;
using Xunit;

namespace AdventOfCode.Common.Test
{
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
