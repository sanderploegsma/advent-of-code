using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2020
{
    public static class Matrix
    {
        public enum Corner
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
        }

        public enum Edge
        {
            Top,
            Bottom,
            Left,
            Right
        }
        
        public static T[,] Create<T>(T[][] data)
        {
            var height = data.Length;
            var width = data.GroupBy(row => row.Length).Single().Key;

            var result = new T[width, height];
            for (var y = 0; y < height; ++y)
            for (var x = 0; x < width; ++x)
                result[x, y] = data[y][x];

            return result;
        }

        public static int Width<T>(this T[,] matrix) => matrix.GetLength(0);
        
        public static int Height<T>(this T[,] matrix) => matrix.GetLength(1);
        
        public static T[] GetColumn<T>(this T[,] matrix, int x) =>
            Enumerable.Range(0, matrix.Height())
                .Select(y => matrix[x, y])
                .ToArray();

        public static T[] GetRow<T>(this T[,] matrix, int y) =>
            Enumerable.Range(0, matrix.Width())
                .Select(x => matrix[x, y])
                .ToArray();

        public static T[] TopEdge<T>(this T[,] matrix) => matrix.GetRow(0);
        public static T[] BottomEdge<T>(this T[,] matrix) => matrix.GetRow(matrix.Height() - 1);
        public static T[] LeftEdge<T>(this T[,] matrix) => matrix.GetColumn(0);
        public static T[] RightEdge<T>(this T[,] matrix) => matrix.GetColumn(matrix.Width() - 1);

        public static T[] GetEdge<T>(this T[,] matrix, Edge edge) => edge switch
        {
            Edge.Top => matrix.TopEdge(),
            Edge.Bottom => matrix.BottomEdge(),
            Edge.Left => matrix.LeftEdge(),
            Edge.Right => matrix.RightEdge(),
            _ => throw new ArgumentOutOfRangeException(nameof(edge), edge, null)
        };

        public static IEnumerable<T[]> GetEdges<T>(this T[,] matrix) =>
            Enum.GetValues(typeof(Edge))
                .Cast<Edge>()
                .Select(matrix.GetEdge);

        public static T GetCorner<T>(this T[,] matrix, Corner corner) => corner switch
        {
            Corner.TopLeft => matrix.TopEdge().First(),
            Corner.TopRight => matrix.TopEdge().Last(),
            Corner.BottomLeft => matrix.BottomEdge().First(),
            Corner.BottomRight => matrix.BottomEdge().Last(),
            _ => throw new ArgumentOutOfRangeException(nameof(corner), corner, null)
        };
        
        public static IEnumerable<T> GetCorners<T>(this T[,] matrix) =>
            Enum.GetValues(typeof(Corner))
                .Cast<Corner>()
                .Select(matrix.GetCorner);

        public static T[,] RotateRight<T>(this T[,] matrix)
        {
            var height = matrix.GetLength(1);
            var width = matrix.GetLength(0);
            
            var result = new T[height, width];
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                result[height - 1 - y, x] = matrix[x, y];
            }

            return result;
        }

        public static T[,] FlipHorizontal<T>(this T[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            
            var result = new T[width, height];
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                result[x, y] = matrix[width - 1 - x, y];
            }

            return result;
        }
    }

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
        [InlineData(0, new [] {1, 2, 3})]
        [InlineData(1, new [] {4, 5, 6})]
        [InlineData(2, new [] {7, 8, 9})]
        public void GetRow(int row, int[] expected)
        {
            Assert.Equal(expected, TestMatrix.GetRow(row));
        }
        
        [Theory]
        [InlineData(0, new [] {1, 4, 7})]
        [InlineData(1, new [] {2, 5, 8})]
        [InlineData(2, new [] {3, 6, 9})]
        public void GetColumn(int column, int[] expected)
        {
            Assert.Equal(expected, TestMatrix.GetColumn(column));
        }

        [Theory]
        [InlineData(Matrix.Edge.Top, new[] {1, 2, 3})]
        [InlineData(Matrix.Edge.Bottom, new[] {7, 8, 9})]
        [InlineData(Matrix.Edge.Left, new[] {1, 4, 7})]
        [InlineData(Matrix.Edge.Right, new[] {3, 6, 9})]
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