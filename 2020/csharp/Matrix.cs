using System.Linq;
using Xunit;

namespace AdventOfCode2020
{
    public static class Matrix
    {
        public static T[,] Create<T>(T[][] data)
        {
            var firstDim = data.Length;
            var secondDim = data.GroupBy(row => row.Length).Single().Key;

            var result = new T[firstDim, secondDim];
            for (var i = 0; i < firstDim; ++i)
            for (var j = 0; j < secondDim; ++j)
                result[i, j] = data[i][j];

            return result;
        }
    }

    public class MatrixTest
    {
        [Fact]
        public void Create_2D_Matrix()
        {
            var data = new[]
            {
                new[] {1, 2, 3},
                new[] {4, 5, 6},
                new[] {7, 8, 9},
            };

            var expected = new[,]
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };

            Assert.Equal(expected, Matrix.Create(data));
        }
    }
}