using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public static class ExtensionMethods
    {
        public static bool IsBetween(this int num, int lower, int upper)
        {
            return lower <= num && num <= upper;
        }

        public static IEnumerable<TOut> ZipAll<T1, T2, TOut>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, TOut> resultSelector)
        {
            using(var e1 = first.GetEnumerator())
            {
                while(e1.MoveNext())
                {
                    using(var e2 = second.GetEnumerator())
                    {
                        while(e2.MoveNext())
                        {
                            yield return resultSelector(e1.Current, e2.Current);
                        }
                    }
                }
            }
        }

        public static TSource MinBy<TSource, TSelector>(this IEnumerable<TSource> source, Func<TSource, TSelector> selector)
        {
            Comparer<TSelector> comparer = Comparer<TSelector>.Default;

            TSelector value;
            TSource element;

            using (IEnumerator<TSource> e = source.GetEnumerator())
            {
                if (!e.MoveNext())
                {
                    return default;
                }

                value = selector(e.Current);
                element = e.Current;
                while (e.MoveNext())
                {
                    var x = selector(e.Current);
                    if (comparer.Compare(x, value) < 0)
                    {
                        value = x;
                        element = e.Current;
                    }
                }
            }

            return element;
        }
    }
}
