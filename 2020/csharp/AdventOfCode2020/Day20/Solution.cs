using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day20
{
    internal class Solution
    {
        private const char SeaPixel = '#';
        private const char MonsterPixel = 'O';

        private readonly PuzzlePiece[,] _puzzle;

        public Solution(string input)
        {
            var pieces = input.Split(Environment.NewLine + Environment.NewLine).Select(ParsePuzzlePiece).ToList();
            _puzzle = Puzzle.Solve(pieces);
        }

        public long PartOne() => _puzzle
            .GetCorners()
            .Select(piece => piece.Id)
            .Product();

        public int PartTwo()
        {
            var stitched = Puzzle.Stitch(_puzzle);
            var masked = FindSeaMonsters(stitched);
            return masked.Pixels.Cast<char>().Count(pixel => pixel == SeaPixel);
        }

        private static PuzzlePiece FindSeaMonsters(PuzzlePiece puzzle)
        {
            //                   #
            // #    ##    ##    ###
            //  #  #  #  #  #  #
            var mask = new (int X, int Y)[]
            {
                (0, 1), (1, 0),
                (4, 0), (5, 1), (6, 1), (7, 0),
                (10, 0), (11, 1), (12, 1), (13, 0),
                (16, 0), (17, 1), (18, 1), (18, 2), (19, 1),
            };

            var maskWidth = mask.Max(pixel => pixel.X);
            var maskHeight = mask.Max(pixel => pixel.Y);

            foreach (var orientation in puzzle.Orientations())
            {
                var foundMonster = false;

                for (var x = 0; x < puzzle.Width - maskWidth; x++)
                for (var y = 0; y < puzzle.Height - maskHeight; y++)
                {
                    var translatedMask = mask.Select(pixel => new {X = pixel.X + x, Y = pixel.Y + y}).ToList();
                    if (translatedMask.Any(pixel => orientation.Pixels[pixel.X, pixel.Y] != SeaPixel))
                        continue;

                    foundMonster = true;
                    foreach (var pixel in translatedMask)
                        orientation.Pixels[pixel.X, pixel.Y] = MonsterPixel;
                }

                if (foundMonster)
                    return orientation;
            }

            throw new Exception("None of the orientations contained sea monsters");
        }

        private static PuzzlePiece ParsePuzzlePiece(string data)
        {
            var id = long.Parse(data.Substring(5, 4));
            var pixels = data.Split(Environment.NewLine).Skip(1).Select(x => x.ToArray()).ToArray();

            return new PuzzlePiece(id, Matrix.Create(pixels));
        }
    }

    internal class PuzzlePiece
    {
        public PuzzlePiece(long id, char[,] pixels)
        {
            Id = id;
            Pixels = pixels;
        }

        public long Id { get; }
        public char[,] Pixels { get; }

        public int Width => Pixels.Width();
        public int Height => Pixels.Height();

        public bool Fits(PuzzlePiece other) =>
            Enum.GetValues(typeof(Matrix.Edge))
                .Cast<Matrix.Edge>()
                .Any(edge => Fits(other, edge));

        public bool Fits(PuzzlePiece other, Matrix.Edge edge)
        {
            var myEdge = Pixels.GetEdge(edge);
            return other.Pixels.GetEdges().Any(theirEdge => myEdge.SequenceEqual(theirEdge) || myEdge.SequenceEqual(theirEdge.Reverse()));
        }

        public PuzzlePiece? ConnectTo(PuzzlePiece other, Matrix.Edge theirEdge, Matrix.Edge myEdge) =>
            Orientations().FirstOrDefault(orientation => orientation.Pixels.GetEdge(myEdge).SequenceEqual(other.Pixels.GetEdge(theirEdge)));

        public IEnumerable<PuzzlePiece> Orientations()
        {
            yield return this;
            yield return new PuzzlePiece(Id, Pixels.RotateRight());
            yield return new PuzzlePiece(Id, Pixels.RotateRight().RotateRight());
            yield return new PuzzlePiece(Id, Pixels.RotateRight().RotateRight().RotateRight());
            yield return new PuzzlePiece(Id, Pixels.FlipHorizontal().RotateRight());
            yield return new PuzzlePiece(Id, Pixels.FlipHorizontal().RotateRight().RotateRight());
            yield return new PuzzlePiece(Id, Pixels.FlipHorizontal().RotateRight().RotateRight().RotateRight());
        }
    }

    internal static class Puzzle
    {
        private static IEnumerable<PuzzlePiece> FindCorners(IReadOnlyCollection<PuzzlePiece> pieces) =>
            from piece in pieces
            let otherPieces = pieces.Where(otherPiece => piece.Id != otherPiece.Id)
            where otherPieces.Count(piece.Fits) == 2
            select piece;

        private static IEnumerable<PuzzlePiece> FindTopLeft(IReadOnlyCollection<PuzzlePiece> pieces) =>
            from corner in FindCorners(pieces)
            let otherPieces = pieces.Where(otherPiece => corner.Id != otherPiece.Id)
            from orientation in corner.Orientations()
            where otherPieces.Any(piece => orientation.Fits(piece, Matrix.Edge.Right))
            where otherPieces.Any(piece => orientation.Fits(piece, Matrix.Edge.Bottom))
            select orientation;

        public static PuzzlePiece[,] Solve(IReadOnlyCollection<PuzzlePiece> pieces) =>
            FindTopLeft(pieces)
                .Select(topLeft => Solve(topLeft, pieces.Where(p => p.Id != topLeft.Id)))
                .WhereNotNull()
                .First();

        private static PuzzlePiece[,]? Solve(PuzzlePiece topLeft, IEnumerable<PuzzlePiece> otherPieces)
        {
            var piecesLeft = otherPieces.ToList();

            var size = (int) Math.Sqrt(piecesLeft.Count + 1);
            var puzzle = new PuzzlePiece[size, size];
            puzzle[0, 0] = topLeft;

            var piece = topLeft;
            var leftEdgePiece = topLeft;
            for (var y = 0; y < size; y++)
            for (var x = 0; x < size; x++)
            {
                // Top left piece is already placed
                if (x == 0 && y == 0)
                    continue;

                // Connect pieces on left edge to the bottom of the previous left edge piece
                if (x == 0)
                {
                    var leftEdgeFittingPieces = piecesLeft
                        .Select(p => p.ConnectTo(leftEdgePiece, theirEdge: Matrix.Edge.Bottom, myEdge: Matrix.Edge.Top))
                        .WhereNotNull()
                        .ToList();

                    if (leftEdgeFittingPieces.Count == 0)
                        return null;

                    piece = leftEdgeFittingPieces.First();
                    leftEdgePiece = leftEdgeFittingPieces.First();
                }
                // Connect other pieces to the right of the previous piece
                else
                {
                    var fittingPieces = piecesLeft
                        .Select(p => p.ConnectTo(piece, theirEdge: Matrix.Edge.Right, myEdge: Matrix.Edge.Left))
                        .WhereNotNull()
                        .ToList();

                    if (fittingPieces.Count == 0)
                        return null;

                    piece =  fittingPieces.First();
                }

                puzzle[x, y] = piece;
                piecesLeft = piecesLeft.Where(p => p.Id != piece.Id).ToList();
            }

            return puzzle;
        }

        public static PuzzlePiece Stitch(PuzzlePiece[,] puzzle)
        {
            // Remove the borders of each piece
            var tileWidth = puzzle[0, 0].Width - 2;
            var tileHeight = puzzle[0, 0].Height - 2;

            var width = puzzle.Width() * tileWidth;
            var height = puzzle.Height() * tileHeight;
            var pixels = new char[width, height];

            for (var puzzleY = 0; puzzleY < puzzle.Height(); puzzleY++)
            for (var puzzleX = 0; puzzleX < puzzle.Width(); puzzleX++)
            {
                var piece = puzzle[puzzleX, puzzleY];
                for (var pieceY = 1; pieceY < piece.Height - 1; pieceY++)
                for (var pieceX = 1; pieceX < piece.Width - 1; pieceX++)
                {
                    var x = pieceX - 1 + puzzleX * tileWidth;
                    var y = pieceY - 1 + puzzleY * tileHeight;
                    pixels[x, y] = piece.Pixels[pieceX, pieceY];
                }
            }

            return new PuzzlePiece(0, pixels);
        }
    }
}
