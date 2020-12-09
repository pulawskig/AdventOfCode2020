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
    }
}
