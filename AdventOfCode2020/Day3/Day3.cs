using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day3
    {
        public static async Task SolvePart1()
        {
            var input = await GetInput();

            var count = 0;
            var j = 0;
            var width = input[0].Length;
            for(int i = 1; i < input.Length; i++)
            {
                j = (j + 3) % width;

                if (input[i][j])
                    count++;
            }

            Console.WriteLine(count);
        }

        public static async Task SolvePart2()
        {
            var input = await GetInput();

            var scenarios = new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

            var result = 1l;
            var width = input[0].Length;

            foreach (var (right, down) in scenarios)
            {
                var count = 0;
                var j = 0;
                for (int i = down; i < input.Length; i += down)
                {
                    j = (j + right) % width;

                    if (input[i][j])
                        count++;
                }

                result *= count;
            }

            Console.WriteLine(result);
        }

        private static async Task<bool[][]> GetInput()
        {
            var lines = await File.ReadAllLinesAsync(@"Day3\Day3Input.txt");
            var input = lines.Select(line => line.Select(c => c == '#').ToArray()).ToArray();

            return input;
        }
    }
}
