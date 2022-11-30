using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common
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
}