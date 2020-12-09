using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day9
    {
        public static async Task SolvePart1()
        {
            var input = await GetInput();

            for(var i = 25; i < input.Length; i++)
            {
                var window = input.Skip(i - 25).Take(25).ToArray();
                var pairs = window.ZipAll(window, (e1, e2) => e1 + e2);

                if(!pairs.Contains(input[i]))
                {
                    Console.WriteLine(input[i]);
                    break;
                }
            }
        }

        public static async Task SolvePart2()
        {
            var input = await GetInput();
            var target = 85848519L;

            for(var i = 0; i < input.Length; i++)
            {
                var sum = 0L;
                int j;
                for(j = i; j < input.Length; j++)
                {
                    sum += input[j];
                    if (sum >= target)
                        break;
                }

                if(sum == target)
                {
                    var range = input[i..j];
                    Console.WriteLine(range.Max() + range.Min());
                    break;
                }
            }
        }

        private static async Task<long[]> GetInput()
        {
            var lines = await File.ReadAllLinesAsync(@"Day9\Day9Input.txt");

            return lines.Select(long.Parse).ToArray();
        }
    }
}
