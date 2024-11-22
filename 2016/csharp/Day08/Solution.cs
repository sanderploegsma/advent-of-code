using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2016.Day08
{
    internal class Solution
    {
        private const int ScreenWidth = 50;
        private const int ScreenHeight = 6;

        private readonly IReadOnlyCollection<IInstruction> _instructions;

        public Solution(IEnumerable<string> input)
        {
            _instructions = input.Select(ParseInstruction).ToList();
        }

        public int PartOne()
        {
            var screen = CreateScreen();
            return screen.Cast<bool>().Count(x => x);
        }

        public string PartTwo()
        {
            var screen = CreateScreen();
            var result = new StringBuilder();

            for (var y = 0; y < screen.Height(); y++)
            {
                var line = string.Concat(screen.GetRow(y).Select(b => b ? '#' : ' '));
                result.AppendLine(line);
            }

            return result.ToString();
        }

        private bool[,] CreateScreen()
        {
            var screen = new bool[ScreenWidth, ScreenHeight];

            foreach (var instruction in _instructions)
            {
                instruction.Invoke(screen);
            }

            return screen;
        }

        private static IInstruction ParseInstruction(string line)
        {
            Match match;

            const string rectPattern = @"rect (?<w>\d+)x(?<h>\d+)";
            if (Regex.IsMatch(line, rectPattern))
            {
                match = Regex.Match(line, rectPattern);
                var width = int.Parse(match.Groups["w"].Value);
                var height = int.Parse(match.Groups["h"].Value);
                return new DrawRectangle {Width = width, Height = height};
            }

            const string rotateRowPattern = @"rotate row y=(?<y>\d+) by (?<n>\d+)";
            if (Regex.IsMatch(line, rotateRowPattern))
            {
                match = Regex.Match(line, rotateRowPattern);
                var y = int.Parse(match.Groups["y"].Value);
                var n = int.Parse(match.Groups["n"].Value);
                return new RotateRow {Y = y, Pixels = n};
            }

            const string rotateColumnPattern = @"rotate column x=(?<x>\d+) by (?<n>\d+)";
            if (Regex.IsMatch(line, rotateColumnPattern))
            {
                match = Regex.Match(line, rotateColumnPattern);
                var x = int.Parse(match.Groups["x"].Value);
                var n = int.Parse(match.Groups["n"].Value);
                return new RotateColumn {X = x, Pixels = n};
            }

            throw new InvalidOperationException($"Unknown instruction: {line}");
        }
    }

    internal interface IInstruction
    {
        void Invoke(bool[,] screen);
    }

    internal class DrawRectangle : IInstruction
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public void Invoke(bool[,] screen)
        {
            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
            {
                screen[x, y] = true;
            }
        }
    }

    internal class RotateRow : IInstruction
    {
        public int Y { get; set; }
        public int Pixels { get; set; }

        public void Invoke(bool[,] screen)
        {
            var row = screen.GetRow(Y);

            for (var x = 0; x < screen.Width(); x++)
            {
                var newX = (x + Pixels) % screen.Width();
                screen[newX, Y] = row[x];
            }
        }
    }

    internal class RotateColumn : IInstruction
    {
        public int X { get; set; }
        public int Pixels { get; set; }

        public void Invoke(bool[,] screen)
        {
            var column = screen.GetColumn(X);

            for (var y = 0; y < screen.Height(); y++)
            {
                var newY = (y + Pixels) % screen.Height();
                screen[X, newY] = column[y];
            }
        }
    }
}
