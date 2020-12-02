using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day2
    {
        public static async Task SolvePart1()
        {
            var input = await GetInput();
            var count = input
                .Where(model => model.Password.Count(c => c == model.Character).IsBetween(model.First, model.Second))
                .Count();

            Console.WriteLine(count);
        }

        public static async Task SolvePart2()
        {
            var input = await GetInput();
            var count = input
                .Where(model => model.Password[model.First - 1] == model.Character ^ model.Password[model.Second - 1] == model.Character)
                .Count();

            Console.WriteLine(count);
        }

        private static async Task<IEnumerable<Day2Model>> GetInput()
        {
            var inputs = await File.ReadAllLinesAsync(@"Day2\Day2Input.txt");
            return inputs.Select(input =>
            {
                var split = input.Split(" ", 3);
                var split2 = split[0].Split("-").Select(int.Parse).ToArray();

                return new Day2Model(split2[0], split2[1], split[1].TrimEnd(':')[0], split[2]);
            });
        }

        private record Day2Model(int First, int Second, char Character, string Password);
    }
}