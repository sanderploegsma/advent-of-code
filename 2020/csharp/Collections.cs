using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Collections
    {
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> o) where T : class =>
            o.Where(x => x != null)!;
    }
}