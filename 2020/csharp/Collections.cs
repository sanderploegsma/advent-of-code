using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Collections
    {
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> o) where T : class =>
            o.Where(x => x != null)!;
        
        public static ISet<T> UnionAll<T>(this IEnumerable<ISet<T>> sets)
        {
            var list = sets.ToList();
            return list
                .Skip(1)
                .Aggregate(list.First(),
                    (acc, cur) =>
                    {
                        acc.UnionWith(cur);
                        return acc;
                    });
        }

        public static ISet<T> IntersectAll<T>(this IEnumerable<ISet<T>> sets)
        {
            var list = sets.ToList();
            return list
                .Skip(1)
                .Aggregate(list.First(),
                    (acc, cur) =>
                    {
                        acc.IntersectWith(cur);
                        return acc;
                    });
        }
    }
}