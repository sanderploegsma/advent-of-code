using System;
using FsCheck;
using FsCheck.Xunit;

namespace AdventOfCode2020
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public static class DirectionExtensions
    {
        private static readonly Direction[] LeftTurns =
            {Direction.North, Direction.West, Direction.South, Direction.East};

        private static readonly Direction[] RightTurns =
            {Direction.North, Direction.East, Direction.South, Direction.West};

        public static Direction TurnLeft(this Direction direction, int times = 1) => direction.Turn(LeftTurns, times);

        public static Direction TurnRight(this Direction direction, int times = 1) => direction.Turn(RightTurns, times);

        private static Direction Turn(this Direction direction, Direction[] turns, int times)
        {
            var id = (Array.IndexOf(turns, direction) + times).Mod(turns.Length);
            return turns[id];
        }
    }

    public class DirectionTest
    {
        [Property]
        public Property TurningLeftFourTimes_ShouldReturnTheSameDirection(Direction direction) => 
            (direction.TurnLeft(4) == direction).ToProperty();

        [Property]
        public Property TurningRightFourTimes_ShouldReturnTheSameDirection(Direction direction) => 
            (direction.TurnRight(4) == direction).ToProperty();

        [Property]
        public Property TurningLeft_ShouldMirrorTurningRight(Direction direction, int leftTurns, int rightTurns) =>
            (direction.TurnLeft(leftTurns) == direction.TurnRight(rightTurns)).When((leftTurns + rightTurns) % 4 == 0);
    }
}