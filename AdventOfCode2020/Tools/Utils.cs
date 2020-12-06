using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public static class Utils
    {
        public static List<List<string>> GroupByEmptyLines(IEnumerable<string> input)
        {
            var result = new List<List<string>>();

            var properInput = input as string[] ?? input.ToArray();

            var last = 0;
            for (var i = 0; i < properInput.Length; i++)
            {
                var line = properInput[i];

                if (string.IsNullOrEmpty(line))
                {
                    result.Add(input.Skip(last).Take(i - last).ToList());
                    last = i + 1;
                }
            }

            result.Add(input.Skip(last).Take(properInput.Length - last).ToList());

            return result;
        }
    }
}
