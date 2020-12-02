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
    }
}
