using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AdventOfCode.Common.Test
{
    public class MatrixTest
    {
        // 1 2 3
        // 4 5 6
        // 7 8 9
        private static readonly int[,] TestMatrix = {
            {1, 4, 7},
            {2, 5, 8},
            {3, 6, 9},
        };

        [Fact]
        public void Create_2D_Matrix()
        {
            // 1 2 3
            // 4 5 6
            // 7 8 9
            var data = new[]
            {
                new[] {1, 2, 3},
                new[] {4, 5, 6},
                new[] {7, 8, 9},
            };

            Assert.Equal(TestMatrix, Matrix.Create(data));
        }

        [Fact]
        public void RotateRight()
        {
            // 7 4 1
            // 8 5 2
            // 9 6 3
            var expected = new int[3, 3];
            expected[0, 0] = 7;
            expected[1, 0] = 4;
            expected[2, 0] = 1;
            expected[0, 1] = 8;
            expected[1, 1] = 5;
            expected[2, 1] = 2;
            expected[0, 2] = 9;
            expected[1, 2] = 6;
            expected[2, 2] = 3;

            Assert.Equal(expected, TestMatrix.RotateRight());
        }

        [Fact]
        public void FlipHorizontal()
        {
            // 3 2 1
            // 6 5 4
            // 9 8 7
            var expected = new int[3, 3];
            expected[0, 0] = 3;
            expected[1, 0] = 2;
            expected[2, 0] = 1;
            expected[0, 1] = 6;
            expected[1, 1] = 5;
            expected[2, 1] = 4;
            expected[0, 2] = 9;
            expected[1, 2] = 8;
            expected[2, 2] = 7;

            Assert.Equal(expected, TestMatrix.FlipHorizontal());
        }

        [Fact]
        public void WidthAndHeight()
        {
            Assert.Equal(3, TestMatrix.Width());
            Assert.Equal(3, TestMatrix.Height());
        }

        [Theory]
        [InlineData(0, new[] { 1, 2, 3 })]
        [InlineData(1, new[] { 4, 5, 6 })]
        [InlineData(2, new[] { 7, 8, 9 })]
        public void GetRow(int row, int[] expected)
        {
            Assert.Equal(expected, TestMatrix.GetRow(row));
        }

        [Theory]
        [InlineData(0, new[] { 1, 4, 7 })]
        [InlineData(1, new[] { 2, 5, 8 })]
        [InlineData(2, new[] { 3, 6, 9 })]
        public void GetColumn(int column, int[] expected)
        {
            Assert.Equal(expected, TestMatrix.GetColumn(column));
        }

        [Theory]
        [InlineData(Matrix.Edge.Top, new[] { 1, 2, 3 })]
        [InlineData(Matrix.Edge.Bottom, new[] { 7, 8, 9 })]
        [InlineData(Matrix.Edge.Left, new[] { 1, 4, 7 })]
        [InlineData(Matrix.Edge.Right, new[] { 3, 6, 9 })]
        public void GetEdge(Matrix.Edge edge, int[] expected)
        {
            Assert.Equal(expected, TestMatrix.GetEdge(edge));
        }

        [Theory]
        [InlineData(Matrix.Corner.TopLeft, 1)]
        [InlineData(Matrix.Corner.TopRight, 3)]
        [InlineData(Matrix.Corner.BottomLeft, 7)]
        [InlineData(Matrix.Corner.BottomRight, 9)]
        public void GetCorner(Matrix.Corner corner, int expected)
        {
            Assert.Equal(expected, TestMatrix.GetCorner(corner));
        }
    }
}
