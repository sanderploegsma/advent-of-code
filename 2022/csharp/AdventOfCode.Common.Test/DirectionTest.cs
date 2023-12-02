using AdventOfCode.Common;
using FsCheck.Xunit;
using FsCheck;

namespace AdventOfCode.Common.Test
{
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
