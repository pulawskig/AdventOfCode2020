using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day7
    {
        public static async Task SolvePart1()
        {
            var input = await GetInput();

            var queue = new Queue<string>();
            queue.Enqueue("shiny gold");

            var set = new HashSet<string>();
            
            while(queue.Count > 0)
            {
                var current = queue.Dequeue();

                var names = input.Where(bag => bag.Inside.Any(i => i.Name == current)).Select(bag => bag.Name).ToArray();

                foreach(var name in names)
                {
                    if (set.Add(name))
                        queue.Enqueue(name);
                }
            }

            Console.WriteLine(set.Count);
        }

        public static async Task SolvePart2()
        {
            var input = await GetInput();

            var dict = input.ToDictionary(bag => bag.Name, _ => 0);

            var queue = new Queue<(string Name, int Count)>();
            queue.Enqueue(("shiny gold", 1));

            while(queue.Count > 0)
            {
                var (name, count) = queue.Dequeue();

                dict[name] += count;
                var bags = input.First(b => b.Name == name).Inside;

                foreach (var bag in bags)
                    queue.Enqueue((bag.Name, count * bag.Count));
            }

            Console.WriteLine(dict.Sum(x => x.Value) - 1);
        }

        private static async Task<List<BagContainment>> GetInput()
        {
            var lines = await File.ReadAllLinesAsync(@"Day7\Day7Input.txt");

            var result = new List<BagContainment>();

            var regex = new Regex(@"(\d+) ([a-zA-Z]+ [a-zA-Z]+)");

            foreach(var line in lines)
            {
                var split = line.Split(' ');
                var name = $"{split[0]} {split[1]}";

                var matches = regex.Matches(line);

                result.Add(new BagContainment
                {
                    Name = name,
                    Inside = matches.Select(match => (match.Groups[2].Value, int.Parse(match.Groups[1].Value))).ToList()
                });
            }

            return result;
        }

        private struct BagContainment
        {
            public string Name { get; set; }
            public List<(string Name, int Count)> Inside { get; set; }
        }
    }
}
