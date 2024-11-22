using AdventOfCode.Common;
using FsCheck;
using FsCheck.Xunit;
using System;

namespace AdventOfCode.Common.Test
{
    public class NumbersExtensionsTest
    {
        [Property]
        public Property Mod_AlwaysReturnsANumberGreaterThanOrEqualToZero(int n, int m)
        {
            Func<bool> property = () => n.Mod(m) >= 0;
            return property.When(m > 0);
        }
    }
}
