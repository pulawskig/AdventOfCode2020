using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day10
    {
        public static async Task SolvePart1()
        {
            var input = await GetInput();

            var zip = input.Append(input.Max() + 3).Zip(input.Prepend(0), (a, b) => a - b).ToArray();
            var ones = zip.Where(x => x == 1).Count();
            var threes = zip.Where(x => x == 3).Count();

            Console.WriteLine(ones * threes);
        }

        public static async Task SolvePart2()
        {
            var input = await GetInput();

            input = input
                .Prepend(0)
                .Append(input.Max() + 3)
                .Reverse()
                .ToArray();

            var subtrees = input.ToDictionary(x => x, _ => 0L);

            foreach (var current in input)
            {
                subtrees[current] = input
                    .Where(x => x > current && x <= current + 3)
                    .Select(x => subtrees[x])
                    .DefaultIfEmpty(1)
                    .Sum();
            }

            Console.WriteLine(subtrees[0]);
        }

        private static async Task<int[]> GetInput()
        {
            var lines = await File.ReadAllLinesAsync(@"Day10\Day10Input.txt");

            return lines
                .Select(int.Parse)
                .OrderBy(x => x)
                .ToArray();
        }
    }
}
