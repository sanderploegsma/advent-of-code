using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day03
{
    internal class Solution
    {
        private const char Tree = '#';
        private readonly char[,] _matrix;

        public Solution(IEnumerable<string> input)
        {
            var grid = input.Select(x => x.ToCharArray()).ToArray();
            _matrix = Matrix.Create(grid);
        }

        public int PartOne() => CountTreesInPath(3, 1);

        public long PartTwo() => new (int dx, int dy)[] {(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)}
            .Select(stepSize => CountTreesInPath(stepSize.dx, stepSize.dy))
            .Select(n => (long) n)
            .Product();

        private int CountTreesInPath(int dx, int dy)
        {
            var trees = 0;
            var (x, y) = (0, 0);
            
            while (y < _matrix.Height())
            {
                if (_matrix[x, y] == Tree)
                    trees++;

                y += dy;
                x += dx;
                x %= _matrix.Width();
            }

            return trees;
        }
    }
}