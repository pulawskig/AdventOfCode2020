using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day6
    {
        public static async Task SolvePart1()
        {
            var groups = await GetInput();
            var input = groups.Select(groups => groups.Aggregate((current, next) => current + next));
            var sum = input.Select(line => line.Distinct().Count()).Sum();

            Console.WriteLine(sum);
        }

        public static async Task SolvePart2()
        {
            var groups = await GetInput();

            var sum = 0;

            foreach(var group in groups)
            {
                IEnumerable<char> key = group.First();

                foreach(var str in group)
                {
                    key = key.Join(str, k => k, c => c, (k, c) => k);
                }

                sum += key.Count();
            }

            Console.WriteLine(sum);
        }

        private static async Task<List<List<string>>> GetInput()
        {
            var lines = await File.ReadAllLinesAsync(@"Day6\Day6Input.txt");
            return Utils.GroupByEmptyLines(lines);
        }
    }
}
