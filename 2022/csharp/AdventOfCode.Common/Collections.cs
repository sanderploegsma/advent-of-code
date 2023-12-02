using System.Collections.Generic;
using System.Linq;
using System;

namespace AdventOfCode.Common
{
    public static class Collections
    {
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> o) where T : class =>
            o.Where(x => x != null)!;

        public static IEnumerable<(T, T)> Pairwise<T>(this IEnumerable<T> items)
        {
            var list = items.ToList();
            return Enumerable.Zip(list.SkipLast(1), list.Skip(1), (a, b) => (a, b));
        }

        public static IEnumerable<(T Value, int Index)> Indexed<T>(this IEnumerable<T> items, int start = 0) =>
            items.Select((item, i) => (item, i + start));

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

        public static IEnumerable<IEnumerable<T>> SubSets<T>(this IEnumerable<T> items)
        {
            var set = items.ToArray();

            bool[] state = new bool[set.Length + 1];
            for (int x; !state[set.Length]; state[x] = true)
            {
                yield return Enumerable.Range(0, state.Length)
                    .Where(i => state[i])
                    .Select(i => set[i]);

                for (x = 0; state[x]; state[x++] = false)
                {
                }
            }
        }

        public static IEnumerable<T> TakeUntilIncluding<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            foreach (var item in items)
            {
                yield return item;
                if (predicate(item))
                {
                    yield break;
                }
            }
        }
    }
}